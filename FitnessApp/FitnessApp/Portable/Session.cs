using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    public class Session
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public Unit SUnit { get; set; }
        public DateTime SDate { get; set; }
        public SportType SType { get; set; }
        public bool Done { get; set; }
        public float Weight { get; set; }
        public string WUnit { get; set; }
        public bool Saved { get; set; }
        public int user_id { get; set; }
        public int stype_id { get; set; }
        public int sunit_id { get; set; }

        public Session()
        {
            // default
            Weight = 0;
            WUnit = "kg";
            Saved = false;
            user_id = SaveAndLoad.LoadUser().Id;

            if(SUnit == null)
                SUnit = SaveAndLoad.GetUnit(sunit_id);
            if(SType == null)
                SType = SaveAndLoad.GetSport(stype_id);
        }

        override
        public string ToString()
        {
            string str;
            if (SDate > DateTime.Now)
            {
                str = ToStringClassic();
            }
            else
            {
                if (SUnit == null)
                    SUnit = SaveAndLoad.GetUnit(sunit_id);
                if (SType == null)
                    SType = SaveAndLoad.GetSport(stype_id);

                string format = @"dd\d\ hh\:mm\:ss";
                string countdown = (SDate - DateTime.Now).ToString(format);
                str = ToStringClassic() + " : " + countdown;
            }
            return str;
        }

        public string ToStringClassic()
        {
            if (SUnit == null)
                SUnit = SaveAndLoad.GetUnit(sunit_id);
            if (SType == null)
                SType = SaveAndLoad.GetSport(stype_id);
            return SType.Name + " : " + Quantity + " " + SUnit.Code + "  " + SDate.ToShortDateString();
        }

        public string ToJson()
        {
            if (SUnit == null)
                SUnit = SaveAndLoad.GetUnit(sunit_id);
            if (SType == null)
                SType = SaveAndLoad.GetSport(stype_id);

            string json = "{\"stype_id\":"+ SType.Id
                +",\"sunit_id\":"+ SUnit.Id
                +",\"user_id\":"+ user_id
                +",\"sdate\":"+ JsonConvert.SerializeObject(SDate)
                +",\"quantity\":"+ Quantity
                +",\"weight\":"+ Weight
                +",\"wunit\":"+ WUnit
                +",\"done\":"+ Done +"}";


            return json;
        }

        /// <summary>
        /// Define the order between this and compareSession
        /// -1 : this is less than compareSession
        /// 0 : this and compareSession are equally ranked
        /// 1 : this is greater than compareSession
        /// </summary>
        /// <param name="compareSession"></param>
        /// <returns></returns>
        public int CompareTo(Session compareSession)
        {
            if(compareSession == null)
                return -1;

            if(DateTime.Compare(this.SDate, compareSession.SDate) == 0)
                return String.Compare(this.SType.Name, compareSession.SType.Name);
            else
                return DateTime.Compare(this.SDate, compareSession.SDate);
        }

        public float GetUnitCoef()
        {
            if (SUnit == null)
                SUnit = SaveAndLoad.GetUnit(sunit_id);
            if (SType == null)
                SType = SaveAndLoad.GetSport(stype_id);

            return SUnit.GetCoef();
        }
    }
}
