using System;

namespace Ex04.Menus.Test.Methods
{
    public class TestMethods
    {
        public static void MenuItem_ShowVersionSelected()
        {
            Console.WriteLine("App Version: 24.2.4.9504");
        }

        public static void MenuItem_CountCapitalLettersSelected()
        {
            string input = string.Empty;
            int countCapital = 0;

            Console.WriteLine("Please enter your sentence:");
            input = Console.ReadLine();
            foreach (char inputChar in input)
            {
                if (char.IsUpper(inputChar))
                {
                    countCapital++;
                }
            }

            Console.WriteLine(string.Format($"There are {countCapital} capitals in your sentece."));
        }

        public static void MenuItem_ShowTimeSelected()
        {
            DateTime currentTime = DateTime.Now;
            Console.WriteLine("The Hour is: " + currentTime.ToString("HH:mm:ss"));
        }

        public static void MenuItem_ShowDateSelected()
        {
            DateTime currentDate = DateTime.Now;
            Console.WriteLine("The current date is: " + currentDate.ToString("dd/MM/yyyy"));
        }
    }
}