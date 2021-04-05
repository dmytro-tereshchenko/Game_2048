﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048
{
    internal sealed class DialogCommand : Command
    {
        private static readonly Func<bool> defaultCanExecuteMethod = () => true;

        private Func<bool> canExecuteMethod;
        private readonly Action<object> executeMethod;

        public DialogCommand(Action<object> executeMethod) :
            this(executeMethod, defaultCanExecuteMethod)
        {
        }

        public DialogCommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
            this.executeMethod = executeMethod;
        }

        public override bool CanExecute()
        {
            return canExecuteMethod();
        }

        public override void Execute(object parameter)
        {
            executeMethod(parameter);
        }
        public override void Execute()
        {
            
        }
    }
}
