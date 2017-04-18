using HeyHomeModel.Model.Implementation;
using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsHome.Controls;
using WebFormsHome.Core;
using WebFormsHome.Core.Interfaces;

namespace WebFormsHome
{
    public partial class Default : System.Web.UI.Page
    {
        DeviceCreator deviceCreator;
        List<Device> devices;
        List<Channel> channels;
        DeviceManager manager = new SerializationManager();
        ControlCreatorByParts controlCreator;
        double currentConsumption;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                devices = (List<Device>)Session["devices"];
                channels = (List<Channel>)Session["channels"];
                DeviceCreator deviceCreator = new DeviceCreator(manager, devices);
                controlCreator = new ControlCreatorByParts(manager);
                currentConsumption = ConsumptionCounter();
            }
            else
            {
                controlCreator = new ControlCreatorByParts(manager);

                channels = manager.GetChannelList();
                devices = manager.GetDevicesList();
                Session["devices"] = devices;
                Session["channels"] = channels;
                currentConsumption = ConsumptionCounter();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            currentConsumption = ConsumptionCounter();
            ShowCurrentConsumption(currentConsumption);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Page.EnableViewState = false;
        
            }
            InitialiseControls();
        }

        protected void InitialiseControls()
        {
            devices = (List<Device>)Session["devices"];

            foreach (var device in devices)
            {
                devicePanels.Controls.Add(controlCreator.CreateControl(device,devices));
            }
            addDevicePanel.Controls.Add(new AddDeviceControl(devices, manager ));
        }

        protected void ShowCurrentConsumption (double sumValueConsumtion)
        {
            Label valueConsumtion = ControlConstructorHelper.
                GenerateLabel("Current Consumption: " + sumValueConsumtion.ToString() + "<span class=\"kW\"> kW*h</span>");
            valueConsumtion.CssClass = "col-md-6 text-center  valueConsumtion";
            consumpPanel.Controls.Add(valueConsumtion);
        }

        protected double ConsumptionCounter()
        {
            double sumValueConsumtion = 0;
            foreach (Device device in devices)
            {
                if (device.TurnOn)
                {
                    sumValueConsumtion += device.Consumption;
                }
            }
            return sumValueConsumtion;
        }

    }
}