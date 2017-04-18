using HeyHomeModel.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyHomeModel.Model.Implementation
{
    public class Lamp : Device, ILightable
    {
        public Lamp(string type) : base(type)
        {
            Intensity = 0;
        }
        public int Intensity { get ; set; }
    }
}
