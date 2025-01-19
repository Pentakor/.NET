using Ex03.GarageLogic.GarageManager;
using System;

namespace Ex03.GarageLogic.Exceptions
{
    internal class VehicleAlreadyExistsException : Exception
    {
        internal VehicleAlreadyExistsException(string i_LicensePlate)
       : base($"Vehicle licensed {i_LicensePlate} already exists in the garage, Updating state to {VehicleRecord.eVehicleStatus.InRepair}") { }    
    }
}