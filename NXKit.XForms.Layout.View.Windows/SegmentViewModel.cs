﻿using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Linq;

using NXKit.View.Windows;

namespace NXKit.XForms.Layout.View.Windows
{

    public class SegmentViewModel :
        ElementViewModel
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public SegmentViewModel(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

        public XElement Label
        {
            get { return GetLabel(); }
        }

        XElement GetLabel()
        {
            return Element.Elements("{http://www.w3.org/2002/xforms}label").FirstOrDefault();
        }

    }

}
