﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

using NXKit.Composition;

namespace NXKit.XForms
{

    /// <summary>
    /// Provides the XForms 'load' properties.
    /// </summary>
    [Extension("{http://www.w3.org/2002/xforms}load")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    public class LoadProperties :
        ElementExtension
    {

        readonly Extension<LoadAttributes> attributes;
        readonly Extension<EvaluationContextResolver> context;
        readonly Lazy<LoadShow> show;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributes"></param>
        /// <param name="context"></param>
        [ImportingConstructor]
        public LoadProperties(
            XElement element,
            Extension<LoadAttributes> attributes,
            Extension<EvaluationContextResolver> context)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Requires<ArgumentNullException>(attributes != null);
            Contract.Requires<ArgumentNullException>(context != null);

            this.attributes = attributes;
            this.context = context;

            this.show = new Lazy<LoadShow>(() =>
                !string.IsNullOrEmpty(attributes.Value.Show) ? (LoadShow)Enum.Parse(typeof(LoadShow), attributes.Value.Show, true) : LoadShow.Replace);
        }

        public string Resource
        {
            get { return attributes.Value.Resource; }
        }

        public LoadShow Show
        {
            get { return show.Value; }
        }

        public IdRef TargetId
        {
            get { return attributes.Value.TargetId; }
        }

    }

}