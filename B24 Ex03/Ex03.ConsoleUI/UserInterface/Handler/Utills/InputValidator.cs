using Ex03.ConsoleUI.UserInterface.Handler.Utills.Printer;
using System;
using System.Text.RegularExpressions;

namespace Ex03.ConsoleUI.UserInterface.Handler.Utills.Validator
{
    sealed internal class InputValidator
    {
        internal void ValidateUserChoice(string i_UserInput, out OutputPrinter.eUserOptions io_Userchoice)
        {
            if (!Enum.TryParse(i_UserInput, true, out io_Userchoice) || !Enum.IsDefined(typeof(OutputPrinter.eUserOptions), io_Userchoice))
            {
                throw new ArgumentException("Invalid choice!");
            }
        }

        internal void TryParsingToFloat(string i_UserInput, out object o_ParsedFloat)
        {
            if (!float.TryParse(i_UserInput, out float floatNumber))
            {
                throw new FormatException($"{i_UserInput} is invalid input, please enter a number!");
            }

            o_ParsedFloat = floatNumber;
        }

        internal void TryParsingToInt(string i_UserInput, out object o_ParsedInteger)
        {
            if (!int.TryParse(i_UserInput, out int intNumber))
            {
                throw new FormatException($"{i_UserInput}  is invalid input, please enter a number!");
            }

            o_ParsedInteger = intNumber;
        }

        internal void TryParsingToChar(string i_UserInput, out object o_ParsedChar)
        {
            if (!char.TryParse(i_UserInput, out char character))
            {
                throw new FormatException($"{i_UserInput}  is invalid input, please enter a character!");
            }

            o_ParsedChar = character;
        }

        internal void TryParsingToUnsignedInt(string i_UserInput, out object o_ParsedUnsignedInteger)
        {
            if (!uint.TryParse(i_UserInput, out uint unsignedIntNumber))
            {
                throw new FormatException($"{i_UserInput}  is invalid input, please enter a positive number!");
            }

            o_ParsedUnsignedInteger = unsignedIntNumber;
        }

        internal static bool ValidateNonEmptyString(string i_Input)
        {
            string pattern = "^(?!\\s*$).+";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(i_Input);
        }
    }
}