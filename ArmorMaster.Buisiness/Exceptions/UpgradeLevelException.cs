using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmorMaster.Buisiness.Exceptions
{
    public class UpgradeLevelException : Exception
    {
        public UpgradeLevelException(string message) : base(message)
        {
        }
    }
}
