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
        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            FillTable(10)
            SetToolTip()

            colString.Group()
            colDate.Group()
        End Sub
        Private Sub FillTable(ByVal rowCount As Integer)
            For index = 0 To rowCount - 1
                dataTable1.Rows.Add(New Object() {String.Format("Row {0}", index), index, Date.Today.AddDays(index / 2 * 2), String.Format("Row #{0}", 100 - index)})
            Next index
            Dim i = rowCount
            Do While i < 2 * rowCount
                dataTable1.Rows.Add(New Object() {String.Format("Row {0}", i), i * 2, Date.Today.AddDays(i / 2 * 2), String.Format("Row #{0}", 100 - i)})
                i += 1
            Loop
        End Sub
        Private Sub SetToolTip()
            Dim tempVar As New ToolTipHelper(myGridControl1)
        End Sub
    End Class
End Namespace
