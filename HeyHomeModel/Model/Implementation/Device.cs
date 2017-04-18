using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyHomeModel.Model.Implementation
{
   public  class Device
    {
        public int Id { get; set; }
        public double Consumption { get; set; }
        public bool TurnOn { get; set; }
        public string Type { get; set; }

        public Device(string type)
        {
            Type = type;
            TurnOn = false;
        }

        public void Switch()
        {
            if (TurnOn)
            {
                TurnOn = false;
            }
            else 
            {
                TurnOn = true;
            }
        }
    }
}
