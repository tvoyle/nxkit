﻿using System;
using System.Collections.Generic;

using XEngine.Util;

namespace XEngine.Forms
{

    public class VisualAnnotationCollection
    {

        private Dictionary<Type, VisualAnnotation> annotations =
            new Dictionary<Type, VisualAnnotation>();

        /// <summary>
        /// Gets an annotation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
            where T : VisualAnnotation
        {
            return (T)annotations.ValueOrDefault(typeof(T));
        }

        /// <summary>
        /// Sets an annotation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="annotation"></param>
        public void Set<T>(T annotation)
            where T : VisualAnnotation
        {
            annotations[typeof(T)] = annotation;
        }

    }

}
