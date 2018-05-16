using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
            user_id = SaveAndLoad.GetUser().Id;

            if(SUnit == null)
                SUnit = SaveAndLoad.GetUnit(sunit_id);
            if(SType == null)
                SType = SaveAndLoad.GetSport(stype_id);
        }

        override
        public string ToString()
        {
            string str;
            if (SDate < DateTime.Now)
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

        public Unit GetUnit()
        {
            if (SUnit == null)
                SUnit = SaveAndLoad.GetUnit(sunit_id);
            return SUnit;
        }

        public SportType GetSportType()
        {
            if (SType == null)
                SType = SaveAndLoad.GetSport(stype_id);
            return SType;
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

            string json = JsonConvert.SerializeObject(new SimpleSession(this));
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
                return String.Compare(this.GetSportType().Name, compareSession.GetSportType().Name);
            else
                return DateTime.Compare(this.SDate, compareSession.SDate);
        }

        public float GetUnitCoef()
        {
            if (SUnit == null)
                SUnit = SaveAndLoad.GetUnit(sunit_id);
            if (SType == null)
                SType = SaveAndLoad.GetSport(stype_id);


            return SUnit.GetCoef()*(1+Weight/10);
        }
    }


    public class SimpleSession
    {
        public int id { get; set; }
        public float quantity { get; set; }
        public string sdate { get; set; }
        public int done { get; set; }
        public float weight { get; set; }
        public string wunit { get; set; }
        public int user_id { get; set; }
        public int stype_id { get; set; }
        public int sunit_id { get; set; }

        public SimpleSession(Session src)
        {
            id = src.Id;
            quantity = src.Quantity;
            //sdate = src.SDate.ToString("s") + "Z";
            sdate = src.SDate.ToString("s");
            done = (src.Done) ? 1 : 0;
            weight = src.Weight;
            wunit = src.WUnit.ToLower();
            user_id = src.user_id;
            stype_id = src.SType.Id;
            sunit_id = src.SUnit.Id;
        }
    }
}
