
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
using WebFormsHome.Core;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Controls.ControlParts
{
    public class ILightableControl : Panel, IDeviceControl
    {
        ILightable lamp;
        public TextBox intensityTextBox { get; set; }
        public Label intensityLabel;
        int deviceId;
        public Device device;
        public Label deviceTypeLabel;
        public Label deviceIdLabel;
        public Label consumptionLabel;
        public Label switchLabel;
        public Label idLabel;
        public Button switchButton;
        public Button applyButton;
        public Button deleteButton;

        public ILightableControl(Device device)
        {
            this.device = device;
            deviceId = device.Id;
            lamp = (ILightable)device;

        }

        public Panel Initializer()
        {
            intensityLabel = ControlConstructorHelper.GenerateLabel("Intensity: ");
            intensityTextBox = GenerateIntensityTextBox();

            Panel innerPanel = new Panel();
            innerPanel.CssClass = "text-center innerCtr";

            Panel intensityPanel = new Panel();
            intensityPanel.CssClass = "col-md-12 row text-center";
            intensityPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(intensityLabel, 7));
            intensityPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(intensityTextBox, 4));
            innerPanel.Controls.Add(intensityPanel);
            return innerPanel;
           // Controls.Add(innerPanel);
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

            textBox.CssClass = "textInputNumber";
            return textBox;
        }

    }
}
