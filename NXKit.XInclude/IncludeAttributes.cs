﻿using System;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace NXKit.XInclude
{

    public class IncludeAttributes :
        AttributeAccessor
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public IncludeAttributes(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="defaultNamespace"></param>
        public IncludeAttributes(XElement element, XNamespace defaultNamespace)
            : base(element, defaultNamespace)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

        public string Href
        {
            get { return GetAttributeValue("href"); }
        }

        public string Parse
        {
            get { return GetAttributeValue("parse"); }
        }

        public string XPointer
        {
            get { return GetAttributeValue("xpointer"); }
        }

        public string Encoding
        {
            get { return GetAttributeValue("encoding"); }
        }

        public string Accept
        {
            get { return GetAttributeValue("accept"); }
        }

        public string AcceptLanguage
        {
            get { return GetAttributeValue("accept-language"); }
        }

    }

}
