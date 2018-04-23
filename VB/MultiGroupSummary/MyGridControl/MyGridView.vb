Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data

Namespace Q354185.MyXtraGrid
	Public Class MyGridView
		Inherits GridView
		Public Sub New(ByVal ownerGrid As GridControl)
			MyBase.New(ownerGrid)
		End Sub

		Public Sub New()
		End Sub

		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property
		Public Function GetGroupFooterRowCount() As Integer
			If IsDesignMode Then
				Return 0
			End If
			Dim footerRowCount = GetFooterRowCount()
			If footerRowCount.Count = 0 Then
				Return 0
			End If
			Return footerRowCount.Max(Function(kvp) kvp.Value)
		End Function
		Private Function GetFooterRowCount() As Dictionary(Of GridColumn, Integer)
			Dim columnFooterRowCount = New Dictionary(Of GridColumn, Integer)()
			For Each summary As GridGroupSummaryItem In GroupSummary
				Dim col = summary.ShowInGroupColumnFooter
				If col Is Nothing Then
					Continue For
				End If
				If columnFooterRowCount.ContainsKey(col) Then
					columnFooterRowCount(col) += 1
				End If
				If Not(columnFooterRowCount.ContainsKey(col)) Then
					columnFooterRowCount.Add(col, 1)
				End If
			Next summary
			Return columnFooterRowCount
		End Function
		Protected Overrides Sub SynchronizeSummary(ByVal gridSum As System.Collections.IList, ByVal summary As SummaryItemCollection)
			Dim gridSumCollection = TryCast(gridSum, GridGroupSummaryItemCollection)
			If gridSumCollection Is Nothing Then
				MyBase.SynchronizeSummary(gridSum, summary)
				Return
			End If
			Dim myGridSumCollection = New List(Of GridGroupSummaryItem)()
			Dim groupFooterRowCount = GetGroupFooterRowCount()
			MyGridSumCollectionAddSummary(gridSumCollection, myGridSumCollection)
            Dim query = From item In gridSumCollection
                Group item By item.ShowInGroupColumnFooter
                Into Group
			Dim tempSummaryItemList = New List(Of GridGroupSummaryItem)()            
            For Each group In query
                Dim summaryItem As GridGroupSummaryItem = Nothing
                Dim showInGroupColumnFooter As GridColumn = Nothing
                Dim fieldName = String.Empty                
                For i = 0 To groupFooterRowCount - 1
                    Try
                        summaryItem = group.Group(i)
                    Catch
                        summaryItem = Nothing
                    End Try
                    If summaryItem IsNot Nothing Then
                        fieldName = summaryItem.FieldName
                        showInGroupColumnFooter = summaryItem.ShowInGroupColumnFooter
                        Continue For
                    End If
                    If fieldName IsNot String.Empty AndAlso showInGroupColumnFooter IsNot Nothing Then
                        summaryItem = New GridGroupSummaryItem() With {.SummaryType = SummaryItemType.None, .ShowInGroupColumnFooter = showInGroupColumnFooter, .FieldName = fieldName}
                        tempSummaryItemList.Add(summaryItem)
                    End If
                Next i
            Next group
            AddEmptySummaryColumn(groupFooterRowCount, tempSummaryItemList)
			GridSumCollectionAddSummaries(gridSumCollection, tempSummaryItemList)
			MyBase.SynchronizeSummary(gridSum, summary)
		End Sub
		Private Sub MyGridSumCollectionAddSummary(ByVal gridSumCollection As GridGroupSummaryItemCollection, ByVal myGridSumCollection As List(Of GridGroupSummaryItem))
			For Each summaryItem As GridGroupSummaryItem In gridSumCollection
				myGridSumCollection.Add(summaryItem)
			Next summaryItem
		End Sub
        Private Sub GridSumCollectionAddSummaries(ByVal gridSumCollection As GridGroupSummaryItemCollection, ByVal tempSummaryItemList As List(Of GridGroupSummaryItem))
            If tempSummaryItemList.Count > 0 Then
                gridSumCollection.AddRange(tempSummaryItemList.ToArray())
            End If
        End Sub
		Private Sub AddEmptySummaryColumn(ByVal groupFooterRowCount As Integer, ByVal tempSummaryItemList As List(Of GridGroupSummaryItem))
			Dim footerRowCount = GetFooterRowCount()
			For Each col As GridColumn In Columns
				If footerRowCount.ContainsKey(col) Then
					Continue For
				End If
				CreateEmptySummaries(col, tempSummaryItemList, groupFooterRowCount)
			Next col
		End Sub
		Private Sub CreateEmptySummaries(ByVal column As GridColumn, ByVal tempSummaryItemList As List(Of GridGroupSummaryItem), ByVal groupFooterRowCount As Integer)
			Dim summaryItem As GridGroupSummaryItem = Nothing
			Dim showInGroupColumnFooter As GridColumn = Nothing
			Dim fieldName = String.Empty
			For i = 0 To groupFooterRowCount - 2
				showInGroupColumnFooter = column
				fieldName = column.FieldName
				summaryItem = New GridGroupSummaryItem() With {.SummaryType = SummaryItemType.None, .ShowInGroupColumnFooter = showInGroupColumnFooter, .FieldName = fieldName}
				tempSummaryItemList.Add(summaryItem)
			Next i
		End Sub
	End Class
End Namespace
