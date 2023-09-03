Namespace MultiGroupSummary

    Partial Class Form1

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (Me.components IsNot Nothing) Then
                Me.components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

'#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.dataSet1 = New System.Data.DataSet()
            Me.dataTable1 = New System.Data.DataTable()
            Me.dataColumn1 = New System.Data.DataColumn()
            Me.dataColumn2 = New System.Data.DataColumn()
            Me.dataColumn3 = New System.Data.DataColumn()
            Me.dataColumn4 = New System.Data.DataColumn()
            Me.myGridControl1 = New Q354185.MyXtraGrid.MyGridControl()
            Me.myGridView1 = New Q354185.MyXtraGrid.MyGridView()
            Me.colString = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colInt = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colDate = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colData = New DevExpress.XtraGrid.Columns.GridColumn()
            CType((Me.dataSet1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.dataTable1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.myGridControl1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.myGridView1), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' dataSet1
            ' 
            Me.dataSet1.DataSetName = "NewDataSet"
            Me.dataSet1.Tables.AddRange(New System.Data.DataTable() {Me.dataTable1})
            ' 
            ' dataTable1
            ' 
            Me.dataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.dataColumn1, Me.dataColumn2, Me.dataColumn3, Me.dataColumn4})
            Me.dataTable1.TableName = "Table1"
            ' 
            ' dataColumn1
            ' 
            Me.dataColumn1.ColumnName = "String"
            ' 
            ' dataColumn2
            ' 
            Me.dataColumn2.ColumnName = "Int"
            Me.dataColumn2.DataType = GetType(Integer)
            ' 
            ' dataColumn3
            ' 
            Me.dataColumn3.ColumnName = "Date"
            Me.dataColumn3.DataType = GetType(System.DateTime)
            ' 
            ' dataColumn4
            ' 
            Me.dataColumn4.ColumnName = "Data"
            ' 
            ' myGridControl1
            ' 
            Me.myGridControl1.DataMember = "Table1"
            Me.myGridControl1.DataSource = Me.dataSet1
            Me.myGridControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.myGridControl1.Location = New System.Drawing.Point(0, 0)
            Me.myGridControl1.MainView = Me.myGridView1
            Me.myGridControl1.Name = "myGridControl1"
            Me.myGridControl1.Size = New System.Drawing.Size(463, 409)
            Me.myGridControl1.TabIndex = 0
            Me.myGridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.myGridView1})
            ' 
            ' myGridView1
            ' 
            Me.myGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colString, Me.colInt, Me.colDate, Me.colData})
            Me.myGridView1.GridControl = Me.myGridControl1
            Me.myGridView1.GroupCount = 2
            Me.myGridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Min, "Data", Me.colData, "Min data = {0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Max, "Data", Me.colData, "Max data = {0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Data", Me.colData, "Count data = {0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Min, "Int", Me.colInt, "Min int = {0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Max, "Int", Me.colInt, "Max int = {0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Average, "Int", Me.colInt, "Avg int = {0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Int", Me.colInt, "Sum int = {0}")})
            Me.myGridView1.Name = "myGridView1"
            Me.myGridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colDate, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colString, DevExpress.Data.ColumnSortOrder.Ascending)})
            ' 
            ' colString
            ' 
            Me.colString.FieldName = "String"
            Me.colString.Name = "colString"
            Me.colString.Visible = True
            Me.colString.VisibleIndex = 0
            ' 
            ' colInt
            ' 
            Me.colInt.FieldName = "Int"
            Me.colInt.Name = "colInt"
            Me.colInt.Visible = True
            Me.colInt.VisibleIndex = 1
            ' 
            ' colDate
            ' 
            Me.colDate.FieldName = "Date"
            Me.colDate.Name = "colDate"
            Me.colDate.Visible = True
            Me.colDate.VisibleIndex = 2
            ' 
            ' colData
            ' 
            Me.colData.FieldName = "Data"
            Me.colData.Name = "colData"
            Me.colData.Visible = True
            Me.colData.VisibleIndex = 0
            Me.colData.Width = 83
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(463, 409)
            Me.Controls.Add(Me.myGridControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            AddHandler Me.Load, New System.EventHandler(AddressOf Me.Form1_Load)
            CType((Me.dataSet1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.dataTable1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.myGridControl1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.myGridView1), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

'#End Region
        Private myGridControl1 As Q354185.MyXtraGrid.MyGridControl

        Private dataSet1 As System.Data.DataSet

        Private dataTable1 As System.Data.DataTable

        Private dataColumn1 As System.Data.DataColumn

        Private dataColumn2 As System.Data.DataColumn

        Private dataColumn3 As System.Data.DataColumn

        Private dataColumn4 As System.Data.DataColumn

        Private myGridView1 As Q354185.MyXtraGrid.MyGridView

        Private colString As DevExpress.XtraGrid.Columns.GridColumn

        Private colInt As DevExpress.XtraGrid.Columns.GridColumn

        Private colDate As DevExpress.XtraGrid.Columns.GridColumn

        Private colData As DevExpress.XtraGrid.Columns.GridColumn
    End Class
End Namespace
