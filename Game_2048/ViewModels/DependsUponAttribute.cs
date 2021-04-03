using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048.ViewModels
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class DependsUponAttribute : Attribute
    {
        private readonly string propertyName;

        public DependsUponAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }
        public string PropertyName => propertyName;
    }
}
