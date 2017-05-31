using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome.Controls
{
    public  class DeviceControl : Panel
    {
        HttpRequest request;
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

        public DeviceControl(Device device, List<Device> deviceList, HttpRequest currentReuest, DeviceManager manager)
        {
            this.manager = manager;
            request = currentReuest;
            this.device = device;
            devices = deviceList;
        }

        public void InitializerBase()
        {
            string id = device.Id.ToString();

            if (device.TurnOn)
                switchLabel = ControlConstructorHelper.GenerateLabel("ON" + "< br />");
            else
                switchLabel = ControlConstructorHelper. GenerateLabel("OFF" + "<br/>");

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
            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(switchButton,7));
            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(switchLabel,4));

            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(applyButton,6));
            panelRow.Controls.Add(ControlConstructorHelper.GenerateBootstrapDiv(deleteButton,6));

            innerPanel.Controls.Add(panelRow);

            Controls.Add(innerPanel);
        }

        public void InitializeType()
        {
            deviceTypeLabel = ControlConstructorHelper.GenerateLabel(device.Type);

            Controls.Add(deviceTypeLabel);
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
            }
            else
            {
                device.Switch();
                switchLabel.Text = "ON";
            }
        }

        protected virtual void ApplyButtonClick(object sender, EventArgs e)
        {
           
        }

        protected virtual void DeleteButtonClick(object sender, EventArgs e)
        {
            //Логика Удаления device из базы

            Parent.Controls.Remove(this); // Удаление графики для фигуры
        }

    }
}