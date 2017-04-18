using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsHome.Controls;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Core
{
    public class ControlCreator : Panel
    {
        public DeviceManager Manager { get; set; }

        public ControlCreator(DeviceManager manager)
        {
            Manager = manager;
        }
        public Control CreateControl(Device device, List<Device> devicesList)
        {
            List<Channel> channels = Manager.GetChannelList();
            List<Device> devices = devicesList;
            Control CTR = new Control();

            if (device is ILightable)
            {
                CTR = new ILightableControl(device, devicesList, Manager);
            }

            if (device is ITemperature)
            {
                CTR  = new ITemperatureControl(device,devicesList, Manager);
            }

            if (device is ITV)
            {
                CTR = new TVControl(device, devicesList, channels, Manager);
            }

            return CTR;
        }

    }
}