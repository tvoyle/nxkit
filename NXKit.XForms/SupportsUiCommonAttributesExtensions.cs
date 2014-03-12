﻿using System.Xml.Linq;

using NXKit.Util;

namespace NXKit.XForms
{

    public static class SupportsUiCommonAttributesExtensions
    {

        /// <summary>
        /// Author-optional attribute to define an appearance hint. If absent, the user agent may freely choose any suitable rendering.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static XName Appearance<T>(this T self)
            where T : StructuralVisual, ISupportsUiCommonAttributes
        {
            var a = self.Document.GetModule<XFormsModule>().ResolveAttribute(self.Element, "appearance");
            return a != null ? a.ValueAsXName() : null;
        }

    }

}