using Plugin.ValidationRules;
using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.ValidationRules
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            var str = value as string;
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
