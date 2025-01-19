using Ex03.ConsoleUI.UserInterface.Handler.User;
using Ex03.ConsoleUI.UserInterface.Handler.Utills.Printer;
using Ex03.GarageLogic.GarageManager;
using Ex03.GarageLogic.Vehicles.Factory;
using System;

namespace Ex03.ConsoleUI.UserInterface
{
    sealed internal class GarageConsoleUI
    {
        private readonly Garage.GarageManager r_GarageManager;
        private readonly VehicleFactory r_VehicleFactory;
        private readonly UserHandler r_UserHandler;
        private const bool v_UserExited = true;

        public GarageConsoleUI()
        {
            r_GarageManager = new Garage.GarageManager(new Garage());
            r_VehicleFactory = new VehicleFactory();
            r_UserHandler = new UserHandler(r_GarageManager.GetInitializePrompts());
        }

        internal void RunSystem()
        {
            bool hasUserExited = !v_UserExited;
            OutputPrinter.eUserOptions userChoice;

            r_UserHandler.WelcomeUser();
            while (!hasUserExited)
            {
                try
                {
                    userChoice = r_UserHandler.HandleUserChoice();
                    r_UserHandler.ExecuteUserChoice(userChoice, r_GarageManager, r_VehicleFactory);
                    hasUserExited = (userChoice == OutputPrinter.eUserOptions.Exit);
                }
                catch (Exception error)
                {
                    if (error.InnerException != null)
                    {
                        r_UserHandler.HandleError($"{error.InnerException.Message}");
                    }
                    else
                    {
                        r_UserHandler.HandleError($"{error.Message}");
                    }
                }
            }

            r_UserHandler.GoodByeUser();
        }
    }
}