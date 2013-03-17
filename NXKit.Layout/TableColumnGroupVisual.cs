﻿using System.Xml.Linq;

using NXKit.Util;

namespace NXKit.Layout
{

    [VisualTypeDescriptor(Constants.Layout_1_0_NS, "table-column-group")]
    public class TableColumnGroupVisualTypeDescriptor : VisualTypeDescriptor
    {

        public override Visual CreateVisual(IEngine form, StructuralVisual parent, XNode node)
        {
            return new TableColumnGroupVisual(parent, (XElement)node);
        }

    }

    public class TableColumnGroupVisual : LayoutVisual
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="element"></param>
        public TableColumnGroupVisual(StructuralVisual parent, XElement element)
            : base(parent, element)
        {

        }

        public string Name
        {
            get { return Module.GetAttributeValue(Element, "name").TrimToNull(); }
        }

    }

}