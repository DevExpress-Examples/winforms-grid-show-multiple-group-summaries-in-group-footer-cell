using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;

namespace Q354185.MyXtraGrid {
    public class MyGridView : GridView {
        public MyGridView(GridControl ownerGrid)
            : base(ownerGrid) {
        }

        public MyGridView() {
        }

        protected override string ViewName {
            get {
                return "MyGridView";
            }
        }
        public int GetGroupFooterRowCount() {
            if (IsDesignMode) {
                return 0;
            }
            var footerRowCount = GetFooterRowCount();
            if (footerRowCount.Count == 0) {
                return 0;
            }
            return footerRowCount.Max(kvp => kvp.Value);
        }
        private Dictionary<GridColumn, int> GetFooterRowCount() {
            var columnFooterRowCount = new Dictionary<GridColumn, int>();
            foreach (GridGroupSummaryItem summary in GroupSummary) {
                var col = summary.ShowInGroupColumnFooter;
                if (col == null) {
                    continue;
                }
                if (columnFooterRowCount.ContainsKey(col)) {
                    columnFooterRowCount[col]++;
                }
                if (!(columnFooterRowCount.ContainsKey(col))) {
                    columnFooterRowCount.Add(col, 1);
                }
            }
            return columnFooterRowCount;
        }
        protected override List<SummaryItem> SynchronizeSummary(System.Collections.IList gridSum, SummaryItemCollection summary)
        {
            var gridSumCollection = gridSum as GridGroupSummaryItemCollection;
            if (gridSumCollection == null) {
                return base.SynchronizeSummary(gridSum, summary);
                
            }
            var myGridSumCollection = new List<GridGroupSummaryItem>();
            var groupFooterRowCount = GetGroupFooterRowCount();
            MyGridSumCollectionAddSummary(gridSumCollection, myGridSumCollection);
            var query = GetGroups(myGridSumCollection);
            var tempSummaryItemList = new List<GridGroupSummaryItem>();
            FillTempSummaryItemList(groupFooterRowCount, query, tempSummaryItemList);
            GridSumCollectionAddSummaries(gridSumCollection, tempSummaryItemList);
            return base.SynchronizeSummary(gridSum, summary);
        }
        private void MyGridSumCollectionAddSummary(GridGroupSummaryItemCollection gridSumCollection, List<GridGroupSummaryItem> myGridSumCollection) {
            foreach (GridGroupSummaryItem summaryItem in gridSumCollection) {
                myGridSumCollection.Add(summaryItem);
            }
        }
        private IEnumerable<IGrouping<GridColumn, GridGroupSummaryItem>> GetGroups(List<GridGroupSummaryItem> gridSumCollection) {
            var groups = from item in gridSumCollection
                         group item by item.ShowInGroupColumnFooter;
            return groups;
        }
        private void FillTempSummaryItemList(int groupFooterRowCount, IEnumerable<IGrouping<GridColumn, GridGroupSummaryItem>> query, List<GridGroupSummaryItem> tempSummaryItemList) {
            foreach (var group in query) {
                GridGroupSummaryItem summaryItem = null;
                GridColumn showInGroupColumnFooter = null;
                var fieldName = string.Empty;
                var summaryItemList = group.ToList();
                for (var i = 0; i < groupFooterRowCount; i++) {
                    try {
                        summaryItem = summaryItemList[i];
                    } catch {
                        summaryItem = null;
                    }
                    if (summaryItem != null) {
                        fieldName = summaryItem.FieldName;
                        showInGroupColumnFooter = summaryItem.ShowInGroupColumnFooter;
                        continue;
                    }
                    if (fieldName != string.Empty && showInGroupColumnFooter != null) {
                        summaryItem = new GridGroupSummaryItem() { SummaryType = SummaryItemType.None, ShowInGroupColumnFooter = showInGroupColumnFooter, FieldName = fieldName };
                        tempSummaryItemList.Add(summaryItem);
                    }
                }
            }
            AddEmptySummaryColumn(groupFooterRowCount, tempSummaryItemList);
        }
        private void GridSumCollectionAddSummaries(GridGroupSummaryItemCollection gridSumCollection, List<GridGroupSummaryItem> tempSummaryItemList) {
            if (tempSummaryItemList.Count > 0) {
                gridSumCollection.AddRange(tempSummaryItemList.ToArray());
            }
        }
        private void AddEmptySummaryColumn(int groupFooterRowCount, List<GridGroupSummaryItem> tempSummaryItemList) {
            var footerRowCount = GetFooterRowCount();
            foreach (GridColumn col in Columns) {
                if (footerRowCount.ContainsKey(col)) {
                    continue;
                }
                CreateEmptySummaries(col, tempSummaryItemList, groupFooterRowCount);
            }
        }
        private void CreateEmptySummaries(GridColumn column, List<GridGroupSummaryItem> tempSummaryItemList, int groupFooterRowCount) {
            GridGroupSummaryItem summaryItem = null;
            GridColumn showInGroupColumnFooter = null;
            var fieldName = string.Empty;
            for (var i = 0; i < groupFooterRowCount - 1; i++) {
                showInGroupColumnFooter = column;
                fieldName = column.FieldName;
                summaryItem = new GridGroupSummaryItem() { SummaryType = SummaryItemType.None, ShowInGroupColumnFooter = showInGroupColumnFooter, FieldName = fieldName };
                tempSummaryItemList.Add(summaryItem);
            }
        }
    }
}
