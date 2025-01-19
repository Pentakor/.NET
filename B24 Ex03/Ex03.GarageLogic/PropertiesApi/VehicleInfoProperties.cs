using Ex03.GarageLogic.Vehicles;
using Ex03.GarageLogic.Vehicles.Factory;
using System;
using System.Reflection;

namespace Ex03.GarageLogic.PropetiesApi
{
    public class VehicleInfoProperties
    {
        private Vehicle m_Vehicle;
        private object m_SpecificVehicleInfo;
        private const bool v_NonPublic = true;

        public VehicleInfoProperties(object i_Vehicle)
        {
            PropertyInfo VehicleInfoProperty;

            m_Vehicle = VehicleFactory.CastToVehicle(i_Vehicle);
            VehicleInfoProperty = m_Vehicle.GetType().GetProperty("Info", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            m_SpecificVehicleInfo = VehicleInfoProperty?.GetGetMethod(v_NonPublic)?.Invoke(m_Vehicle, null);
        }

        public void SetVehicleProperty(string i_FieldName, object i_Value)
        {
            setInfoProperty(i_FieldName, i_Value);
        }

        private void setInfoProperty(string i_FieldName, object i_Value)
        {
            if (m_SpecificVehicleInfo != null)
            {
                setProperty(i_FieldName, i_Value);
            }
            else
            {
                throw new InvalidOperationException("Vehicle info is not available.");
            }
        }

        private void setProperty(string i_FieldName, object i_Value)
        {
            ParameterInfo parameterInfo;
            MethodInfo methodInfo;
            PropertyInfo desiredProperty;
            object convertedValue;

            if (m_SpecificVehicleInfo != null)
            {
                methodInfo = m_SpecificVehicleInfo.GetType().GetMethod(i_FieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Instance);
                if (methodInfo != null)
                {
                    parameterInfo = methodInfo.GetParameters()[0];
                    convertedValue = Convert.ChangeType(i_Value, parameterInfo.ParameterType);
                    methodInfo.Invoke(m_SpecificVehicleInfo, new object[] { convertedValue });
                }
                else
                {
                    desiredProperty = m_SpecificVehicleInfo.GetType().GetProperty(i_FieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                    if (desiredProperty != null && desiredProperty.CanWrite)
                    {
                        convertedValue = Convert.ChangeType(i_Value, desiredProperty.PropertyType);
                        desiredProperty.GetSetMethod(v_NonPublic)?.Invoke(m_SpecificVehicleInfo, new object[] { convertedValue });
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Vehicle info is not available.");
            }
        }
    }
}