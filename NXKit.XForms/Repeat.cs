﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Linq;
using NXKit.Xml;


namespace NXKit.XForms
{

    [Interface("{http://www.w3.org/2002/xforms}repeat")]
    public class Repeat :
        ElementExtension,
        IOnInit,
        IOnRefresh
    {

        readonly RepeatAttributes attributes;
        readonly Lazy<IBindingNode> bindingNode;
        readonly Lazy<Binding> binding;
        readonly Lazy<IUIBindingNode> uiBindingNode;
        readonly Lazy<UIBinding> uiBinding;
        readonly Lazy<RepeatState> state;
        readonly Lazy<XElement> template;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public Repeat(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            this.attributes = new RepeatAttributes(Element);
            this.bindingNode = new Lazy<IBindingNode>(() => Element.Interface<IBindingNode>());
            this.binding = new Lazy<Binding>(() => bindingNode.Value.Binding);
            this.uiBindingNode = new Lazy<IUIBindingNode>(() => Element.Interface<IUIBindingNode>());
            this.uiBinding = new Lazy<UIBinding>(() => uiBindingNode.Value.UIBinding);
            this.state = new Lazy<RepeatState>(() => Element.AnnotationOrCreate<RepeatState>());
            this.template = new Lazy<XElement>(() => State.Template);
        }

        RepeatState State
        {
            get { return state.Value; }
        }

        UIBinding UIBinding
        {
            get { return uiBinding.Value; }
        }

        Binding Binding
        {
            get { return binding.Value; }
        }

        /// <summary>
        /// Gets or sets the current repeat index.
        /// </summary>
        public int Index
        {
            get { return State.Index; }
            set { State.Index = value; }
        }

        /// <summary>
        /// Gets or sets the persisted template.
        /// </summary>
        XElement Template
        {
            get { return State.Template; }
            set { State.Template = value; }
        }

        void Initialize()
        {
            // acquire template
            Template = new XElement(
                Constants.XForms_1_0 + "template",
                Element.GetNamespacePrefixAttributes(),
                Element.Nodes());
            Element.RemoveNodes();
        }

        /// <summary>
        /// Dynamically generate repeat items, reusing existing instances if available.
        /// </summary>
        /// <returns></returns>
        void RefreshNodes()
        {
            // store current index item
            var lastIndexItem = Element
                .Nodes()
                .FirstOrDefault(i => i.AnnotationOrCreate<RepeatItemState>().Index == Index);

            // build new list of properly ordered nodes
            var items = new LinkedList<XElement>();
            if (Binding != null &&
                Binding.ModelItems.Length > 0)
                for (int index = 1; index <= Binding.ModelItems.Length; index++)
                {
                    var modelItem = Binding.ModelItems[index - 1];
                    if (modelItem == null)
                        continue;

                    // get existing item or create new
                    var node = Element.Elements()
                        .FirstOrDefault(i => i.AnnotationOrCreate<RepeatItemState>().ModelItemId == modelItem.Xml.GetObjectId());
                    if (node == null)
                        node = new XElement(
                            Constants.XForms_1_0 + "group",
                            Template.GetNamespacePrefixAttributes(),
                            Template.Nodes());

                    // configure item state
                    var anno = node.AnnotationOrCreate<RepeatItemState>();
                    var swap = anno.Index != index;
                    anno.Index = index;
                    anno.ModelItemId = modelItem.Xml.GetObjectId();
                    items.AddLast(node);
                }

            // items which have been added
            var additions = items
                .Except(Element.Nodes())
                .OfType<XElement>()
                .ToList();

            // replace the element's content
            Element.RemoveNodes(); // remove first to prevent cloning
            Element.Add(items);

            // model-construct-done sequence applied to new children
            foreach (var addition in additions)
            {
                // refresh bindings
                foreach (var i in GetAllExtensions<IOnRefresh>(addition))
                    i.RefreshBinding();

                // discard refresh events
                foreach (var i in GetAllExtensions<IOnRefresh>(addition))
                    i.DiscardEvents();

                // final refresh
                foreach (var i in GetAllExtensions<IOnRefresh>(addition))
                    i.Refresh();
            }

            // restore or reset index
            if (lastIndexItem != null &&
                lastIndexItem.Parent != null)
                Index = lastIndexItem.AnnotationOrCreate<RepeatItemState>().Index;
            else if (items.Count > 0)
                Index = 1;
            else
                Index = 0;
        }

        /// <summary>
        /// Gets all implementations of the given extension type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllExtensions<T>(XElement root)
        {
            Contract.Requires<ArgumentNullException>(root != null);

            return root
                .DescendantNodesAndSelf()
                .SelectMany(i => i.Interfaces<T>());
        }

        /// <summary>
        /// Refreshes the interface of this element.
        /// </summary>
        void Refresh()
        {
            // ensure index value is within range
            if (Index < 0)
                if (Binding == null ||
                    Binding.ModelItems == null ||
                    Binding.ModelItems.Length == 0)
                    Index = 0;
                else
                    Index = attributes.StartIndex;

            if (Binding != null &&
                Binding.ModelItems != null)
                if (Index > Binding.ModelItems.Length)
                    Index = Binding.ModelItems.Length;

            // rebuild node tree
            RefreshNodes();
        }

        /// <summary>
        /// Gets the <see cref="EvaluationContext"/> for a specific item.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        internal EvaluationContext GetItemContext(XElement element)
        {
            var index = element.Interface<RepeatItem>().Index;
            if (index <= 0)
                throw new InvalidOperationException();

            if (Binding == null ||
                Binding.ModelItems == null ||
                Binding.ModelItems.Length < index)
                return null;

            return new EvaluationContext(
                Binding.ModelItems[index - 1].Model,
                Binding.ModelItems[index - 1].Instance,
                Binding.ModelItems[index - 1],
                index,
                Binding.ModelItems.Length);
        }

        void IOnInit.Init()
        {
            Initialize();
        }

        void IOnRefresh.RefreshBinding()
        {

        }

        void IOnRefresh.Refresh()
        {
            Refresh();
        }

        void IOnRefresh.DispatchEvents()
        {

        }

        void IOnRefresh.DiscardEvents()
        {

        }


    }

}
