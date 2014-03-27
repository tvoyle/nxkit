﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace NXKit.XPath
{

    /// <summary>
    /// Marks a function as a function.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [MetadataAttribute]
    public class XsltContextFunctionAttribute :
        ExportAttribute
    {

        readonly string expandedName;
        bool isPrefixRequired;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name"></param>
        public XsltContextFunctionAttribute(XName name)
            : base(typeof(IXsltContextFunction))
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.expandedName = name.ToString();
            this.isPrefixRequired = true;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="expandedName"></param>
        public XsltContextFunctionAttribute(string expandedName)
            : this(XName.Get(expandedName))
        {
            Contract.Requires<ArgumentNullException>(expandedName != null);
        }

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public string ExpandedName
        {
            get { return expandedName; }
        }

        /// <summary>
        /// Gets or sets whether the function requires a prefix.
        /// </summary>
        public bool IsPrefixRequired
        {
            get { return isPrefixRequired; }
            set { isPrefixRequired = value; }
        }

    }

}
