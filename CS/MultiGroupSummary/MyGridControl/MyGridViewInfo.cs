using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;

namespace Q354185.MyXtraGrid {
    public class MyGridViewInfo : GridViewInfo {
        public MyGridViewInfo(GridView gridView)
            : base(gridView) {
        }
        protected override int CalcGroupFooterHeight() {
            var myView = (MyGridView)View;
            if (myView.GetGroupFooterRowCount() <= 0) {
                return 0;
            }
            return base.CalcGroupFooterHeight() * myView.GetGroupFooterRowCount();
        }
        protected override int GetMaxColumnFooterCount() {
            return ((MyGridView)View).GetGroupFooterRowCount();
        }
        protected override void CalcRowCellsFooterInfo(GridRowFooterInfo fi, GridRowInfo ri) {
            AddColumnsInfo(fi.RowHandle);
            base.CalcRowCellsFooterInfo(fi, ri);
            ChangeDisplayText(fi);
            RemoveColumnsInfo();
        }
        private void AddColumnsInfo(int rowHandle) {
            rowHandle = rowHandle >= 0 ? -1 : rowHandle;
            var groupSummary = View.GetGroupSummaryValues(rowHandle);
            var myGroupSummaryList = new List<DictionaryEntry>();
            foreach (DictionaryEntry item in groupSummary) {
                myGroupSummaryList.Add(item);
            }
            var query = from item in myGroupSummaryList
                        let summary = (GridGroupSummaryItem)item.Key
                        group item by summary.ShowInGroupColumnFooter;
            foreach (var group in query) {
                var myGroup = group.ToList();
                for (var i = 1; i < myGroup.Count; i++) {
                    var sourceCInfo = ColumnsInfo[group.Key];
                    if (sourceCInfo == null) {
                        continue;
                    }
                    AddNewColumnsInfo(myGroup, i, sourceCInfo);
                }
            }
        }
        private void AddNewColumnsInfo(List<DictionaryEntry> group, int i, GridColumnInfoArgs initCellInfo) {
            var cellInfo = new GridColumnInfoArgs(initCellInfo.Column);
            cellInfo.Assign(initCellInfo);
            cellInfo.Tag = group[i].Key as GridGroupSummaryItem;
            cellInfo.Info.StartRow = i;
            ColumnsInfo.Add(cellInfo);
        }
        private void RemoveColumnsInfo() {
            var count  = ColumnsInfo.OfType<GridColumnInfoArgs>().Where(ci => ci.Tag == null).Count() ;
            while (ColumnsInfo.Count > count) {
                ColumnsInfo.RemoveAt(count);
            }
        }
        private void ChangeDisplayText(GridRowFooterInfo fi) {
            foreach (GridFooterCellInfoArgs fci in fi.Cells) {
                var rowSummaryItem = View.GetRowSummaryItem(fi.RowHandle, fci.Column);
                if (rowSummaryItem.Key == null) {
                    continue;
                }
                var item = fci.ColumnInfo.Tag as GridGroupSummaryItem;
                if (item == null) {
                    fci.Visible = ((GridGroupSummaryItem)rowSummaryItem.Key).SummaryType == SummaryItemType.None ? false : true;
                    continue;
                }
                fci.Visible = item.SummaryType == SummaryItemType.None ? false : true;
                ChangeFooterCellDisplayText(fi, fci, item);
            }
        }
        private void ChangeFooterCellDisplayText(GridRowFooterInfo fi, GridFooterCellInfoArgs fci, GridGroupSummaryItem item) {
            var dEntry = GetCustomSummaryItemValue(fi.RowHandle, fci.Column, item);
            fci.Visible = item.SummaryType == SummaryItemType.None ? false : true;
            fci.Value = dEntry.Value;
            fci.DisplayText = ((GridSummaryItem)dEntry.Key).GetDisplayText(dEntry.Value, false);
        }
        public virtual DictionaryEntry GetCustomSummaryItemValue(int rowHandle, GridColumn column, object summaryItem) {
            if (column == null) {
                return new DictionaryEntry();
            }
            var summary = View.GetGroupSummaryValues(rowHandle);
            if (summary == null) {
                return new DictionaryEntry();
            }
            foreach (DictionaryEntry dEntry in summary) {
                var item = dEntry.Key as GridGroupSummaryItem;
                if (item.ShowInGroupColumnFooter != column) {
                    continue;
                }
                if (item != summaryItem) {
                    continue;
                }
                return dEntry;
            }
            return new DictionaryEntry();
        }
        protected override void UpdateRowFooterInfo(GridRowInfo ri, GridRowFooterInfo fi) {
            base.UpdateRowFooterInfo(ri, fi);
            ChangeDisplayText(fi);
        }
    }
}
