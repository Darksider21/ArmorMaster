using ArmorMaster.Buisiness.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Services
{
    public class CalculationService : ICalculationService
    {
        public int GetNThTriangularNumber(int n)
        {
            return ( n*n + n) / 2;
        }

        
    }
}
