using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    public class Session
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public DateTime Dt { get; set; }
        public SportType Type { get; set; }

    }
}
