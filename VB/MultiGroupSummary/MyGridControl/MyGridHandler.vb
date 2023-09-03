Imports DevExpress.XtraGrid.Views.Grid.Handler
Imports DevExpress.XtraGrid.Views.Grid

Namespace Q354185.MyXtraGrid

    Public Class MyGridHandler
        Inherits GridHandler

        Public Sub New(ByVal gridView As GridView)
            MyBase.New(gridView)
        End Sub

        Protected Overrides Function CreateGridViewFooterMenu(ByVal gridView As GridView) As DevExpress.XtraGrid.Menu.GridViewFooterMenu
            Return New MyGridFooterMenu(gridView)
        End Function
    End Class
End Namespace
