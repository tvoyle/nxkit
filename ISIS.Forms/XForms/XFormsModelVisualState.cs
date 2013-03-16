﻿using System;

namespace ISIS.Forms.XForms
{

    [Serializable]
    public class XFormsModelVisualState
    {

        public bool Construct { get; set; }

        public bool ConstructDone { get; set; }

        public bool ConstructDoneOnce { get; set; }

        public bool Ready { get; set; }

        public bool RebuildFlag { get; set; }

        public bool RecalculateFlag { get; set; }

        public bool RevalidateFlag { get; set; }

        public bool RefreshFlag { get; set; }
    }

}
