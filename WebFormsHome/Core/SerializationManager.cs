using HeyHomeModel.Model.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using WebFormsHome.Core.Interfaces;
using System.Web.Hosting;

namespace WebFormsHome.Core
{
    public class SerializationManager : DeviceManager
    {
        public SerializationManager()
        {

        }

        public void SaveChannel(Channel channel)
        { 
            string json = JsonConvert.SerializeObject(channel);
            File.AppendAllText(@"chanels.txt", json);
        }

        public void SaveChannels(List<Channel> channels)
        {
            string json = JsonConvert.SerializeObject(channels);
            File.WriteAllText(HostingEnvironment.MapPath("~/lib/chanels.json"), json);
        }

        public void SaveDevices(List<Device> devices)
        {
            string json = JsonConvert.SerializeObject(devices, 
                Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            File.WriteAllText(HostingEnvironment.MapPath("~/lib/devices.json"), json);
        }

        public void SaveDevice(Device device)
        {
            List<Device> devices = GetDevicesList();
            Device dev = devices.Where(d => d.Id == device.Id).FirstOrDefault();
            devices.Remove(dev);
            devices.Add(device);

            string json = JsonConvert.SerializeObject(devices,
                Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            
            File.WriteAllText(HostingEnvironment.MapPath("~/lib/devices.json"), json);
        }

        //Desirialize

        public List<Channel> GetChannelList()
        {
           // string json = File.ReadAllText(@"F:\dotNet\Oracle\Projects\HeyHomeModel\WebFormsHome\lib\chanels.json");
            string json = File.ReadAllText(HostingEnvironment.MapPath("~/lib/chanels.json") ); 

            List<Channel> channels = JsonConvert.DeserializeObject<List<Channel>>(json);

            return channels;
        }

        public List<Device> GetDevicesList()
        {
            string json = File.ReadAllText(HostingEnvironment.MapPath("~/lib/devices.json"));

            List<Device> devices = JsonConvert.DeserializeObject<List<Device>>(json,
                new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return devices;
        }
    }
}