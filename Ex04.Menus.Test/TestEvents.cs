using Ex04.Menus.Events;
using Ex04.Menus.Test.Methods;

namespace Ex04.Menus.Test.Event
{
    public class TestEvents
    {
        private BaseMenu m_MainMenu;

        public TestEvents()
        {
            m_MainMenu = assembleMenu();
        }

        public void Run()
        {
            m_MainMenu.Show();
        }

        private BaseMenu assembleMenu()
        {
            BaseMenu mainMenu = new BaseMenu("Delegates Main Menu");
            MenuItem versionsAndCapitalsMenu = new MenuItem("Version and Capitals");
            MenuItem ShowDateOrTimeMenu = new MenuItem("Show Date/Time");
            MenuItem showVersion = new MenuItem("Show Version");
            MenuItem countCapitals = new MenuItem("Count Capitals");
            MenuItem showTime = new MenuItem("Show Time");
            MenuItem showDate = new MenuItem("Show Date");

            mainMenu.Root.AddChild(versionsAndCapitalsMenu);
            mainMenu.Root.AddChild(ShowDateOrTimeMenu);
            showVersion.SelectedMenuItem += TestMethods.MenuItem_ShowVersionSelected;
            countCapitals.SelectedMenuItem += TestMethods.MenuItem_CountCapitalLettersSelected;
            versionsAndCapitalsMenu.AddChild(showVersion);
            versionsAndCapitalsMenu.AddChild(countCapitals);
            showTime.SelectedMenuItem += TestMethods.MenuItem_ShowTimeSelected;
            showDate.SelectedMenuItem += TestMethods.MenuItem_ShowDateSelected;
            ShowDateOrTimeMenu.AddChild(showTime);
            ShowDateOrTimeMenu.AddChild(showDate);

            return mainMenu;
        }
    }
}
