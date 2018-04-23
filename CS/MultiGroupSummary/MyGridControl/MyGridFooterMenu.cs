using DevExpress.XtraGrid.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace Q354185.MyXtraGrid {
    public class MyGridFooterMenu : GridViewFooterMenu {
        public MyGridFooterMenu(GridView view)
            : base(view) {
        }
        public override void Init(object info) {
            var myInfo = info as GridHitInfo;
            base.Init(info);
            var rowSummaryItem = View.GetRowSummaryItem(myInfo.RowHandle, myInfo.Column);
            SetSummaryItem((GridGroupSummaryItem)rowSummaryItem.Key);
            if (myInfo.HitTest != GridHitTest.RowFooter) {
                return;
            }
            var item = myInfo.FooterCell.ColumnInfo.Tag as GridGroupSummaryItem;
            SetSummaryItem(item);
        }
        private void SetSummaryItem(GridGroupSummaryItem item) {
            if (item == null) {
                return;
            }
            SummaryItem = item;
            CreateItems();
        }
    }
}
