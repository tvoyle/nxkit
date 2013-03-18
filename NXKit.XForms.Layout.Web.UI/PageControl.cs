﻿using System.Web.UI;

using NXKit.Web.UI;

namespace NXKit.XForms.Layout.Web.UI
{

    [VisualControlTypeDescriptor]
    public class PageControlDescriptor : VisualControlTypeDescriptor
    {

        public override bool CanHandleVisual(Visual visual)
        {
            return visual is PageVisual;
        }

        public override bool IsContent(Visual visual)
        {
            return true;
        }

        public override VisualControl CreateControl(View view, Visual visual)
        {
            return new PageControl(view, (PageVisual)visual);
        }

    }

    public class PageControl : VisualContentControl<PageVisual>
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="visual"></param>
        public PageControl(View view, PageVisual visual)
            : base(view, visual)
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "Layout_Page");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }

    }

}
