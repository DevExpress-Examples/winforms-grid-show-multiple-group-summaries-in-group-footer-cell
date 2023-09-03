Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data

Namespace Q354185.MyXtraGrid

    Public Class MyGridView
        Inherits DevExpress.XtraGrid.Views.Grid.GridView

        Public Sub New(ByVal ownerGrid As DevExpress.XtraGrid.GridControl)
            MyBase.New(ownerGrid)
        End Sub

        Public Sub New()
        End Sub

        Protected Overrides ReadOnly Property ViewName As String
            Get
                Return "MyGridView"
            End Get
        End Property

        Public Function GetGroupFooterRowCount() As Integer
            If Me.IsDesignMode Then
                Return 0
            End If

            Dim footerRowCount = Me.GetFooterRowCount()
            If footerRowCount.Count = 0 Then
                Return 0
            End If

            Return footerRowCount.Max(Function(kvp) kvp.Value)
        End Function

        Private Function GetFooterRowCount() As Dictionary(Of DevExpress.XtraGrid.Columns.GridColumn, Integer)
            Dim columnFooterRowCount = New System.Collections.Generic.Dictionary(Of DevExpress.XtraGrid.Columns.GridColumn, Integer)()
            For Each summary As DevExpress.XtraGrid.GridGroupSummaryItem In Me.GroupSummary
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
            Next

            Return columnFooterRowCount
        End Function

        Protected Overrides Sub SynchronizeSummary(ByVal gridSum As System.Collections.IList, ByVal summary As DevExpress.Data.SummaryItemCollection)
            Dim gridSumCollection = TryCast(gridSum, DevExpress.XtraGrid.GridGroupSummaryItemCollection)
            If gridSumCollection Is Nothing Then
                MyBase.SynchronizeSummary(gridSum, summary)
                Return
            End If

            Dim myGridSumCollection = New System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem)()
            Dim groupFooterRowCount = Me.GetGroupFooterRowCount()
            Me.MyGridSumCollectionAddSummary(gridSumCollection, myGridSumCollection)
            Dim query = Me.GetGroups(myGridSumCollection)
            Dim tempSummaryItemList = New System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem)()
            Me.FillTempSummaryItemList(groupFooterRowCount, query, tempSummaryItemList)
            Me.GridSumCollectionAddSummaries(gridSumCollection, tempSummaryItemList)
            MyBase.SynchronizeSummary(gridSum, summary)
        End Sub

        Private Sub MyGridSumCollectionAddSummary(ByVal gridSumCollection As DevExpress.XtraGrid.GridGroupSummaryItemCollection, ByVal myGridSumCollection As System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem))
            For Each summaryItem As DevExpress.XtraGrid.GridGroupSummaryItem In gridSumCollection
                myGridSumCollection.Add(summaryItem)
            Next
        End Sub

        Private Function GetGroups(ByVal gridSumCollection As System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem)) As IEnumerable(Of System.Linq.IGrouping(Of DevExpress.XtraGrid.Columns.GridColumn, DevExpress.XtraGrid.GridGroupSummaryItem))
             ''' Cannot convert LocalDeclarationStatementSyntax, System.InvalidCastException: Unable to cast object of type 'Microsoft.CodeAnalysis.VisualBasic.Syntax.EmptyStatementSyntax' to type 'Microsoft.CodeAnalysis.VisualBasic.Syntax.ExpressionSyntax'.
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.RemodelVariableDeclaration(VariableDeclarationSyntax declaration)
'''    at ICSharpCode.CodeConverter.VB.MethodBodyExecutableStatementVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input:
'''             var groups = from item in gridSumCollection
'''                          group item by item.ShowInGroupColumnFooter;
''' 
'''  Return groups
        End Function

        Private Sub FillTempSummaryItemList(ByVal groupFooterRowCount As Integer, ByVal query As System.Collections.Generic.IEnumerable(Of System.Linq.IGrouping(Of DevExpress.XtraGrid.Columns.GridColumn, DevExpress.XtraGrid.GridGroupSummaryItem)), ByVal tempSummaryItemList As System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem))
            For Each group In query
                Dim summaryItem As DevExpress.XtraGrid.GridGroupSummaryItem = Nothing
                Dim showInGroupColumnFooter As DevExpress.XtraGrid.Columns.GridColumn = Nothing
                Dim fieldName = String.Empty
                Dim summaryItemList = group.ToList()
                For i = 0 To groupFooterRowCount - 1
                    Try
                        summaryItem = summaryItemList(i)
                    Catch
                        summaryItem = Nothing
                    End Try

                    If summaryItem IsNot Nothing Then
                        fieldName = summaryItem.FieldName
                        showInGroupColumnFooter = summaryItem.ShowInGroupColumnFooter
                        Continue For
                    End If

                    If Not Equals(fieldName, String.Empty) AndAlso showInGroupColumnFooter IsNot Nothing Then
                        summaryItem = New DevExpress.XtraGrid.GridGroupSummaryItem() With {.SummaryType = DevExpress.Data.SummaryItemType.None, .ShowInGroupColumnFooter = showInGroupColumnFooter, .FieldName = fieldName}
                        tempSummaryItemList.Add(summaryItem)
                    End If
                Next
            Next

            Me.AddEmptySummaryColumn(groupFooterRowCount, tempSummaryItemList)
        End Sub

        Private Sub GridSumCollectionAddSummaries(ByVal gridSumCollection As DevExpress.XtraGrid.GridGroupSummaryItemCollection, ByVal tempSummaryItemList As System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem))
            If tempSummaryItemList.Count > 0 Then
                gridSumCollection.AddRange(tempSummaryItemList.ToArray())
            End If
        End Sub

        Private Sub AddEmptySummaryColumn(ByVal groupFooterRowCount As Integer, ByVal tempSummaryItemList As System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem))
            Dim footerRowCount = Me.GetFooterRowCount()
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In Me.Columns
                If footerRowCount.ContainsKey(col) Then
                    Continue For
                End If

                Me.CreateEmptySummaries(col, tempSummaryItemList, groupFooterRowCount)
            Next
        End Sub

        Private Sub CreateEmptySummaries(ByVal column As DevExpress.XtraGrid.Columns.GridColumn, ByVal tempSummaryItemList As System.Collections.Generic.List(Of DevExpress.XtraGrid.GridGroupSummaryItem), ByVal groupFooterRowCount As Integer)
            Dim summaryItem As DevExpress.XtraGrid.GridGroupSummaryItem = Nothing
            Dim showInGroupColumnFooter As DevExpress.XtraGrid.Columns.GridColumn = Nothing
            Dim fieldName = String.Empty
            For i = 0 To groupFooterRowCount - 1 - 1
                showInGroupColumnFooter = column
                fieldName = column.FieldName
                summaryItem = New DevExpress.XtraGrid.GridGroupSummaryItem() With {.SummaryType = DevExpress.Data.SummaryItemType.None, .ShowInGroupColumnFooter = showInGroupColumnFooter, .FieldName = fieldName}
                tempSummaryItemList.Add(summaryItem)
            Next
        End Sub
    End Class
End Namespace
