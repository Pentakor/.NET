using Ex03.GarageLogic.DTO;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.Car
{
    internal class CarInfo : VehicleInfo
    {
        private eColor m_Color;
        private eNumOfDoors m_NumOfDoors;
        private const bool v_IsInfo = true;

        internal string Color
        {
            get { return m_Color.ToString(); }
            set { setCarColor(value.ToString()); }
        }

        internal string NumOfDoors
        {
            get { return m_NumOfDoors.ToString(); }
            set { setNumberOfDoors(uint.Parse(value) + 1); }
        }

        private void setNumberOfDoors(uint i_NumOfDoors)
        {
            m_NumOfDoors = parseNumOfDoors(i_NumOfDoors);
        }

        private eNumOfDoors parseNumOfDoors(uint i_ChoosedNumOfDoors)
        {         
            if (!uint.TryParse(i_ChoosedNumOfDoors.ToString(), out uint vehicleNumberOfDoors))
            {
                throw new ValueOutOfRangeException("Number of doors choice is between",
                                              Enum.GetValues(typeof(eNumOfDoors)).Cast<uint>().Min(),
                                              Enum.GetValues(typeof(eNumOfDoors)).Cast<uint>().Max());
            }

            if (!Enum.IsDefined(typeof(eNumOfDoors), vehicleNumberOfDoors))
            {
                throw new ArgumentException("Invalid number of doors number.");
            }

            return (eNumOfDoors)vehicleNumberOfDoors;
        }

        private void setCarColor(string i_Color)
        {
            m_Color = parseCarColor(i_Color);
        }

        private eColor parseCarColor(string i_Color)
        {
            uint returnValue;

            if (!uint.TryParse(i_Color, out returnValue))
            {
                throw new ValueOutOfRangeException("Colors choice is between",
                                       Enum.GetValues(typeof(eColor)).Cast<uint>().Min(),
                                       Enum.GetValues(typeof(eColor)).Cast<uint>().Max());
            }

            if (!Enum.IsDefined(typeof(eColor), returnValue))
            {
                throw new ArgumentException("Invalid color optional choose.");
            }

            return (eColor)returnValue;
        }

        internal enum eNumOfDoors : uint
        {
            TwoDoors = 2,
            ThreeDoors = 3,
            FourDoors = 4,
            FiveDoors = 5,
        }

        internal enum eColor : uint
        {
            White = 1,
            Yellow,
            Red,
            Black
        }

        private List<string> getCarColorOptions()
        {
            List<string> colorOptions = new List<string>
            {
                eColor.White.ToString(),
                eColor.Yellow.ToString(),
                eColor.Red.ToString(),
                eColor.Black.ToString()
            };

            return colorOptions;
        }

        private List<string> getNumOfDoorsOptions()
        {
            List<string> numOfDoorsOptions = new List<string>
            {
                eNumOfDoors.TwoDoors.ToString(),
                eNumOfDoors.ThreeDoors.ToString(),
                eNumOfDoors.FourDoors.ToString(),
                eNumOfDoors.FiveDoors.ToString()
            };

            return numOfDoorsOptions;
        }

        internal override List<FieldClassInfo> GetFieldsInfos()
        {
            List<FieldClassInfo> fieldsInfo = base.GetFieldsInfos();

            fieldsInfo.AddRange(new List<FieldClassInfo>
            {
                new FieldClassInfo("Color", "Please enter your car's color from the list: ", FieldClassInfo.eInputType.Int , v_IsInfo, getCarColorOptions()),
                new FieldClassInfo("NumOfDoors", "Please enter your number of car's doors from the list: ", FieldClassInfo.eInputType.Int, v_IsInfo, getNumOfDoorsOptions())
            });

            return fieldsInfo;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("Car Details:");
            stringBuilder.AppendLine($"Car color: {m_Color}");
            stringBuilder.AppendLine($"Car's amount of doors: {(uint)m_NumOfDoors}");

            return stringBuilder.ToString();
        }
    }
}