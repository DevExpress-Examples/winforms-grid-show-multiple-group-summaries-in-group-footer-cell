using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Grid;

namespace Q354185.MyXtraGrid {
    public class MyGridInfoRegistrator : GridInfoRegistrator {
        public override BaseView CreateView(GridControl grid) {
            return new MyGridView(grid as GridControl);
        }
        public override DevExpress.XtraGrid.Views.Base.ViewInfo.BaseViewInfo CreateViewInfo(BaseView view) {
            return new MyGridViewInfo((GridView)view);
        }
        public override string ViewName {
            get {
                return "MyGridView";
            }
        }
        public override DevExpress.XtraGrid.Views.Base.Handler.BaseViewHandler CreateHandler(BaseView view) {
            return new MyGridHandler(view as MyGridView);
        }
    }
}
