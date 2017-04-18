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
            IDeviceControl CTR;

            if (device is ILightable)
            {
                CTR = new ILightableControl(device);

                baseCtr = new DeviceControl(device, devices, CTR, Manager);
            }

           else  if (device is ITemperature)
            {
                CTR = new ITemperatureControl(device);

                baseCtr = new DeviceControl(device, devices, CTR, Manager);
            }

           else if ((device is IVoluable) && (device is IChannelable))
            {
                IVoluableControl CTR_1 = new IVoluableControl(device);
                IChannelableControl CTR_2 =new IChannelableControl(channels,device);

                CTR = new ITVControl(CTR_1, CTR_2);

                baseCtr = new DeviceControl(device, devices, CTR, Manager);
            }

            else
            {
                baseCtr = new DeviceControl();
            }

            return baseCtr;
        }

    }
}