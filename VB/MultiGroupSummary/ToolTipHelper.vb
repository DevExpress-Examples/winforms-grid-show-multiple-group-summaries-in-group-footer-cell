Imports Microsoft.VisualBasic
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraGrid
Imports Q354185.MyXtraGrid
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace MultiGroupSummary
	Public Class ToolTipHelper
		Private gridCtrl As MyGridControl
		Public Sub New(ByVal myGridControl1 As MyGridControl)
			Init(myGridControl1)
		End Sub
		Private Sub Init(ByVal gridControl As MyGridControl)
			gridCtrl = gridControl
			Dim toolTipController = New ToolTipController()
			gridCtrl.ToolTipController = toolTipController
			AddHandler toolTipController.GetActiveObjectInfo, AddressOf ToolTipController_GetActiveObjectInfo
		End Sub
		Private Sub ToolTipController_GetActiveObjectInfo(ByVal sender As Object, ByVal e As ToolTipControllerGetActiveObjectInfoEventArgs)
			Dim info As ToolTipControlInfo = Nothing
			Dim view = TryCast(gridCtrl.GetViewAt(e.ControlMousePosition), MyGridView)
			If view Is Nothing Then
				Return
			End If
			Dim hitInfo = view.CalcHitInfo(e.ControlMousePosition)
			If hitInfo Is Nothing Then
				Return
			End If
			Dim hitInfoFooterCell = hitInfo.FooterCell
			If hitInfoFooterCell IsNot Nothing Then
				Dim summaryItem = New GridGroupSummaryItem()
				Dim tag = TryCast(hitInfoFooterCell.ColumnInfo.Tag, GridGroupSummaryItem)
				If tag IsNot Nothing Then
					summaryItem = tag
				Else
					Dim rowSummaryItem = view.GetRowSummaryItem(hitInfo.RowHandle, hitInfoFooterCell.Column)
					summaryItem = TryCast(rowSummaryItem.Key, GridGroupSummaryItem)
				End If
				If summaryItem Is Nothing Then
					Return
				End If
				If summaryItem.SummaryType = SummaryItemType.None Then
					Return
				End If
				info = New ToolTipControlInfo(hitInfoFooterCell.Value, hitInfoFooterCell.DisplayText)
			End If
			If info Is Nothing Then
				Return
			End If
			e.Info = info
		End Sub
	End Class
End Namespace
