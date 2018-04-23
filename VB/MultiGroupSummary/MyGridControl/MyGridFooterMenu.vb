Imports DevExpress.XtraGrid.Menu
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid

Namespace Q354185.MyXtraGrid
    Public Class MyGridFooterMenu
        Inherits GridViewFooterMenu

        Public Sub New(ByVal view As GridView)
            MyBase.New(view)
        End Sub
        Public Overrides Sub Init(ByVal info As Object)
            Dim myInfo = TryCast(info, GridHitInfo)
            MyBase.Init(info)
            Dim rowSummaryItem = View.GetRowSummaryItem(myInfo.RowHandle, myInfo.Column)
            SetSummaryItem(CType(rowSummaryItem.Key, GridGroupSummaryItem))
            If myInfo.HitTest <> GridHitTest.RowFooter Then
                Return
            End If
            Dim item = TryCast(myInfo.FooterCell.ColumnInfo.Tag, GridGroupSummaryItem)
            SetSummaryItem(item)
        End Sub
        Private Sub SetSummaryItem(ByVal item As GridGroupSummaryItem)
            If item Is Nothing Then
                Return
            End If
            SummaryItem = item
            CreateItems()
        End Sub
    End Class
End Namespace
