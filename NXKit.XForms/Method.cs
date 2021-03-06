﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

using NXKit.Composition;
using NXKit.XForms.IO;

namespace NXKit.XForms
{

    /// <summary>
    /// The method element (deprecated in favor of using an AVT in the method attribute) allows the submission method
    /// to be dynamically calculated based on instance data.
    /// </summary>
    [Extension("{http://www.w3.org/2002/xforms}method")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    public class Method :
        ElementExtension
    {

        readonly MethodAttributes attributes;
        readonly Extension<IBindingNode> bindingNode;
        readonly Lazy<Binding> valueBinding;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributes"></param>
        /// <param name="bindingNode"></param>
        [ImportingConstructor]
        public Method(
            XElement element,
            MethodAttributes attributes,
            Extension<IBindingNode> bindingNode)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Requires<ArgumentNullException>(attributes != null);
            Contract.Requires<ArgumentNullException>(bindingNode != null);

            this.attributes = attributes;
            this.bindingNode = bindingNode;
            this.valueBinding = new Lazy<Binding>(() => BindingUtil.ForAttribute(attributes.ValueAttribute));
        }

        Binding Binding
        {
            get { return bindingNode.Value != null ? bindingNode.Value.Binding : null; }
        }

        Binding ValueBinding
        {
            get { return valueBinding.Value; }
        }

        /// <summary>
        /// Gets the appropriate value to use when selecting the item.
        /// </summary>
        /// <returns></returns>
        string GetValue()
        {
            if (Binding != null)
                return Binding.Value;

            if (ValueBinding != null)
                return ValueBinding.Value;

            return Element.Value;
        }

        public ModelMethod RequestMethod
        {
            get { return GetValue(); }
        }

    }

}
