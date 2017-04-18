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
    public class IVoluableControl:Panel, IDeviceControl

    {
        IVoluable volDevice;
       public TextBox volumeTextBox;
        Label intensityLabel;
        int deviceId;
        Device device;

        public IVoluableControl(Device device)
        {
            this.device = device;
            deviceId = device.Id;
            volDevice = (IVoluable)device;
           // Initializer();
        }

        public Panel Initializer()
        {
            intensityLabel = ControlConstructorHelper.GenerateLabel("Volume: ");
            volumeTextBox = GenerateVolumeTextBox();

            Panel innerPanel = new Panel();
            innerPanel.CssClass = "text-center innerCtr";

            Panel valuePanel = new Panel();
            valuePanel.CssClass = "col-md-12 row text-center";
            valuePanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(intensityLabel, 7));
            valuePanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(volumeTextBox, 4));
            innerPanel.Controls.Add(valuePanel);
            return innerPanel;
        }

        TextBox GenerateVolumeTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.ID = "volume" + deviceId.ToString();
            textBox.Text = volDevice.Volume.ToString();
            textBox.TextMode = TextBoxMode.Number;
            textBox.Attributes.Add("max", "100");
            textBox.Attributes.Add("min", "0");
            //textBox.Text = volDevice.Volume.ToString();

            if (device.TurnOn)
            {
                textBox.Text = volDevice.Volume.ToString();
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