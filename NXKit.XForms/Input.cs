﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

using NXKit.Composition;
using NXKit.DOMEvents;

namespace NXKit.XForms
{

    /// <summary>
    /// This form control enables free-form data entry or a user interface component appropriate to the datatype of the
    /// bound node.
    /// </summary>
    [Extension("{http://www.w3.org/2002/xforms}input")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    public class Input :
        ElementExtension,
        IEventDefaultAction
    {

        readonly Extension<EventTarget> eventTarget;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="eventTarget"></param>
        [ImportingConstructor]
        public Input(
            XElement element, 
            Extension<EventTarget> eventTarget)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Requires<ArgumentNullException>(eventTarget != null);

            this.eventTarget = eventTarget;
        }

        void IEventDefaultAction.DefaultAction(Event evt)
        {
            if (evt.Type == DOMEvents.Events.DOMFocusIn)
                eventTarget.Value.Dispatch(Events.Focus);
        }

    }

}
