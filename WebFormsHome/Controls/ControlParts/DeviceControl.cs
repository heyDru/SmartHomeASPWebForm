using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebFormsHome.Controls.Interfaces;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Controls.ControlParts
{
    public class DeviceControl : Panel
    {
        protected IDeviceControl CTR;
        DeviceManager manager;

        protected List<Device> devices;
        protected Device device;
        protected Label deviceTypeLabel;
        protected Label deviceIdLabel;
        protected Label consumptionLabel;
        protected Label switchLabel;
        protected Label idLabel;
        public Button switchButton;
        public Button applyButton;
        public Button deleteButton;



        public DeviceControl()
        {

        }

        public DeviceControl(Device device, List<Device> deviceList, IDeviceControl specialControl, DeviceManager manager)
        {
            this.device = device;
            devices = deviceList;
            CTR = specialControl;
            this.manager = manager;
            InitializeType();
            CTR.Initializer();
            InitializerBase();
        }



        public void InitializerBase()
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
            Controls.Add(CTR.Initializer());
            Controls.Add(innerPanel);
        }

        public void InitializeType()
        {
            deviceTypeLabel = ControlConstructorHelper.GenerateLabel(device.Type);
            deviceTypeLabel.CssClass = "deviceTypeLabel";
            Panel innerpanel = new Panel();
            innerpanel.CssClass = "col-md-12 text-center";
            innerpanel.Controls.Add(deviceTypeLabel);

            Controls.Add(innerpanel);
        }

        public void InitializerControl(IDeviceControl specialControl)
        {
            CTR = specialControl;
            Controls.Add(CTR.Initializer());
        }



        protected HtmlGenericControl GenerateSpan(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
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
            if (device is ILightable)
            {
                var currentCTR = (ILightableControl)CTR;
                if (device.TurnOn)
                {

                    device.Switch();
                    switchLabel.Text = "OFF";
                    currentCTR.intensityTextBox.Enabled = false;
                    applyButton.Enabled = false;
                }
                else
                {
                    device.Switch();
                    switchLabel.Text = "ON";
                    currentCTR.intensityTextBox.Enabled = true;
                    currentCTR.intensityTextBox.Text = ((ILightable)device).Intensity.ToString();
                    applyButton.Enabled = true;
                }
                foreach (var item in devices)
                {
                    if (item.Id == device.Id)
                        item.TurnOn = device.TurnOn;
                }
                manager.SaveDevices(devices);
            }

            if(device is ITemperature)
            {
                var currentCTR = (ITemperatureControl)CTR;
                if (device.TurnOn)
                {
                    device.Switch();
                    switchLabel.Text = "OFF";
                    currentCTR.temperatureTextBox.Enabled = false;
                    applyButton.Enabled = true;
                }
                else
                {
                    device.Switch();
                    switchLabel.Text = "ON";
                    currentCTR.temperatureTextBox.Enabled = true;
                    currentCTR.temperatureTextBox.Text = ((ITemperature)device).Temperature.ToString();
                    applyButton.Enabled = true;
                }
                foreach (var item in devices)
                {
                    if (item.Id == device.Id)
                        item.TurnOn = device.TurnOn;
                }
                manager.SaveDevices(devices);
            }

          else  if ((device is IVoluable) && (device is IChannelable))
            {
                var currentCTR = (ITVControl)CTR;
                if (device.TurnOn)
                {
                    device.Switch();
                    switchLabel.Text = "OFF";
                    currentCTR.VoluableControl.volumeTextBox.Enabled = false;
                    currentCTR.ChannelControl.channelsDropDownList.Enabled = false;
                    applyButton.Enabled = true;
                }
                else
                {
                    device.Switch();
                    switchLabel.Text = "ON";
                    currentCTR.VoluableControl.volumeTextBox.Enabled = true;
                    currentCTR.VoluableControl.volumeTextBox.Text = ((IVoluable)device).Volume.ToString();
                    currentCTR.ChannelControl.channelsDropDownList.Enabled = true;

                    applyButton.Enabled = true;
                }
                foreach (var item in devices)
                {
                    if (item.Id == device.Id)
                        item.TurnOn = device.TurnOn;
                }
                manager.SaveDevices(devices);
            }

        }

        protected virtual void ApplyButtonClick(object sender, EventArgs e)
        {
            if (device is ILightable)
            {
                var currentCTR = (ILightableControl)CTR;
                int intensity;
                if (Int32.TryParse(currentCTR.intensityTextBox.Text, out intensity))
                {
                    ((ILightable)device).Intensity = intensity;
                }
                //device = (Device)lamp;

                manager.SaveDevices(devices);
            }
            //////////

            if (device is ITemperature)
            {
                var currentCTR = (ITemperatureControl)CTR;
                int temperature;
                if (Int32.TryParse(currentCTR.temperatureTextBox.Text, out temperature))
                {
                    ((ITemperature)device).Temperature = temperature;
                }
               // device = (Device)heater;

                manager.SaveDevices(devices);
            }

            if ((device is IVoluable) && (device is IChannelable))
            {
                var currentCTR = (ITVControl)CTR;
                TV tv = (TV)device;
                int volume;
                if (Int32.TryParse(currentCTR.VoluableControl.volumeTextBox.Text, out volume))
                {
                    tv.Volume = volume;
                }
                tv.CurrentChannel = currentCTR.ChannelControl.channelsDropDownList.SelectedValue;
               device = (Device)tv;

                manager.SaveDevices(devices);
            }

        }

        protected virtual void DeleteButtonClick(object sender, EventArgs e)
        {
            Device dev = devices.Where(d => d.Id == this.device.Id).FirstOrDefault();
            devices.Remove(dev);
            Parent.Controls.Remove(this);
            manager.SaveDevices(devices);
        }

    }
}