using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebFormsHome.Controls
{
    public class IChannelableControl : Panel
    {
        IChannelable channelableDevice;
        List<Channel> channels;
        Label currentChannelNumber;
        Label currentChannelLabel;
        DropDownList channelsDropDownList;
        Button AddNewChannel;
        int deviceId;

        public IChannelableControl(IEnumerable<Channel> channels, Device device)
        {
            this.channels = (List<Channel>)channels;
            channelableDevice = (TV)device;
            channelableDevice.SetDefaultCurrentChannel();
            deviceId = device.Id;
            Initializer();
        }
        public void Initializer()
        {
            currentChannelLabel = ControlConstructorHelper.GenerateLabel("Current Channel: ");
            channelsDropDownList = GenerateChannelList();

            Panel innerPanel = new Panel();
            innerPanel.CssClass = "text-center innerCtr";
            innerPanel.Controls.Add(currentChannelLabel);
            innerPanel.Controls.Add(channelsDropDownList);

            Controls.Add(innerPanel);
        }

        protected DropDownList GenerateChannelList()
        {
            DropDownList list = new DropDownList();
            list.CssClass = "channelDropList";
            foreach (Channel ch in channels)
            {
                if (ch != null)
                {
                    ListItem listItem = new ListItem(ch.ChannelName, ch.ChannelId.ToString());
                    if (ch.ChannelName.ToUpper() == channelableDevice.CurrentChannel)
                        listItem.Selected = true;
                    list.Items.Add(listItem);
                }
            }
            return list;
        }

    }
}