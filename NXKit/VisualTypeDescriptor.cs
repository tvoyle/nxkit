﻿using System.Xml.Linq;

namespace NXKit
{

    /// <summary>
    /// Descripts a <see cref="Visual"/> type and provides factory methods to initialize instances.
    /// </summary>
    public abstract class VisualTypeDescriptor
    {

        public abstract Visual CreateVisual(IEngine form, StructuralVisual parent, XNode node);

    }

}