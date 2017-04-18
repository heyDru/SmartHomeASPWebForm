using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebFormsHome.Core;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Controls
{
    public class TVControl:Panel

    {
        ITV tv;

        TextBox volumeTextBox;
        Label intensityLabel;

        Label currentChannelNumber;
        Label currentChannelLabel;
        DropDownList channelsDropDownList;
        Button AddNewChannel;

        int deviceId;
        List<Device> devices;
        List<Channel> channels;

        protected Device device;
        protected Label deviceTypeLabel;
        protected Label deviceIdLabel;
        protected Label consumptionLabel;
        protected Label switchLabel;
        protected Label idLabel;
        public Button switchButton;
        public Button applyButton;
        public Button deleteButton;

        DeviceManager manager;

        public TVControl(Device device, List<Device> devicesList, List<Channel> channelList,DeviceManager manager)
        {
            this.manager = manager;
            this.device = device;
            channels = channelList;
            deviceId = device.Id;
            tv = (ITV)device;
            devices = devicesList;
            InitializeType();
            Initializer();
            InitializerBase();
        }

        protected void InitializerBase()
        {
            string id = device.Id.ToString();
            CssClass = "col-md-3 devicePanel";

            if (device.TurnOn)
                switchLabel = ControlConstructorHelper.GenerateLabel("ON" + "<br/>");
            else
                switchLabel = ControlConstructorHelper.GenerateLabel("OFF" + "<br/>");

            consumptionLabel = ControlConstructorHelper.GenerateLabel("Consumption: " + device.Consumption.ToString() + "<span class=\"kW\"> kW*h</span>");
            applyButton = GenerateApplyButton(id);
            deleteButton = GenerateDeleteButton(id);
            switchButton = GenerateSwitchButton(id);

            switchButton.CssClass = " btn switchButton";
            applyButton.CssClass = "btn btn-primary applyButton";
            deleteButton.CssClass = " btn btn-danger deleteButton";


            Panel innerPanel = new Panel();
            innerPanel.CssClass = "col-md-12 text-center basePanel";
            innerPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(consumptionLabel, 12));

            Panel panelRow = new Panel();
            panelRow.CssClass = "row";
            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(switchButton, 7));
            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(switchLabel, 4));

            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(applyButton, 6));
            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(deleteButton, 6));

            innerPanel.Controls.Add(panelRow);

            Controls.Add(innerPanel);
        }

        protected void InitializeType()
        {
            deviceTypeLabel = ControlConstructorHelper.GenerateLabel(device.Type);
            deviceTypeLabel.CssClass = "deviceTypeLabel";
            Panel innerpanel = new Panel();
            innerpanel.CssClass = "col-md-12 text-center";
            innerpanel.Controls.Add(deviceTypeLabel);

            Controls.Add(innerpanel);
        }
        protected void Initializer()
        {
            VolumeInitializer();
            ChannelInitializer();
        }

        protected void VolumeInitializer ()
        {
            intensityLabel = ControlConstructorHelper.GenerateLabel("Volume: ");
            volumeTextBox = GenerateVolumeTextBox();
            // deviceTypeLabel = ControlConstructorHelper.GenerateLabel(Device.Type);

            Panel innerPanel = new Panel();
            innerPanel.CssClass = "text-center innerCtr";
            // innerPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(deviceTypeLabel, 12));

            Panel valuePanel = new Panel();
            valuePanel.CssClass = "col-md-12 row text-center";
            valuePanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(intensityLabel, 7));
            valuePanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(volumeTextBox, 4));
            innerPanel.Controls.Add(valuePanel);

            Controls.Add(innerPanel);
        }

        protected void ChannelInitializer ()
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
                    if (ch.ChannelName.ToUpper() == tv.CurrentChannel)
                        listItem.Selected = true;
                    list.Items.Add(listItem);
                }
            }
            if (!device.TurnOn)
            {
                list.Enabled = false;
                //list.Visible = true;
            }
            return list;
        }

        TextBox GenerateVolumeTextBox()
        {
            IVoluable volDevice = (IVoluable)device;
            TextBox textBox = new TextBox();
            textBox.ID = "volume" + deviceId.ToString();
            textBox.Attributes.Add("max", "100");
            textBox.Attributes.Add("min", "0");
            textBox.TextMode = TextBoxMode.Number;

            if (device.TurnOn)
            {
                textBox.Text = volDevice.Volume.ToString();

                textBox.Enabled = true;
            }
            else
            {
                textBox.Text = "off";
                textBox.Enabled = false;
            }
            textBox.CssClass = "intensityInput";

            return textBox;
        }

        protected Button GenerateDeleteButton(string id)
        {
            var deleteBtn = new Button();

            deleteBtn.Text = "Delete";
            deleteBtn.ID = "del" + id;
            deleteBtn.Click += DeleteButtonClick;
            return deleteBtn;
        }

        protected Button GenerateApplyButton(string id)
        {
            var applyBtn = new Button();
            applyBtn.Text = "Apply";
            applyBtn.ID = "apply" + id;
            if (device.TurnOn)
            {
                applyBtn.Enabled = true;
            }
            else
            {
                applyBtn.Enabled = false;
            }
            applyBtn.Click += ApplyButtonClick;
            return applyBtn;
        }

        protected Button GenerateSwitchButton(string id)
        {
            var switchBtn = new Button();
            switchBtn.ID = "switch" + id;
            switchBtn.Text = "On/Off";
            switchBtn.Click += SwitchButtonClick;

            return switchBtn;
        }

        protected virtual void SwitchButtonClick(object sender, EventArgs e)
        {
            if (device.TurnOn)
            {
                device.Switch();
                switchLabel.Text = "OFF";
                volumeTextBox.Enabled = false;
                volumeTextBox.Text = "---";
                channelsDropDownList.Enabled = false;
                applyButton.Enabled = false;
            }
            else
            {
                device.Switch();
                switchLabel.Text = "ON";
                volumeTextBox.Enabled = true;
                volumeTextBox.Text = tv.Volume.ToString();
                channelsDropDownList.Enabled = true;
                applyButton.Enabled = true;
            }
            foreach (var item in devices)
            {
                if (item.Id == device.Id)
                    item.TurnOn = device.TurnOn;
            }
            manager.SaveDevices(devices);
        }

        protected virtual void ApplyButtonClick(object sender, EventArgs e)
        {
            int volume;
            if (Int32.TryParse(volumeTextBox.Text, out volume))
            {
                tv.Volume = volume;
            }
            tv.CurrentChannel = channelsDropDownList.SelectedValue;
            device = (Device)tv;
            manager.SaveDevices(devices);
        }

        protected virtual void DeleteButtonClick(object sender, EventArgs e)
        {
            Device dev = devices.Where(d => d.Id == deviceId).FirstOrDefault();
            devices.Remove(dev);
            Parent.Controls.Remove(this); // Удаление графики для фигуры
            manager.SaveDevices(devices);
        }

    }
}