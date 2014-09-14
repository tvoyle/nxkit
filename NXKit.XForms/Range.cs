﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

using NXKit.Composition;

namespace NXKit.XForms
{

    [Extension("{http://www.w3.org/2002/xforms}range")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    [Remote]
    public class Range :
        ElementExtension
    {

        readonly Extension<RangeAttributes> attributes;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributes"></param>
        [ImportingConstructor]
        public Range(
            XElement element,
            Extension<RangeAttributes> attributes)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Requires<ArgumentNullException>(attributes != null);

            this.attributes = attributes;
        }

        [Remote]
        public int? Start
        {
            get { return attributes.Value.Start; }
        }

        [Remote]
        public int? End
        {
            get { return attributes.Value.End; }
        }

        [Remote]
        public int? Step
        {
            get { return attributes.Value.Step; }
        }

    }

}
