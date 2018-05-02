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
            return SType+ " : " + Quantity + " " + SUnit.Code + "  " + SDate;
        }
    }
}
