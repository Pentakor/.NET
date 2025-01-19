using Ex03.GarageLogic.DTO;
using Ex03.GarageLogic.Vehicles.Types.Objects.Car;
using Ex03.GarageLogic.Vehicles.Types.Objects.MotorCycle;
using Ex03.GarageLogic.Vehicles.Types.Objects.Truck;
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic.Vehicles.Factory
{
    public class VehicleFactory
    {
        private const bool v_IsInfo = true;

        internal enum eNumOfWheels
        {
            MotorCycle = 2,
            Car = 5,
            Truck = 12
        }

        internal enum eMaxWheelAirPressure
        {
            MotorCycle = 33,
            Car = 31,
            Truck = 28
        }

        internal enum eVehicleType : uint
        {
            FueledMotorCycle = 1,
            ElectricMotorCycle,
            FueledCar,
            ElectricCar,
            FueledTruck
        }

        public FieldClassInfo GetTypesOfVehicles()
        {
            return new FieldClassInfo("VehicleTypes", "Please enter your desired vehicle type: ", FieldClassInfo.eInputType.Int, !v_IsInfo, new List<string> 
            {
                "Fueled Motorcycle",
                "Electric Motorcycle",
                "Fueled Car",
                "Electric Car",
                "Fueled Truck"
            });
        }

        public object ManufactureVehicle(string i_VehicleType, string i_LicensePlate)
        {
            eVehicleType vehicleType = parseVehicleType(i_VehicleType);
            Vehicle manufacturedVehicle;

            switch (vehicleType)
            {
                case eVehicleType.FueledMotorCycle:
                    manufacturedVehicle = new FueledMotorCycle();
                    break;
                case eVehicleType.ElectricMotorCycle:
                    manufacturedVehicle = new ElectricMotorCycle();
                    break;
                case eVehicleType.FueledCar:
                    manufacturedVehicle = new FueledCar();
                    break;
                case eVehicleType.ElectricCar:
                    manufacturedVehicle = new ElectricCar();
                    break;
                case eVehicleType.FueledTruck:
                    manufacturedVehicle = new FueledTruck();
                    break;
                default:
                    manufacturedVehicle = null;
                    break;
            }

            initializeVehicleInfo(manufacturedVehicle, i_LicensePlate, vehicleType);

            return manufacturedVehicle;
        }

        private void initializeVehicleInfo(Vehicle io_Vehicle, string i_LicensePlate, eVehicleType i_VehicleType)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.FueledMotorCycle:
                case eVehicleType.ElectricMotorCycle:
                    io_Vehicle.Info = new MotorCycleInfo();
                    break;
                case eVehicleType.FueledCar:
                case eVehicleType.ElectricCar:
                    io_Vehicle.Info = new CarInfo();
                    break;
                case eVehicleType.FueledTruck:
                    io_Vehicle.Info = new TruckInfo();
                    break;
                default:
                    break;
            }

            io_Vehicle.Info.LicencePlate = i_LicensePlate;
        }

        internal static Vehicle CastToVehicle(object i_NewVehicle)
        {
            if (i_NewVehicle is Vehicle vehicle)
            {
                return vehicle;
            }
            else
            {
                throw new InvalidCastException("The provided object is not of type Vehicle.");
            }
        }

        public List<FieldClassInfo> GetFieldInfosOfVehicle(object i_ManufacturedVehicle)
        {
            Vehicle newVehicle = CastToVehicle(i_ManufacturedVehicle);

            return newVehicle.GetFieldsList();
        }

        private eVehicleType parseVehicleType(string i_VehicleTypeInput)
        {
            if (!uint.TryParse(i_VehicleTypeInput, out uint vehicleTypeAsNumber))
            {
                throw new ArgumentException("Invalid vehicle type input. Input must be a valid number.");
            }

            if (!Enum.IsDefined(typeof(eVehicleType), vehicleTypeAsNumber))
            {
                throw new ArgumentException("Invalid vehicle type.");
            }

            return (eVehicleType)vehicleTypeAsNumber;
        }
    }
}