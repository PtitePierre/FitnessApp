using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DoB { get; set; }
        public float Weight { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
    }
}
