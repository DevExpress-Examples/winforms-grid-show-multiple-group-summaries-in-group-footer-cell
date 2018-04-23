using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;

namespace MultiGroupSummary {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            FillTable(10);
            SetToolTip();
        }
        private void FillTable(int rowCount) {
            for (var i = 0; i < rowCount; i++) {
                dataTable1.Rows.Add(new object[] { String.Format("Row {0}", i), i, DateTime.Today.AddDays(i / 2 * 2), String.Format("Row #{0}", 100 - i) });
            }
            for (var i = rowCount; i < 2 * rowCount; i++) {
                dataTable1.Rows.Add(new object[] { String.Format("Row {0}", i), i * 2, DateTime.Today.AddDays(i / 2 * 2), String.Format("Row #{0}", 100 - i) });
            }
        }
        private void SetToolTip() {
            new ToolTipHelper(myGridControl1);
        }
    }
}
