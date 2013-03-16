﻿using System.Collections.Generic;
using System.Linq;

using XEngine.Util;

namespace XEngine.Forms.Web.UI.Layout
{

    public class TableControlCellControlCollection : VisualControlCollection
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="visual"></param>
        public TableControlCellControlCollection(FormView view, TableControl parent)
            : base(view, parent.Visual)
        {
            Table = parent.Model;
        }

        private Table Table { get; set; }

        protected override IEnumerable<Visual> GetVisuals()
        {
            // manages all children cells of a table
            return Table.Rows
                .Recurse(i => i.Rows)
                .SelectMany(i => i.Cells)
                .Select(i => i.Visual)
                .DistinctByReference();
        }

    }

}
