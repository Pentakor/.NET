using Ex03.GarageLogic.Vehicles.Factory;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.MotorCycle
{
    internal class FueledMotorCycle : FueledVehicle
    {
        private const float k_MaxFuelCapacity = 5.5f;

        internal FueledMotorCycle()
        {
            InitializeWheels((int)VehicleFactory.eNumOfWheels.MotorCycle, (float)VehicleFactory.eMaxWheelAirPressure.MotorCycle);
            InitializeFuelInfo(eFuelType.Octan98, k_MaxFuelCapacity);
        }
    }
}