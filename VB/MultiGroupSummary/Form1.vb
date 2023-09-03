Imports System
Imports DevExpress.XtraEditors

Namespace MultiGroupSummary

    Public Partial Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            FillTable(10)
            SetToolTip()
        End Sub

        Private Sub FillTable(ByVal rowCount As Integer)
            For i = 0 To rowCount - 1
                dataTable1.Rows.Add(New Object() {String.Format("Row {0}", i), i, Date.Today.AddDays(i \ 2 * 2), String.Format("Row #{0}", 100 - i)})
            Next

            For i = rowCount To 2 * rowCount - 1
                dataTable1.Rows.Add(New Object() {String.Format("Row {0}", i), i * 2, Date.Today.AddDays(i \ 2 * 2), String.Format("Row #{0}", 100 - i)})
            Next
        End Sub

        Private Sub SetToolTip()
            Dim tmp_ToolTipHelper = New ToolTipHelper(myGridControl1)
        End Sub
    End Class
End Namespace
