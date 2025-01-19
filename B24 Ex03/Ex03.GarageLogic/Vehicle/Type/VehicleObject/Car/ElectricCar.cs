using Ex03.GarageLogic.Vehicles.Factory;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.Car
{
    internal class ElectricCar : ElectricVehicle
    {
        private const float k_MaxBatteryTime = 3.5f;

        internal ElectricCar()
        {
            InitializeWheels((int)VehicleFactory.eNumOfWheels.Car, (float)VehicleFactory.eMaxWheelAirPressure.Car);
            InitializeMaxBattery(k_MaxBatteryTime);
        }
    }
}