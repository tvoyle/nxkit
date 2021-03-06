﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;

namespace NXKit.Scripting
{

    /// <summary>
    /// Exports the given object as a script object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    [MetadataAttribute]
    public class ScriptObjectAttribute :
        ExportAttribute
    {

        readonly string name;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name"></param>
        public ScriptObjectAttribute(string name)
            : base(typeof(IScriptObject))
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.name = name;
        }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

    }

}
