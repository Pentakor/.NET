using Ex03.ConsoleUI.UserInterface.Handler.Utills.Printer;
using Ex03.ConsoleUI.UserInterface.Handler.Utills.Reader;
using Ex03.ConsoleUI.UserInterface.Handler.Utills.Validator;
using Ex03.GarageLogic.DTO;
using Ex03.GarageLogic.GarageManager;
using Ex03.GarageLogic.PropetiesApi;
using Ex03.GarageLogic.Vehicles.Factory;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI.UserInterface.Handler.User
{
    sealed internal class UserHandler
    {
        private readonly Dictionary<string, FieldClassInfo> r_InitilaizedPrompts;
        private readonly OutputPrinter r_OutputPrinter;
        private readonly InputReader r_InputReader;
        private readonly InputValidator r_InputValidator;
        private const bool v_SuccesfullyInserted = true;
        private const bool v_SuccesfullyRead = true;
        private const bool k_ValidOwnerInfo = true;

        public UserHandler(Dictionary<string, FieldClassInfo> i_InitilaizedPromptsDict)
        {
            r_InitilaizedPrompts = i_InitilaizedPromptsDict;
            r_OutputPrinter = new OutputPrinter();
            r_InputReader = new InputReader();
            r_InputValidator = new InputValidator();
        }

        internal void WelcomeUser()
        {
            r_OutputPrinter.WelcomeUser();
        }

        internal void GoodByeUser()
        {
            r_OutputPrinter.GoodByeMessage();
        }

        internal void HandleError(string i_ErrorMessage)
        {
            r_OutputPrinter.PrintError(i_ErrorMessage);
        }

        internal OutputPrinter.eUserOptions HandleUserChoice()
        {
            string userInput;

            r_OutputPrinter.PrintUserOptions();
            userInput = r_InputReader.ReadUserChoice();
            r_InputValidator.ValidateUserChoice(userInput, out OutputPrinter.eUserOptions userChoice);

            return userChoice;
        }

        internal void ExecuteUserChoice(OutputPrinter.eUserOptions i_UserChoice, Garage.GarageManager i_GarageManager, VehicleFactory i_VehicleFactory)
        {
            switch (i_UserChoice)
            {
                case OutputPrinter.eUserOptions.InsertVehicle:
                    addNewVehicleToGarage(i_GarageManager, i_VehicleFactory);
                    break;
                case OutputPrinter.eUserOptions.DisplayLicenses:
                    displayLicenses(i_GarageManager);
                    break;
                case OutputPrinter.eUserOptions.UpdateVehicleState:
                    updateVehicleState(i_GarageManager);
                    break;
                case OutputPrinter.eUserOptions.InflateVehicleTires:
                    inflateVehicleTires(i_GarageManager);
                    break;
                case OutputPrinter.eUserOptions.FillFuel:
                    fillFuel(i_GarageManager);
                    break;
                case OutputPrinter.eUserOptions.ChargeBattery:
                    chargeBattery(i_GarageManager);
                    break;
                case OutputPrinter.eUserOptions.DisplayVehicleInfo:
                    displayVehicleInfo(i_GarageManager);
                    break;
                case OutputPrinter.eUserOptions.Exit:
                    break;
                default:
                    break;
            }
        }

        private string handleFieldWithOptions(FieldClassInfo i_FieldInfo)
        {
            string userChoice;

            r_OutputPrinter.PrintOptionsList(i_FieldInfo.Options);
            userChoice = r_InputReader.ReadInput(i_FieldInfo.PromptMessage);
            if (userChoice.Length != 1 || !char.IsDigit(userChoice[0]))
            {
                throw new FormatException("You should enter a valid number from the above options.");
            }

            return userChoice;
        }

        private object handleFieldWithoutOptions(FieldClassInfo i_FieldInfo)
        {
            object returnValue = null;
            string input = r_InputReader.ReadInput(i_FieldInfo.PromptMessage);

            switch (i_FieldInfo.InputType)
            {
                case FieldClassInfo.eInputType.Int:
                    r_InputValidator.TryParsingToInt(input, out returnValue);
                    break;
                case FieldClassInfo.eInputType.Char:
                    r_InputValidator.TryParsingToChar(input, out returnValue);
                    break;
                case FieldClassInfo.eInputType.Float:
                    r_InputValidator.TryParsingToFloat(input, out returnValue);
                    break;
                case FieldClassInfo.eInputType.String:
                    returnValue = input;
                    break;
                default:
                    break;
            }

            return returnValue;
        }

        private void handleFieldsInformation(object i_ManufacturedVehicle, FieldClassInfo i_FieldInfo)
        {
            object userChoice;

            if (i_FieldInfo.Options != null && i_FieldInfo.Options.Count > 0)
            {
                userChoice = handleFieldWithOptions(i_FieldInfo);
            }
            else
            {
                userChoice = handleFieldWithoutOptions(i_FieldInfo);
            }

            setField(i_ManufacturedVehicle, i_FieldInfo.FieldName, userChoice, i_FieldInfo.IsInfo);
        }

        private void setInfoField(object i_ManufacturedVehicle, string i_FieldName, object i_Value)
        {
            VehicleInfoProperties propertiesDTO = new VehicleInfoProperties(i_ManufacturedVehicle);

            try
            {
                propertiesDTO.SetVehicleProperty(i_FieldName, i_Value);
            }
            catch (Exception error)
            {
                if (error.InnerException != null)
                {
                    throw error.InnerException;
                }
                else
                {
                    throw error;
                }
            }
        }

        private void setVehicleField(object i_ManufacturedVehicle, string i_FieldName, object i_Value)
        {
            VehicleProperties propertiesDTO = new VehicleProperties(i_ManufacturedVehicle);

            try
            {
                propertiesDTO.SetVehicleProperty(i_FieldName, i_Value);
            }
            catch (Exception error)
            {
                if (error.InnerException != null)
                {
                    throw error.InnerException;
                }
                else
                {
                    throw error;
                }
            }
        }

        private void setField(object i_ManufacturedVehicle, string i_FieldName, object i_Value, bool i_IsInfo)
        {
            if (i_IsInfo)
            {
                setInfoField(i_ManufacturedVehicle, i_FieldName, i_Value);
            }
            else
            {
                setVehicleField(i_ManufacturedVehicle, i_FieldName, i_Value);
            }
        }

        private void addNewVehicleToGarage(Garage.GarageManager i_GarageManager, VehicleFactory i_VehicleFactory)
        {
            bool successfullyInserted = !v_SuccesfullyInserted;
            string licensePlateInput;
            string vehicleTypeInput;
            Owner vehicleOwner;

            while (!successfullyInserted)
            {
                try
                {
                    licensePlateInput = readVehicleLicenseAndVerify(i_GarageManager);
                    vehicleTypeInput = readVehicleType(i_VehicleFactory.GetTypesOfVehicles());
                    readVehicleInfo(licensePlateInput, vehicleTypeInput, i_VehicleFactory, out object manufacturedVehicle);
                    vehicleOwner = readOwnerInfo();
                    i_GarageManager.AddNewVehicleToGarage(manufacturedVehicle, vehicleOwner);
                    Console.WriteLine("Succesfully inserted!");
                    successfullyInserted = v_SuccesfullyInserted;
                }
                catch (Exception error)
                {
                    if (error.InnerException != null)
                    {
                        HandleError($"{error.InnerException.Message}");
                    }
                    else
                    {
                        HandleError($"{error.Message}");
                    }
                }
            }
        }

        private void readVehicleInfo(string i_LicensePlate, string i_VehicleTypeInput, VehicleFactory i_VehicleFactory, out object io_ManufacturedVehicle)
        {
            bool successfullyRead = !v_SuccesfullyRead;
            io_ManufacturedVehicle = null;

            while (!successfullyRead)
            {
                io_ManufacturedVehicle = i_VehicleFactory.ManufactureVehicle(i_VehicleTypeInput, i_LicensePlate);
                List<FieldClassInfo> listOfFieldInfos = i_VehicleFactory.GetFieldInfosOfVehicle(io_ManufacturedVehicle);

                try
                {
                    for (int i = 0; i < listOfFieldInfos.Count; i++)
                    {
                        if (listOfFieldInfos[i].FieldName.Equals("ListOfWheels"))
                        {
                            handleFieldsInformation(io_ManufacturedVehicle, listOfFieldInfos[i + 1]);
                            handleFieldsInformation(io_ManufacturedVehicle, listOfFieldInfos[i + 2]);
                            i += 2;
                        }
                        else
                        {
                            handleFieldsInformation(io_ManufacturedVehicle, listOfFieldInfos[i]);
                        }
                    }

                    successfullyRead = v_SuccesfullyRead;
                }
                catch (Exception error)
                {
                    if (error.InnerException != null)
                    {
                        HandleError($"{error.InnerException.Message}");
                    }
                    else
                    {
                        HandleError($"{error.Message}");
                    }
                }
            }
        }

        private Owner readOwnerInfo()
        {
            bool validOwnerInfo = !k_ValidOwnerInfo;
            Owner newOwner = new Owner();
            List<FieldClassInfo> listOfFields = newOwner.GetFieldsInfo();
            List<string> userOutputs = new List<string>(listOfFields.Count);

            while (!validOwnerInfo)
            {
                userOutputs.Clear();
                try
                {
                    foreach (FieldClassInfo fieldInfo in newOwner.GetFieldsInfo())
                    {
                        userOutputs.Add(r_InputReader.ReadInput(fieldInfo.PromptMessage));
                    }

                    newOwner.Name = userOutputs[0];
                    newOwner.PhoneNumber = userOutputs[1];
                    validOwnerInfo = k_ValidOwnerInfo;
                }
                catch (Exception error)
                {
                    if (error.InnerException != null)
                    {
                        HandleError($"{error.InnerException.Message}");
                    }
                    else
                    {
                        HandleError($"{error.Message}");
                    }
                }
            }

            return newOwner;
        }

        private string readVehicleType(FieldClassInfo i_VehicleTypesFieldInfo)
        {
            string userOutput;

            userOutput = handleFieldWithOptions(i_VehicleTypesFieldInfo);

            return userOutput;
        }

        private string readVehicleLicenseAndVerify(Garage.GarageManager i_GarageManager)
        {
            string licensePlateInput;

            licensePlateInput = r_InputReader.ReadInput(r_InitilaizedPrompts["LicensePlatePrompt"].PromptMessage);
            i_GarageManager.VerifyVehicleNotInGarage(licensePlateInput);

            return licensePlateInput;
        }

        private void displayLicenses(Garage.GarageManager i_GarageManager)
        {
            string filterStatus;
            LicensePlatesDTO licensePlates;

            r_OutputPrinter.PrintOptionsList(r_InitilaizedPrompts["FilteredStatusInGaragePrompt"].Options);
            filterStatus = r_InputReader.ReadInput(r_InitilaizedPrompts["FilteredStatusInGaragePrompt"].PromptMessage);
            licensePlates = i_GarageManager.FilterByStatus(filterStatus, out string parsedFilterStatus);
            r_OutputPrinter.PrintLicensePlates(parsedFilterStatus, licensePlates);
        }

        private void updateVehicleState(Garage.GarageManager i_GarageManager)
        {
            string licensePlate, newState, oldState;

            licensePlate = r_InputReader.ReadInput(r_InitilaizedPrompts["LicensePlatePrompt"].PromptMessage);
            r_OutputPrinter.PrintOptionsList(r_InitilaizedPrompts["StatusInGaragePrompt"].Options);
            newState = r_InputReader.ReadInput(r_InitilaizedPrompts["StatusInGaragePrompt"].PromptMessage);
            oldState = i_GarageManager.UpdateVehicleState(licensePlate, newState);
            r_OutputPrinter.PrintUpdatedStatusMessage(licensePlate, oldState, Enum.GetName(typeof(VehicleRecord.eVehicleStatus), int.Parse(newState)));
        }

        private void inflateVehicleTires(Garage.GarageManager i_GarageManager)
        {
            string licensePlate = r_InputReader.ReadInput(r_InitilaizedPrompts["LicensePlatePrompt"].PromptMessage);

            i_GarageManager.InflateVehicleTiresToMaximum(licensePlate);
            r_OutputPrinter.PrintSuccesfullyInflatedMessage(licensePlate);
        }

        private void fillFuel(Garage.GarageManager i_GarageManager)
        {
            string licensePlate, fuelType, fuelAmountInput;
            float newAmount;

            licensePlate = r_InputReader.ReadInput(r_InitilaizedPrompts["LicensePlatePrompt"].PromptMessage);
            r_OutputPrinter.PrintOptionsList(r_InitilaizedPrompts["FuelTypesPrompt"].Options);
            fuelType = r_InputReader.ReadInput(r_InitilaizedPrompts["FuelTypesPrompt"].PromptMessage);
            fuelAmountInput = r_InputReader.ReadInput(r_InitilaizedPrompts["FuelAmountPrompt"].PromptMessage);
            r_InputValidator.TryParsingToFloat(fuelAmountInput, out object fuelAmount);
            newAmount = i_GarageManager.FillFuel(licensePlate, fuelType, (float)fuelAmount);
            r_OutputPrinter.PrintSuccesfullyFillOfFuelMessage(licensePlate, fuelAmountInput, newAmount);
        }

        private void chargeBattery(Garage.GarageManager i_GarageManager)
        {
            string licensePlate, chargingTimeInput;
            float newAmount;

            licensePlate = r_InputReader.ReadInput(r_InitilaizedPrompts["LicensePlatePrompt"].PromptMessage);
            chargingTimeInput = r_InputReader.ReadInput(r_InitilaizedPrompts["BatteryAmountPrompt"].PromptMessage);
            r_InputValidator.TryParsingToFloat(chargingTimeInput, out object chargingTime);
            newAmount = i_GarageManager.ChargeBatteryAndValidateNegativity(licensePlate, (float)chargingTime);
            r_OutputPrinter.PrintSuccesfullyChargeOfBatteryMessage(licensePlate, (float)chargingTime, newAmount);

        }

        private void displayVehicleInfo(Garage.GarageManager i_GarageManager)
        {
            string licensePlate;
            VehicleInfoDTO vehicleInfo;

            licensePlate = r_InputReader.ReadInput(r_InitilaizedPrompts["LicensePlatePrompt"].PromptMessage);
            vehicleInfo = i_GarageManager.GetVehicleInfo(licensePlate);
            Console.Write(vehicleInfo.ToString());
        }
    }
}