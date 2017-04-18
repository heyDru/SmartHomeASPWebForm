using HeyHomeModel.Model.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsHome.Core;
using WebFormsHome.Core.Interfaces;
using WebFormsHome.Utilities;

namespace WebFormsHome.Controls
{
    public class AddDeviceControl: Panel
    {
        DeviceManager manager;
        Device device;
        Button addButton;
        TextBox consumptionTextBox;
        DropDownList dropDeviceList;
        List<Device> devices;
        RandomIdGenerator idGenerator = new RandomIdGenerator();
        DeviceCreator creator ;
        ControlCreator ctrCreator;
        Label validatorLabel;

        public AddDeviceControl(List<Device> deviceList,DeviceManager manager )
        {
            devices = deviceList;
            this.manager = manager;
            creator = new DeviceCreator(manager,deviceList);
            ctrCreator = new ControlCreator(manager);
            Initializer();
        }

        protected void Initializer()
        {
            Panel innerPanel = new Panel();
            innerPanel.CssClass = "row";


            consumptionTextBox = GenerateConsumptionTextBox();
            dropDeviceList = GenerateDeviceTypesList();
            addButton = GenerateAddButton();

            validatorLabel = new Label();
            validatorLabel.CssClass = "col-md-6 text-center   validatorLabel";


            innerPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(dropDeviceList,3));
            innerPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(consumptionTextBox,3));
            innerPanel.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(addButton,3));
         
            Controls.Add(innerPanel);
            Controls.Add(validatorLabel);
        }

        protected TextBox GenerateConsumptionTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.CssClass = "consumptionTextBox";
            textBox.ID = "consumptionBox";
            textBox.Text = "";
            textBox.TextMode = TextBoxMode.Number;
            textBox.Attributes.Add("step", "0.1");
            textBox.Attributes.Add("placeholder", "Consumption");
            return textBox;
        }

        protected DropDownList GenerateDeviceTypesList()
        {
            List<string> deviceTypes = new List<string>
            {
                "LAMP",
                "HEATER",
                "TV"
            };
            DropDownList list = new DropDownList();

            foreach (string deviceType in deviceTypes) 
            {
                ListItem listItem = new ListItem(deviceType);
                list.Items.Add(listItem);
            }
            
            return list;
        }

        protected Button GenerateAddButton()
        {
            var addBtn = new Button();

            addBtn.Text = "Add";
            addBtn.ID = "addBtn";
            addBtn.CssClass = "addButton";
            addBtn.Click += AddButtonClick;
            return addBtn;
        }

        protected void AddButtonClick(object sender, EventArgs e)
        {
            if (consumptionTextBox.Text != "")
            {
                double comp;
                if (double.TryParse(consumptionTextBox.Text, out comp))
                {
                    device = creator.CreateDevice(dropDeviceList.SelectedItem.Text, comp);

                    Control p = Parent;
                    var devicePanels =  p.Parent.Parent.FindControl("devicePanels");
                    devicePanels.Controls.Add(ctrCreator.CreateControl(device, devices));
                }
                else
                {
                    validatorLabel.Text = "Please, enter correct consumption value ";
                }
            }
            else
            {
                validatorLabel.Text = "Please, enter consumption value ";
            }

        }
    }
}