using Ex03.ConsoleUI.UserInterface.Handler.Utills.Validator;
using System;

namespace Ex03.ConsoleUI.UserInterface.Handler.Utills.Reader
{
    sealed internal class InputReader
    {
        internal string ReadUserChoice() => ReadInput("Please enter your desired action:");

        internal string ReadInput(string message)
        {
            Console.Write(message + " ");
            string input = Console.ReadLine();

            if (!InputValidator.ValidateNonEmptyString(input))
            {
                throw new FormatException("You should enter an input!");
            }

            return input;
        }
    }
}