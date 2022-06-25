using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Util
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BusMessageAttribute : Attribute
    {
        public string _busMessageName;

        public BusMessageAttribute(string busMessageName)
        {
            this._busMessageName = busMessageName;
        }
    }
}
