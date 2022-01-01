using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class RandomProvider : IRandomProvider
    {

        public Random CurrentRandom { get; }

        public RandomProvider()
        {
            CurrentRandom = new Random();
        }
        public int Next(int lowBarier, int heighBarier)
        {
            return CurrentRandom.Next(lowBarier, heighBarier);
        }
    }
}
