using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebFormsHome.Controls
{
    public static class ControlConstructorHelper
    {
        public static Label GenerateLabel(string str,string id="")
        {
            Label label = new Label();
            label.Text = str.ToString();
            label.ID = id;
            return label;
        }

        public static Panel GenerateBootstrapDiv(WebControl innerControl, int n)
        {
            Panel panel = new Panel();
            panel.Controls.Add(innerControl);
            panel.CssClass = "col-md-" + n;

            return panel;
        }

        public static TextBox GenerateIntensityTextBox(Device device)
        {
               Lamp lamp = (Lamp)device;

                TextBox textBox = new TextBox();
                textBox.ID = "intensity" + device.Id.ToString();
                textBox.Text = lamp.Intensity.ToString();

                textBox.CssClass = "intensityInput";

                return textBox;    
        }

    }
}