﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

using NXKit.Composition;

namespace NXKit.XForms.Layout
{

    [Extension("{http://schemas.nxkit.org/2014/xforms-layout}table-cell")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    public class TableCell :
        ElementExtension
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        [ImportingConstructor]
        public TableCell(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

    }

}
