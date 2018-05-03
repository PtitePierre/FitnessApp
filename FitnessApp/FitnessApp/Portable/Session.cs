using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    /*
    enum weight_unit
    {
        g,
        kg,
        lb
    }
    */

    public class Session
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public Unit SUnit { get; set; }
        public DateTime SDate { get; set; }
        public SportType SType { get; set; }
        public bool Done { get; set; }
        
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
                str = SType + " : " + Quantity + " " + SUnit.Code + "  " + countdown;
            }
            return str;
        }

        public string ToStringClassic()
        {
            return SType+ " : " + Quantity + " " + SUnit.Code + "  " + SDate;
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

            if(String.Compare(this.SType.Name, compareSession.SType.Name) == 0)
                return DateTime.Compare(this.SDate, compareSession.SDate);
            else
                return String.Compare(this.SType.Name, compareSession.SType.Name);
        }
    }
}
