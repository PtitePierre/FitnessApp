using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    public class Unit
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        override
        public string ToString()
        {
            return Code + " : " + Name;
        }
    }
}
