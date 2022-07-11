using System;
using System.Linq;

namespace Network
{
    public class PortChecker : Checker
    {
        protected override bool Check(string value) => value.Length > 0 && value.All(char.IsDigit);
    }
}