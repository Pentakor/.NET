using Ex03.GarageLogic.Vehicles.Factory;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.Car
{
    internal class FueledCar : FueledVehicle
    {
        private const float k_MaxFuelCapacity = 45f;

        internal FueledCar()
        {
            InitializeWheels((int)VehicleFactory.eNumOfWheels.Car, (float)VehicleFactory.eMaxWheelAirPressure.Car);
            InitializeFuelInfo(eFuelType.Octan95, k_MaxFuelCapacity);
        }
    }
}