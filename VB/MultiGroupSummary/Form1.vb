Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraEditors

Namespace MultiGroupSummary
	Partial Public Class Form1
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			FillTable(10)
			SetToolTip()
		End Sub
		Private Sub FillTable(ByVal rowCount As Integer)
			For i = 0 To rowCount - 1
				dataTable1.Rows.Add(New Object() { String.Format("Row {0}", i), i, DateTime.Today.AddDays(i / 2 * 2), String.Format("Row #{0}", 100 - i) })
			Next i
			For i = rowCount To 2 * rowCount - 1
				dataTable1.Rows.Add(New Object() { String.Format("Row {0}", i), i * 2, DateTime.Today.AddDays(i / 2 * 2), String.Format("Row #{0}", 100 - i) })
			Next i
		End Sub
		Private Sub SetToolTip()
			Dim TempToolTipHelper As ToolTipHelper = New ToolTipHelper(myGridControl1)
		End Sub
	End Class
End Namespace
