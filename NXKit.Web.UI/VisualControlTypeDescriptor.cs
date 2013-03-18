﻿namespace NXKit.Web.UI
{

    public abstract class VisualControlTypeDescriptor
    {

        /// <summary>
        /// Returns <c>true</c> if the 
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public virtual bool CanHandleVisual(Visual visual)
        {
            return false;
        }

        /// <summary>
        /// Returns <c>true</c> if the visual should be considered opaque.
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public virtual bool IsOpaque(Visual visual)
        {
            return true;
        }

        /// <summary>
        /// Returns <c>true</c> if the visual should be rendered as content.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsContent(Visual visual)
        {
            return false;
        }

        /// <summary>
        /// Creates an instance of the associated visual control.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="visual"></param>
        /// <returns></returns>
        public virtual VisualControl CreateControl(View view, Visual visual)
        {
            return null;
        }

    }

}
