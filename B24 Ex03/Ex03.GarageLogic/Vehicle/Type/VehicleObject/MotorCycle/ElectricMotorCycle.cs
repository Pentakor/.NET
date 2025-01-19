using Ex03.GarageLogic.Vehicles.Factory;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.MotorCycle
{
    internal class ElectricMotorCycle : ElectricVehicle
    {
        private const float k_MaxBatteryTime = 2.5f;

        internal ElectricMotorCycle()
        {
            InitializeWheels((int)VehicleFactory.eNumOfWheels.MotorCycle, (float)VehicleFactory.eMaxWheelAirPressure.MotorCycle);
            InitializeMaxBattery(k_MaxBatteryTime);
        }
    }
}
