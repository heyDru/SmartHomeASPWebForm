using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebFormsHome.Controls.Interfaces;
using WebFormsHome.Core;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Controls.ControlParts
{
    public class ITemperatureControl : Panel, IDeviceControl
    {
        ITemperature heater;
        public TextBox temperatureTextBox;
        public Label temperatureLabel;
        int deviceId;
        public Device device;
        //public Label deviceTypeLabel;
        //public Label deviceIdLabel;
        //public Label consumptionLabel;
        //public Label switchLabel;
        //public Label idLabel;
        //public Button switchButton;
        //public Button applyButton;
        //public Button deleteButton;

        public ITemperatureControl(Device device)
        {
            this.device = device;
            deviceId = device.Id;
            heater = (ITemperature)device;
        }

        public Panel Initializer()
        {
            temperatureLabel = ControlConstructorHelper.GenerateLabel("Temperature: ");
            temperatureTextBox = GenerateTemperatureTextBox();

            Panel innerPanel = new Panel();
            innerPanel.CssClass = "text-center innerCtr";

            Panel temperaturePanel = new Panel();
            temperaturePanel.CssClass = "col-md-12 row text-center";
            temperaturePanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(temperatureLabel, 7));
            temperaturePanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(temperatureTextBox, 4));
            innerPanel.Controls.Add(temperaturePanel);
            return innerPanel;
        }

        TextBox GenerateTemperatureTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.ID = "temp" + deviceId.ToString();
            textBox.TextMode = TextBoxMode.Number;
            textBox.Attributes.Add("max", "30");
            textBox.Attributes.Add("min", "15");

            if (device.TurnOn)
            {
                textBox.Text = heater.Temperature.ToString();
                textBox.Enabled = true;
            }
            else
            {
                textBox.Enabled = false;
            }

            textBox.CssClass = "textInputNumber";
            return textBox;
        }   
    }
}
