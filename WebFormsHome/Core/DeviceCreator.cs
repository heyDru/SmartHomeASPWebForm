using HeyHomeModel.Model.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFormsHome.Core.Interfaces;
using WebFormsHome.Utilities;
using WebFormsHome.Utilities.Interfaces;

namespace WebFormsHome.Core
{
    public class DeviceCreator
    {
        IIdGeneratable idGenerator;
        DeviceManager manager;
        List<Device> devices;

        public DeviceCreator()
        {
        }

        public DeviceCreator(DeviceManager manager, List<Device> deviceList)
        {
            idGenerator = new RandomIdGenerator();
            this.manager = manager;
            devices = deviceList;
        }

        public Device CreateDevice(string type, double consumption)
        {
            Device device;

            if (type == "LAMP")
            {
                device = new Lamp(type);
                device.Id = idGenerator.GenerateId();
                device.Consumption = consumption;
                devices.Add(device);
                manager.SaveDevice(device);
            }

            else if (type == "HEATER")
            {
                device = new Heater(type);
                device.Id = idGenerator.GenerateId();
                device.Consumption = consumption;
                devices.Add(device);
                manager.SaveDevice(device);
            }

            else if (type == "TV")
            {
                device = new TV(type);
                TV tv = (TV)device;
                device.Id = idGenerator.GenerateId();
                device.Consumption = consumption;
                List<Channel> channels = manager.GetChannelList();
                tv.ChannelList = channels;
                tv.SetDefaultCurrentChannel();
                devices.Add(device);
                manager.SaveDevice(device);
            }

            else
            {
                device = new Lamp(type);
                device.Id = idGenerator.GenerateId();
                device.Consumption = consumption;
            }

            return device;
        }
    }
}