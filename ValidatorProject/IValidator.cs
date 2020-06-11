using System;
using System.Collections.Generic;
using System.Text;

namespace ValidatorProject
{
    interface IValidator
    {
        public abstract bool IsValid(string commandType, Dictionary<string, string> commandKeyValuePairsOfArguments);
    }
}
