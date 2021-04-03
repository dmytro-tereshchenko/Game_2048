using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048.ViewModels
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class CanExecuteAttribute : Attribute
    {
        private readonly string propertyName;

        public CanExecuteAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }
        public string PropertyName => propertyName;
    }
}
