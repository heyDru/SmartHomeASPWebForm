using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebFormsHome.Controls.Interfaces;

namespace WebFormsHome.Controls.ControlParts
{
    public class IChannelableControl : Panel, IDeviceControl
    {
        IChannelable channelableDevice;
        List<Channel> channels;
        public Label currentChannelNumber;
        public Label currentChannelLabel;
        public DropDownList channelsDropDownList;
        Button AddNewChannel;
        int deviceId;
        Device device;

        public IChannelableControl(IEnumerable<Channel> channels, Device device)
        {
            this.channels = (List<Channel>)channels;
            //  channelableDevice = (TV)device;
           // channelableDevice.SetDefaultCurrentChannel();
            deviceId = device.Id;
            channelableDevice = (IChannelable)device;
            this.device = device;
            //Initializer();
        }

        public Panel Initializer()
        {
            currentChannelLabel = ControlConstructorHelper.GenerateLabel("Current Channel: ");
            channelsDropDownList = GenerateChannelList();

            Panel innerPanel = new Panel();
            innerPanel.CssClass = "text-center innerCtr";
            innerPanel.Controls.Add(currentChannelLabel);
            innerPanel.Controls.Add(channelsDropDownList);

            return innerPanel;
        }

        protected DropDownList GenerateChannelList()
        {
            DropDownList list = new DropDownList();
            list.CssClass = "channelDropList";
            foreach (Channel ch in channels)
            {
                if (ch != null)
                {
                    ListItem listItem = new ListItem(ch.ChannelName, ch.ChannelName);

                    if (ch.ChannelName.ToUpper() == channelableDevice.CurrentChannel.ToUpper())
                    {
                        listItem.Selected = true;
                    }
                    list.Items.Add(listItem);

                }
            }

            if (device.TurnOn)
            {
                // textBox.Text = heater.Temperature.ToString();
                list.Enabled = true;
            }
            else
            {
                list.Enabled = false;
            }
            return list;
        }

    }
}