using DevExpress.XtraGrid.Views.Grid.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace Q354185.MyXtraGrid {
    public class MyGridHandler : GridHandler {
        public MyGridHandler(GridView gridView)
            : base(gridView) {
        }
        protected override DevExpress.XtraGrid.Menu.GridViewFooterMenu CreateGridViewFooterMenu(GridView gridView) {
            return new MyGridFooterMenu(gridView);
        }
    }
}
