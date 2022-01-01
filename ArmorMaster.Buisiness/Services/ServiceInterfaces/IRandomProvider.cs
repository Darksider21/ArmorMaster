using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services.ServiceInterfaces
{
    public interface IRandomProvider
    {
        public  Random CurrentRandom { get; }
        public int Next(int lowBarier, int heighBarier);
    }
}
