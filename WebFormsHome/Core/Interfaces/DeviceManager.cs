using HeyHomeModel.Model.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormsHome.Core.Interfaces
{
   public interface DeviceManager
    {
        void SaveDevice(Device device);
        void SaveDevices(List<Device> devices);
        void SaveChannel(Channel channel);
        void SaveChannels(List<Channel> channel);

        List<Channel> GetChannelList();
        List<Device> GetDevicesList();
    }
}
