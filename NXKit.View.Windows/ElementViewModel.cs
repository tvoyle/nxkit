﻿using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace NXKit.View.Windows
{

    /// <summary>
    /// Base View-Model type that can be generated by the <see cref="ElementViewModelControl"/>.
    /// </summary>
    public abstract class ElementViewModel :
        INotifyPropertyChanged
    {

        readonly XElement element;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public ElementViewModel(XElement element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            this.element = element;
        }

        /// <summary>
        /// Gets the <see cref="XElement"/>.
        /// </summary>
        public XElement Element
        {
            get { return element; }
        }

        /// <summary>
        /// Raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="args"></param>
        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, args);
        }

        /// <summary>
        /// Raises the PropertyChanged event for a given property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

    }

}
