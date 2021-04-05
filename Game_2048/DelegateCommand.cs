﻿using System;

namespace Game_2048
{
    internal sealed class DelegateCommand : Command
    {
        private static readonly Func<bool> defaultCanExecuteMethod = () => true;

        private Func<bool> canExecuteMethod;
        private readonly Action executeMethod;

        public DelegateCommand(Action executeMethod) :
            this(executeMethod, defaultCanExecuteMethod)
        { }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
            this.executeMethod = executeMethod;
        }

        public override bool CanExecute() => canExecuteMethod();

        public override void Execute() => executeMethod();
        public override void Execute(object parameter) { }
    }
}
