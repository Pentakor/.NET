using Ex03.GarageLogic.DTO;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Vehicles;
using Ex03.GarageLogic.Vehicles.Factory;
using Ex03.GarageLogic.Vehicles.Types;
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic.GarageManager
{
    public class Garage
    {
        private readonly Dictionary<string, Vehicle> r_VehiclesInGarage;
        private readonly GarageManager r_GarageManager;

        public Garage()
        {
            r_VehiclesInGarage = new Dictionary<string, Vehicle>();
            r_GarageManager = new GarageManager(this);
        }

        public class GarageManager
        {
            private readonly Garage r_Garage;
            private readonly Dictionary<string, VehicleRecord> r_VehiclesRecords;
            private const string k_AllOption = "4";

            public GarageManager(Garage i_Garage)
            {
                r_Garage = i_Garage;
                r_VehiclesRecords = new Dictionary<string, VehicleRecord>();
            }

            public Dictionary<string, FieldClassInfo> GetInitializePrompts()
            {
                Dictionary<string, FieldClassInfo> fieldsInfo = new Dictionary<string, FieldClassInfo>();

                fieldsInfo.Add("LicensePlatePrompt", askForLicenseToRegister());
                fieldsInfo.Add("FilteredStatusInGaragePrompt", askForFilterStatus());
                fieldsInfo.Add("StatusInGaragePrompt", askForVehicleStatuses());
                fieldsInfo.Add("FuelTypesPrompt", askForFuelTypes());
                fieldsInfo.Add("FuelAmountPrompt", askForFuelAmount());
                fieldsInfo.Add("BatteryAmountPrompt", askForBatteryAmount());

                return fieldsInfo;
            }

            private FieldClassInfo askForLicenseToRegister()
            {
                return new FieldClassInfo("LicensePlatePrompt", "Please enter vehicle's license plate: ");
            }

            private FieldClassInfo askForFilterStatus()
            {
                List<string> filterlist = VehicleRecord.GetVehicleStatusesOptions();
                filterlist.Add("All");

                return new FieldClassInfo("FilteredStatusInGaragePrompt", "Please enter vehicle's status: ", filterlist);
            }

            private FieldClassInfo askForVehicleStatuses()
            {
                return new FieldClassInfo("StatusInGaragePrompt", "Please enter vehicle's new state: ", VehicleRecord.GetVehicleStatusesOptions());
            }

            private FieldClassInfo askForFuelAmount()
            {
                return new FieldClassInfo("FuelAmountPrompt", "Please enter the amount of fuel (In Litres) you would like to fill: ");
            }

            private FieldClassInfo askForBatteryAmount()
            {
                return new FieldClassInfo("BatteryAmountPrompt", "Please enter the number of minutes you would like to charge the vehicle: ");
            }

            private FieldClassInfo askForFuelTypes()
            {
                return new FieldClassInfo("FuelTypesPrompt", "Please enter the type of fuel you would like to fill: ", FueledVehicle.GetFueledVehicleFuelTypesOptions());
            }

            public void AddNewVehicleToGarage(object i_NewVehicle, Owner i_VehicleOwner)
            {
                Vehicle newVehicle = VehicleFactory.CastToVehicle(i_NewVehicle);
                string newVehicleLicensePlate = newVehicle.Info.LicencePlate;

                r_Garage.r_VehiclesInGarage.Add(newVehicleLicensePlate, newVehicle);
                r_VehiclesRecords.Add(newVehicleLicensePlate, new VehicleRecord(i_VehicleOwner));
            }

            public LicensePlatesDTO FilterByStatus(string i_FilterStatus, out string io_ParsedFilterStatus)
            {
                string status;
                LicensePlatesDTO returnValue = new LicensePlatesDTO();

                if (i_FilterStatus.Equals(k_AllOption))
                {
                    foreach (KeyValuePair<string, VehicleRecord> kvp in r_VehiclesRecords)
                    {
                        status = kvp.Value.VehicleStatus;
                        if (!returnValue.LicensePlatesDict.ContainsKey(status))
                        {
                            returnValue.LicensePlatesDict[status] = new List<string>();
                        }

                        returnValue.LicensePlatesDict[status].Add(kvp.Key);
                    }
                    io_ParsedFilterStatus = "All";
                }
                else
                {
                    io_ParsedFilterStatus = parseVehicleStatus(i_FilterStatus).ToString();
                    foreach (KeyValuePair<string, VehicleRecord> kvp in r_VehiclesRecords)
                    {
                        if (kvp.Value.VehicleStatus == io_ParsedFilterStatus.ToString())
                        {
                            if (!returnValue.LicensePlatesDict.ContainsKey(kvp.Value.VehicleStatus))
                            {
                                returnValue.LicensePlatesDict[kvp.Value.VehicleStatus] = new List<string>();
                            }

                            returnValue.LicensePlatesDict[kvp.Value.VehicleStatus].Add(kvp.Key);
                        }
                    }
                }

                return returnValue;
            }

            public string UpdateVehicleState(string i_LicensePlate, string i_NewVehicleStatusInput)
            {
                getVehicleRecordByLicensePlate(i_LicensePlate, out VehicleRecord desiredVehicleRecord);
                string oldStatus = desiredVehicleRecord.VehicleStatus.ToString();
                VehicleRecord.eVehicleStatus newStatus = parseVehicleStatus(i_NewVehicleStatusInput);

                if (oldStatus == newStatus.ToString())
                {
                    throw new ArgumentException($"Vehicle status is already {oldStatus}.");
                }
                desiredVehicleRecord.VehicleStatus = newStatus.ToString();

                return oldStatus;
            }

            public void InflateVehicleTiresToMaximum(string i_LicensePlate)
            {
                getVehicleSpecificationByLicensePlate(i_LicensePlate, out Vehicle desiredVehicle);
                desiredVehicle.InflateTiresToMaximumAirPressure();
            }

            public float FillFuel(string i_LicensePlate, string i_FuelType, float i_FuelAmountToAdd)
            {
                FueledVehicle fueledVehicle;
                FueledVehicle.eFuelType fuelType;

                getVehicleSpecificationByLicensePlate(i_LicensePlate, out Vehicle io_desiredVehicle);
                if (io_desiredVehicle is ElectricVehicle)
                {
                    throw new ArgumentException($"Vehicle licensed {i_LicensePlate} is an electric vehicle.");
                }

                fueledVehicle = io_desiredVehicle as FueledVehicle;
                fuelType = fueledVehicle.ParseFuelType(i_FuelType);
                fueledVehicle.AddFuelAndValidateType(i_FuelAmountToAdd, fuelType.ToString());

                return fueledVehicle.RemainingFuel;
            }

            public float ChargeBatteryAndValidateNegativity(string i_LicensePlate, float i_ChargingTimeInMinutes)
            {
                ElectricVehicle electricVehicle;

                getVehicleSpecificationByLicensePlate(i_LicensePlate, out Vehicle desiredVehicle);
                if (desiredVehicle is FueledVehicle)
                {
                    throw new ArgumentException($"Vehicle licensed {i_LicensePlate} is a fueled vehicle.");
                }

                electricVehicle = desiredVehicle as ElectricVehicle;
                electricVehicle.ChargeBatteryAndValidateNegativity(i_ChargingTimeInMinutes);

                return electricVehicle.RemainingBatteryTime;
            }

            public VehicleInfoDTO GetVehicleInfo(string i_LicensePlate)
            {
                getVehicleSpecificationByLicensePlate(i_LicensePlate, out Vehicle desiredVehicle);
                getVehicleRecordByLicensePlate(i_LicensePlate, out VehicleRecord desiredVehicleRecord);

                return new VehicleInfoDTO
                {
                    VehicleRecordInfo = desiredVehicleRecord.ToString(),
                    VehicleSpecification = desiredVehicle.ToString()
                };
            }

            public void VerifyVehicleNotInGarage(string i_LicensePlate)
            {
                if (isVehicleInGarage(i_LicensePlate))
                {
                    r_VehiclesRecords[i_LicensePlate].VehicleStatus = VehicleRecord.eVehicleStatus.InRepair.ToString();
                    throw new VehicleAlreadyExistsException(i_LicensePlate);
                }
            }

            private void getVehicleRecordByLicensePlate(string i_LicensePlate, out VehicleRecord o_DesiredVehicleRecord)
            {
                if (!r_VehiclesRecords.TryGetValue(i_LicensePlate, out o_DesiredVehicleRecord))
                {
                    throw new ArgumentException($"Vehicle licensed {i_LicensePlate} doesn't exist in our garage!");
                }
            }

            private void getVehicleSpecificationByLicensePlate(string i_LicensePlate, out Vehicle o_DesiredVehicle)
            {
                if (!r_Garage.r_VehiclesInGarage.TryGetValue(i_LicensePlate, out o_DesiredVehicle))
                {
                    throw new ArgumentException($"Vehicle licensed {i_LicensePlate} doesn't exist in our garage!");
                }
            }

            private VehicleRecord.eVehicleStatus parseVehicleStatus(string i_Status)
            {
                if (!uint.TryParse(i_Status, out uint vehicleTypeAsNumber))
                {
                    throw new ArgumentException("Invalid vehicle status input. Input must be a valid number.");
                }

                if (!Enum.IsDefined(typeof(VehicleRecord.eVehicleStatus), vehicleTypeAsNumber))
                {
                    throw new ArgumentException("Invalid vehicle status.");
                }

                return (VehicleRecord.eVehicleStatus)vehicleTypeAsNumber;
            }

            private bool isVehicleInGarage(string i_LicensePlate) => r_VehiclesRecords.ContainsKey(i_LicensePlate);
        }
    }
}