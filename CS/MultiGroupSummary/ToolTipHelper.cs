using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using Q354185.MyXtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiGroupSummary {
    public class ToolTipHelper {
        private MyGridControl gridCtrl;
        public ToolTipHelper(MyGridControl myGridControl1) {
            Init(myGridControl1);
        }
        private void Init(MyGridControl gridControl) {
            gridCtrl = gridControl;
            var toolTipController = new ToolTipController();
            gridCtrl.ToolTipController = toolTipController;
            toolTipController.GetActiveObjectInfo += new ToolTipControllerGetActiveObjectInfoEventHandler(ToolTipController_GetActiveObjectInfo);
        }
        private void ToolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e) {
            ToolTipControlInfo info = null;
            var view = gridCtrl.GetViewAt(e.ControlMousePosition) as MyGridView;
            if (view == null) {
                return;
            }
            var hitInfo = view.CalcHitInfo(e.ControlMousePosition);
            if (hitInfo == null) {
                return;
            }
            var hitInfoFooterCell = hitInfo.FooterCell;
            if (hitInfoFooterCell != null) {
                var summaryItem = new GridGroupSummaryItem();
                var tag = hitInfoFooterCell.ColumnInfo.Tag as GridGroupSummaryItem;
                if (tag != null) {
                    summaryItem = tag;
                } else {
                    var rowSummaryItem = view.GetRowSummaryItem(hitInfo.RowHandle, hitInfoFooterCell.Column);
                    summaryItem = rowSummaryItem.Key as GridGroupSummaryItem;
                }
                if (summaryItem == null) {
                    return;
                }
                if (summaryItem.SummaryType == SummaryItemType.None) {
                    return;
                }
                info = new ToolTipControlInfo(hitInfoFooterCell.Value, hitInfoFooterCell.DisplayText);
            }
            if (info == null) {
                return;
            }
            e.Info = info;
        }
    }
}
