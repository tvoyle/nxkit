﻿using System.Xml.Linq;

using NXKit.Util;

namespace NXKit.XForms
{

    public static class SupportsIncrementalAttributeExtensions
    {

        public static bool Incremental<T>(this T self)
            where T : NXElement, ISupportsIncrementalAttribute
        {
            var a = self.Document.Module<XFormsModule>().ResolveAttribute(self, "incremental");
            return a != null ? a.Value == "true" : false;
        }

    }

}
