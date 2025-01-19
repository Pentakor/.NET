using Ex04.Menus.Interfaces;
using Ex04.Menus.Test.Methods;

namespace Ex04.Menus.Test.Interface
{
    public class TestInterface : IListener
    {
        private BaseMenu m_MainMenu;

        public TestInterface()
        {
            m_MainMenu = assembleMenu();
        }

        public void Run()
        {
            m_MainMenu.Show();
        }

        private BaseMenu assembleMenu()
        {
            BaseMenu mainMenu = new BaseMenu("Interfaces Main Menu");
            MenuItem versionsAndCapitalsMenu = new MenuItem("Version and Capitals");
            MenuItem ShowDateOrTimeMenu = new MenuItem("Show Date/Time");
            MenuItem showVersion = new MenuItem("Show Version");
            MenuItem countCapitals = new MenuItem("Count Capitals");
            MenuItem showTime = new MenuItem("Show Time");
            MenuItem showDate = new MenuItem("Show Date");

            mainMenu.Root.AddChild(versionsAndCapitalsMenu);
            mainMenu.Root.AddChild(ShowDateOrTimeMenu);
            showVersion.AddListener(this as IListener);
            countCapitals.AddListener(this as IListener);
            versionsAndCapitalsMenu.AddChild(showVersion);
            versionsAndCapitalsMenu.AddChild(countCapitals);
            showTime.AddListener(this as IListener);
            showDate.AddListener(this as IListener);
            ShowDateOrTimeMenu.AddChild(showTime);
            ShowDateOrTimeMenu.AddChild(showDate);

            return mainMenu;
        }

        public void Notify(string i_Title)
        {
            switch (i_Title)
            {
                case "Show Date":
                    TestMethods.MenuItem_ShowDateSelected();
                    break;
                case "Show Time":
                    TestMethods.MenuItem_ShowTimeSelected();
                    break;
                case "Count Capitals":
                    TestMethods.MenuItem_CountCapitalLettersSelected();
                    break;
                case "Show Version":
                    TestMethods.MenuItem_ShowVersionSelected();
                    break;
                default:
                    break;
            }
        }
    }
}
