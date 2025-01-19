using System;

namespace Ex04.Menus.Events
{
    public class BaseMenu
    {
        private readonly MenuItem r_Root;
        private const bool v_ExitChosen = true;
        private const bool v_InvalidInput = true;
        private const bool v_MainMenu = true;

        public MenuItem Root
        {
            get { return r_Root; }
        }

        public BaseMenu(string i_Title)
        {
            r_Root = new MenuItem(i_Title, v_MainMenu);
        }

        private static string returnBackOrExit(MenuItem i_CurrentMenu)
        {
            string returnValue;

            if (i_CurrentMenu.IsMainMenu)
            {
                returnValue = "Exit";
            }
            else
            {
                returnValue = "Back";
            }

            return returnValue;
        }

        public void Show()
        {
            Show(r_Root);
        }

        internal static void Show(MenuItem i_CurrentMenu)
        {
            int userChoice;
            bool exitChosen = !v_ExitChosen;

            while (!exitChosen)
            {
                Console.WriteLine();
                Console.WriteLine(string.Format($"**{i_CurrentMenu.Title}**"));
                Console.WriteLine("-----------------------");
                for (int i = 0; i < i_CurrentMenu.Children.Count; i++)
                {
                    Console.WriteLine(string.Format($"{i + 1} -> {i_CurrentMenu.Children[i].Title}"));
                }

                Console.WriteLine(string.Format($"0 -> {returnBackOrExit(i_CurrentMenu)}"));
                Console.WriteLine("-----------------------");
                userChoice = getChoice(i_CurrentMenu.Children.Count, i_CurrentMenu);
                if (userChoice == 0)
                {
                    exitChosen = v_ExitChosen;
                }
                else
                {
                    i_CurrentMenu.Children[userChoice - 1].Select();
                }
            }
        }

        private static int getChoice(int i_Max, MenuItem i_CurrentMenu)
        {
            int choice;

            while (v_InvalidInput)
            {
                Console.WriteLine(string.Format($"Enter your request: (1 to {i_Max} or press '0' to {returnBackOrExit(i_CurrentMenu)})"));
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= i_Max)
                {
                    break;
                }

                Console.WriteLine("Invalid choice, please try again.");
            }

            return choice;
        }
    }
}
