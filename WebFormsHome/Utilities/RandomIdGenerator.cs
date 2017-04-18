using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFormsHome.Utilities.Interfaces;

namespace WebFormsHome.Utilities
{
    public class RandomIdGenerator: IIdGeneratable
    {
        public int GenerateId()
        {
            Random rnd = new Random();
            int id = rnd.Next(0, 1000000);
            return id;
        }
    }
}