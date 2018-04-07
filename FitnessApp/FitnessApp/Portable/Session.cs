using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    class Session
    {
        private int Id { get; set; }
        private int Quantity { get; set; }
        private Unit Unit { get; set; }
        private DateTime Dt { get; set; }
        private SportType Type { get; set; }

    }
}
