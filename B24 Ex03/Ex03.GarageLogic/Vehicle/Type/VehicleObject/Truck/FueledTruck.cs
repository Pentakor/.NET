using Ex03.GarageLogic.Vehicles.Factory;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.Truck
{
    internal class FueledTruck : FueledVehicle
    {
        private const float k_MaxFuelCapacity = 120f;

        internal FueledTruck()
        {
            InitializeWheels((int)VehicleFactory.eNumOfWheels.Truck, (float)VehicleFactory.eMaxWheelAirPressure.Truck);
            InitializeFuelInfo(eFuelType.Diesel, k_MaxFuelCapacity);
        }
    }
}