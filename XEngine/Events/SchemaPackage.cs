﻿using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

using NXKit;

namespace NXKit.Events
{

    [SchemaPackage]
    public sealed class SchemaPackage : NXKit.SchemaPackage
    {

        public override IEnumerable<XNamespace> Namespaces
        {
            get
            {
                yield return Constants.Events_1_0;
            }
        }

        public override string ResolveSchema(XNamespace ns)
        {
            if (ns == Constants.Events_1_0)
                return Constants.Events_1_0_HREF;
            else
                return null;
        }

        public override Stream OpenSchema(string location)
        {
            if (location == Constants.Events_1_0_HREF)
                return typeof(EventsModule).Assembly.GetManifestResourceStream("ISIS.Forms.Events.xml-events-attribs-1.xsd");
            else
                return null;
        }

    }

}
