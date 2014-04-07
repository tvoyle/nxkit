﻿using System;
using System.Web.UI;

using NXKit.Web.UI;

namespace NXKit.Test.Web.Site
{

    public partial class Default :
        Page,
        IPostBackEventHandler
    {

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);
        }

        protected void View_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                UriTextBox.Text = new Uri(Request.Url, "../Resources/form.xml").ToString();
                View.Configure(UriTextBox.Text);
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == "Load")
                View.Configure(UriTextBox.Text);
        }

    }

}