using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    class User
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private string Username { get; set; }
        private DateTime CreationDate { get; set; }
        private DateTime DoB { get; set; }
        private float Weight { get; set; }
        private string Password { get; set; }
        private int Level { get; set; }
    }
}
