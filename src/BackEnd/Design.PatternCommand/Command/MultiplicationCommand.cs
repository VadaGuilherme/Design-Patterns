﻿using Design.Pattern.Command.Interfaces;
using Design.Pattern.Command.Receiver;

namespace Design.Pattern.Command.Command
{
    public class MultiplicationCommand : ICommand
    {
        private readonly SimpleCalculator _simpleCalculator;

        public MultiplicationCommand(SimpleCalculator simpleCalculator)
        {
            _simpleCalculator = simpleCalculator;
        }

        public void Execute()
        {
            _simpleCalculator.Multiplication();
        }
    }
}