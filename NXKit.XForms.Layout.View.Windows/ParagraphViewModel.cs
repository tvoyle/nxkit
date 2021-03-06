﻿using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;

using NXKit.View.Windows;

namespace NXKit.XForms.Layout.View.Windows
{

    public class ParagraphViewModel :
        ElementViewModel
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public ParagraphViewModel(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

    }

}
