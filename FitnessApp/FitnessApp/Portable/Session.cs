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
    }
}
