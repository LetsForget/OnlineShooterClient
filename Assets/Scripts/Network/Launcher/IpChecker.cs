using System;
using System.Linq;

namespace Network.Launcher
{
    public class IpChecker : Checker
    {
        protected override bool Check(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var splitValues = value.Split('.');
            
            if (splitValues.Length != 4)
            {
                return false;
            }

            return splitValues.All(r => byte.TryParse(r, out _));
        }
    }
}