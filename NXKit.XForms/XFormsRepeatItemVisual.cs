﻿using System.Xml.Linq;


namespace NXKit.XForms
{

    public class XFormsRepeatItemVisual : XFormsVisual, IEvaluationContextScope, INamingScope, IRelevancyScope
    {

        public override string Id
        {
            get { return "NODE" + Module.GetModelItemId(Context, Context.Node); }
        }

        /// <summary>
        /// Obtains the evaluation context for this visual.
        /// </summary>
        public XFormsEvaluationContext Context { get; private set; }

        /// <summary>
        /// Sets the context to a new value, should only be used by the repeat container.
        /// </summary>
        /// <param name="ec"></param>
        internal void SetContext(XFormsEvaluationContext ec)
        {
            Context = ec;

            // ensure module item id is initialized
            Module.GetModelItemId(Context, Context.Node);
        }

        public bool Relevant
        {
            get { return Module.GetModelItemRelevant(Context.Node); }
        }

    }

}
