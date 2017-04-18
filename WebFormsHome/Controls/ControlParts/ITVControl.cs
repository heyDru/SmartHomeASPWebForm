using HeyHomeModel.Model.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebFormsHome.Controls.Interfaces;

namespace WebFormsHome.Controls.ControlParts
{
    public class ITVControl : Panel, IDeviceControl
    {
        public IVoluableControl VoluableControl;
        public IChannelableControl ChannelControl;
        Device device;

        public ITVControl(IVoluableControl control_1, IChannelableControl control_2)
        {
            VoluableControl = control_1;
            ChannelControl = control_2;
            Controls.Add(control_1.Initializer());
            Controls.Add(control_2.Initializer());
        }

        public Panel Initializer()
        {
            return this;
        }
    }
}