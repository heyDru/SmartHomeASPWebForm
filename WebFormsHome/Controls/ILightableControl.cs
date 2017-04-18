
using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebFormsHome.Core;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Controls
{
    public class ILightableControl : Panel
    {
        ILightable lamp;
        TextBox intensityTextBox;
        Label intensityLabel;
        int deviceId;
        List<Device> devices;
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

        public ILightableControl(Device device, List<Device> devicesList,DeviceManager manager)
        {
            this.manager = manager;
            this.device = device;
            deviceId = device.Id;
            lamp = (ILightable)device;
            devices = devicesList;
            InitializeType();
            Initializer();
            InitializerBase();
        }

        protected void Initializer()
        {
            intensityLabel = ControlConstructorHelper.GenerateLabel("Intensity: ");
            intensityTextBox = GenerateIntensityTextBox();

            Panel innerPanel = new Panel();
            innerPanel.CssClass = "text-center lampCtr";

            Panel intensityPanel = new Panel();
            intensityPanel.CssClass = "col-md-12 row text-center";
            intensityPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(intensityLabel, 7));
            intensityPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(intensityTextBox, 4));
            innerPanel.Controls.Add(intensityPanel);

            Controls.Add(innerPanel);
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
            innerpanel.CssClass="col-md-12 text-center";
            innerpanel.Controls.Add(deviceTypeLabel);

            Controls.Add(innerpanel);
        }

        TextBox GenerateIntensityTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.ID = "intensity" + deviceId.ToString();
            textBox.Attributes.Add("max", "10");
            textBox.Attributes.Add("min", "0");
            textBox.TextMode = TextBoxMode.Number;

            if (device.TurnOn)
            {
                textBox.Text = lamp.Intensity.ToString();
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
            if (device.TurnOn)
            {
                device.Switch();
                switchLabel.Text = "OFF";
                intensityTextBox.Enabled = false;
                intensityTextBox.Text = "---";
                applyButton.Enabled = false;
            }
            else
            {
                device.Switch();
                switchLabel.Text = "ON";
                intensityTextBox.Enabled = true;
                intensityTextBox.Text = lamp.Intensity.ToString();
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
            int intensity;
            if (Int32.TryParse(intensityTextBox.Text, out intensity))
            {
                lamp.Intensity = intensity;
            }
            device = (Device)lamp;

            manager.SaveDevices(devices);
        }

        protected virtual void DeleteButtonClick(object sender, EventArgs e)
        {
            Device dev = devices.Where(d => d.Id == deviceId).FirstOrDefault();
            devices.Remove(dev);
            Parent.Controls.Remove(this);
            manager.SaveDevices(devices);
        }
    }
}
