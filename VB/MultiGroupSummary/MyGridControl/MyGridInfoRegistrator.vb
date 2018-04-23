Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Grid

Namespace Q354185.MyXtraGrid
    Public Class MyGridInfoRegistrator
        Inherits GridInfoRegistrator

        Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
            Return New MyGridView(TryCast(grid, GridControl))
        End Function
        Public Overrides Function CreateViewInfo(ByVal view As BaseView) As DevExpress.XtraGrid.Views.Base.ViewInfo.BaseViewInfo
            Return New MyGridViewInfo(CType(view, GridView))
        End Function
        Public Overrides ReadOnly Property ViewName() As String
            Get
                Return "MyGridView"
            End Get
        End Property
        Public Overrides Function CreateHandler(ByVal view As BaseView) As DevExpress.XtraGrid.Views.Base.Handler.BaseViewHandler
            Return New MyGridHandler(TryCast(view, MyGridView))
        End Function
    End Class
End Namespace
