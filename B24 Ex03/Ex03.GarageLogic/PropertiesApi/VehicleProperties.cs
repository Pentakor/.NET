using Ex03.GarageLogic.Vehicles;
using Ex03.GarageLogic.Vehicles.Factory;
using System;
using System.Reflection;

namespace Ex03.GarageLogic.DTO
{
    public class VehicleProperties
    {
        private Vehicle m_Vehicle;
        private object m_SpecificVehicleInfo;
        private const bool v_NonPublic = true;

        public VehicleProperties(object i_Vehicle)
        {         
            PropertyInfo vehicleInfoType;

            m_Vehicle = VehicleFactory.CastToVehicle(i_Vehicle);
            vehicleInfoType = m_Vehicle.GetType().GetProperty("Info", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            m_SpecificVehicleInfo = vehicleInfoType?.GetGetMethod(v_NonPublic)?.Invoke(m_Vehicle, null);
        }

        public void SetVehicleProperty(string i_FieldName, object i_Value)
        {
            setProperty(m_Vehicle, i_FieldName, i_Value);
        }

        public void SetInfoProperty(string i_FieldName, object i_Value)
        {
            if (m_SpecificVehicleInfo != null)
            {
                setProperty(m_SpecificVehicleInfo, i_FieldName, i_Value);
            }
            else
            {
                throw new InvalidOperationException("Vehicle info is not available.");
            }
        }

        private void setProperty(object i_Object, string i_FieldName, object i_Value)
        {
            Type instanceType = i_Object.GetType();
            MethodInfo methodInfo = instanceType.GetMethod(i_FieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Instance);
            PropertyInfo propertyInfo;
            ParameterInfo parameterInfo;
            object convertedValue;

            if (methodInfo != null)
            {
                parameterInfo = methodInfo.GetParameters()[0];
                convertedValue = Convert.ChangeType(i_Value, parameterInfo.ParameterType);
                methodInfo.Invoke(i_Object, new object[] { convertedValue });
            }
            else
            {
                propertyInfo = instanceType.GetProperty(i_FieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    convertedValue = Convert.ChangeType(i_Value, propertyInfo.PropertyType);
                    propertyInfo.GetSetMethod(v_NonPublic)?.Invoke(i_Object, new object[] { convertedValue });
                }
            }
        }
    }
}