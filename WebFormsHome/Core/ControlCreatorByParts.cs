using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsHome.Controls.ControlParts;
using WebFormsHome.Controls.Interfaces;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Core
{
    public class ControlCreatorByParts : Panel
    {
        public DeviceManager Manager { get; set; }

        public ControlCreatorByParts(DeviceManager manager)
        {
            Manager = manager;
        }

        public Control CreateControl(Device device, List<Device> devicesList)
        {
            List<Channel> channels = Manager.GetChannelList();
            List<Device> devices = devicesList;
            DeviceControl baseCtr ;
            IDeviceControl deviceControl;

            if (device is ILightable)
            {
                deviceControl = new ILightableControl(device);

                baseCtr = new DeviceControl(device, devices, deviceControl, Manager);
            }

           else  if (device is ITemperature)
            {
                deviceControl = new ITemperatureControl(device);

                baseCtr = new DeviceControl(device, devices, deviceControl, Manager);
            }

           else if ((device is IVoluable) && (device is IChannelable))
            {
                IVoluableControl voluableControl = new IVoluableControl(device);
                IChannelableControl channelableControl = new IChannelableControl(channels,device);

                deviceControl = new ITVControl(voluableControl, channelableControl);

                baseCtr = new DeviceControl(device, devices, deviceControl, Manager);
            }

            else
            {
                baseCtr = new DeviceControl();
            }

            return baseCtr;
        }

    }
}