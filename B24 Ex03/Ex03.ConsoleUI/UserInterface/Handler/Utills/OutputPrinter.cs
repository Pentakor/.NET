using Ex03.GarageLogic.DTO;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI.UserInterface.Handler.Utills.Printer
{
    sealed internal class OutputPrinter
    {
        private const int k_WaitingTime = 2500;
        private const float k_MinutesToHour = 60f;

        internal enum eUserOptions : uint
        {
            InsertVehicle = 1,
            DisplayLicenses,
            UpdateVehicleState,
            InflateVehicleTires,
            FillFuel,
            ChargeBattery,
            DisplayVehicleInfo,
            Exit
        }

        internal void WelcomeUser() => Console.WriteLine("Welcome to the Garage Management System!");

        internal void PrintUserOptions()
        {
            Console.Write("Please choose your desired action:");
            Console.WriteLine($@"
            {(int)eUserOptions.InsertVehicle} - Add a new vehicle to the garage
            {(int)eUserOptions.DisplayLicenses} - Display license plates
            {(int)eUserOptions.UpdateVehicleState} - Update existing vehicle's status
            {(int)eUserOptions.InflateVehicleTires} - Inflate vehicle's tires
            {(int)eUserOptions.FillFuel} - Fill a vehicle's gas tank
            {(int)eUserOptions.ChargeBattery} - Charge a vehicle's battery
            {(int)eUserOptions.DisplayVehicleInfo} - Display vehicle's information
            {(int)eUserOptions.Exit} - Exit");
        }

        internal void PrintVehicleStatuses()
        {
            Console.WriteLine("Available vehicle statuses: ");
        }

        internal void PrintOptionsList(List<string> i_Options)
        {
            Console.WriteLine("Options:");
            for (int i = 0; i < i_Options.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {i_Options[i]}");
            }
        }

        internal void PrintLicensePlates(string i_VehicleStatus, LicensePlatesDTO i_LicensePlates)
        {
            if (!i_VehicleStatus.Equals("All") && !i_LicensePlates.LicensePlatesDict.ContainsKey(i_VehicleStatus))
            {
                Console.WriteLine($"There are no vehicles under status {i_VehicleStatus}.");
            }
            else
            {
                Console.WriteLine($"Vehicles on status: ");
                Console.Write(i_LicensePlates.ToString());
            }
        }

        internal void PrintUpdatedStatusMessage(string i_LicensePlate, string i_OldState, string i_NewState)
        {
            Console.WriteLine($"Successfully updated vehicle licensed {i_LicensePlate} status from {i_OldState} to {i_NewState}");
        }

        internal void PrintSuccesfullyInflatedMessage(string i_LicensePlate)
        {
            Console.WriteLine($"Successfully inflated vehicle licensed {i_LicensePlate}'s wheels");
        }

        internal void PrintSuccesfullyFillOfFuelMessage(string i_LicensePlate, string i_FuelAmountAdded, float i_NewAmount)
        {
            Console.WriteLine($"Successfully filled {i_FuelAmountAdded} litres of gas in vehicle "
                                + $"licensed {i_LicensePlate}. New amount: {i_NewAmount}L");
        }

        internal void PrintSuccesfullyChargeOfBatteryMessage(string i_LicensePlate, float i_ChargeTimeMinutesAdded, float i_NewAmount)
        {
            Console.WriteLine($"Successfully charged {i_ChargeTimeMinutesAdded / k_MinutesToHour} hours of battery in vehicle " +
               $"licensed {i_LicensePlate}. New amount: {i_NewAmount}H");
        }

        internal void PrintError(string i_Error) => Console.WriteLine($"Error: {i_Error}\n");

        internal void GoodByeMessage()
        {
            Console.WriteLine("Thank you for using our garage! Goodbye!");
            System.Threading.Thread.Sleep(k_WaitingTime);
        }
    }
}