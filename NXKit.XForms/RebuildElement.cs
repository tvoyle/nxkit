﻿using System.Xml.Linq;

using NXKit.DOMEvents;

namespace NXKit.XForms
{

    [Element("rebuild")]
    public class RebuildElement :
        XFormsElement,
        IAction
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="xml"></param>
        public RebuildElement(XElement xml)
            : base(xml)
        {

        }

        public void Handle(Event ev)
        {
            Invoke();
        }

        public void Invoke()
        {
            var modelAttr = Module.GetAttributeValue(this, "model");
            if (modelAttr != null)
            {
                var modelVisual = (ModelElement)ResolveId(modelAttr);
                if (modelVisual != null)
                    modelVisual.OnRebuild();
                else
                {
                    this.Interface<INXEventTarget>().DispatchEvent(Events.BindingException);
                    return;
                }
            }
        }

    }

}
