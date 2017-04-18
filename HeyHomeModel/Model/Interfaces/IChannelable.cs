using HeyHomeModel.Model.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyHomeModel.Model.Interfaces
{
    public interface IChannelable
    {       
        void ChangeChannel(int id);

        List<Channel> ChannelList { get; set; }

        string CurrentChannel { get; set; }

        void AddChannel(string name);

        void SetDefaultCurrentChannel();

    }
}
