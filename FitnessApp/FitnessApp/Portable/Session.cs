using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    
    public enum weight_unit
    {
        kg,
        g,
        lb
    }

    public class Session
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public Unit SUnit { get; set; }
        public DateTime SDate { get; set; }
        public SportType SType { get; set; }
        public bool Done { get; set; }
        public float Weight { get; set; }
        public weight_unit WUnit { get; set; }
        public bool Saved { get; set; }

        public Session()
        {
            // default
            Weight = 0;
            WUnit = weight_unit.kg;
            Saved = false;
        }
        
        override
        public string ToString()
        {
            string str;
            if (Done)
            {
                str = ToStringClassic();
            }
            else
            {
                //StringFormat = '{0:MMMM dd, yyyy}'  "DD HH:mm"
                string format = @"dd\d\ hh\:mm\:ss";
                string countdown = (SDate - DateTime.Now).ToString(format);
                str = SType.Name + " : " + Quantity + " " + SUnit.Code + "  " + countdown;
            }
            return str;
        }

        public string ToStringClassic()
        {
            return SType.Name + " : " + Quantity + " " + SUnit.Code + "  " + SDate;
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
    }
}
