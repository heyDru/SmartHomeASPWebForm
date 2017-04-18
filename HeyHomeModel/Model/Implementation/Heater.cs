using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyHomeModel.Model.Implementation
{
    public class Heater : Device, ITemperature
    {
        public Heater(string type) : base(type)
        {
            Temperature = 20;
        }

        public int Temperature { get ; set; }
    }
}
