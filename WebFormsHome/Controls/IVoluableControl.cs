using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebFormsHome.Controls
{
    public class IVoluableControl:Panel

    {
        IVoluable volDevice;
        TextBox volumeTextBox;
        Label intensityLabel;
        int deviceId;

        public IVoluableControl(Device device)
        {
            deviceId = device.Id;
            volDevice = (IVoluable)device;
            Initializer();
        }

        public void Initializer()
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

            Controls.Add(innerPanel);
        }

        TextBox GenerateVolumeTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.ID = "volume" + deviceId.ToString();
            textBox.Text = volDevice.Volume.ToString();
            textBox.Attributes.Add("max", "100");
            textBox.Attributes.Add("min", "0");
            textBox.CssClass = "intensityInput";
            textBox.Text = volDevice.Volume.ToString();

            return textBox;
        }
    }
}