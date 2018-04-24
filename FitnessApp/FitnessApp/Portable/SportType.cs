using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    public class SportType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Unit> Units { get; set; }

        override
        public string ToString()
        {
            return Name;
        }
    }
}
