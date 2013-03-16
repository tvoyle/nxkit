﻿using System;
using System.Collections.Generic;

using ISIS.Util;

namespace ISIS.Forms
{

    [Serializable]
    public class VisualStateCollection
    {

        internal Dictionary<string, Dictionary<Type, object>> store;

        /// <summary>
        /// Gets the state object of the specified type for the specified <see cref="Visual"/>.
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object Get(StructuralVisual visual, Type type, Func<object> getDefaultValue)
        {
            if (store == null)
                store = new Dictionary<string, Dictionary<Type, object>>();

            var visualStore = store.ValueOrDefault(visual.UniqueId);
            if (visualStore == null)
                visualStore = store[visual.UniqueId] = new Dictionary<Type, object>();

            var value = visualStore.ValueOrDefault(type);
            if (value == null)
                value = visualStore[type] = getDefaultValue();

            return value;
        }

        /// <summary>
        /// Gets the state object of type <typeparamref name="T"/> for <paramref name="visual"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="visual"></param>
        /// <returns></returns>
        public T Get<T>(StructuralVisual visual)
            where T : new()
        {
            return (T)Get(visual, typeof(T), () => new T());
        }

    }

}
