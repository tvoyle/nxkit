﻿using System.Xml.Xsl;

namespace NXKit.XPath
{

    /// <summary>
    /// Describes an exported <see cref="IXsltContextFunction"/>.
    /// </summary>
    public interface IXsltContextFunctionMetadata
    {

        /// <summary>
        /// Gets the fully qualified name of the function.
        /// </summary>
        string[] ExpandedName { get; }

        /// <summary>
        /// Gets whether or not the function requires a prefix.
        /// </summary>
        bool[] IsPrefixRequired { get; }

    }

}
