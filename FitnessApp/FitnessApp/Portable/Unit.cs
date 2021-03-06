﻿using System;
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

        public string ToStringBtn()
        {
            return Id + ". " + Code + " : " + Name;
        }

        public float GetCoef()
        {
            float coef;
            switch (Code)
            {
                case "it":
                    coef = 0.01f;
                    break;
                case "h":
                    coef = 6;
                    break;
                case "min":
                    coef = 0.1f;
                    break;
                case "sec":
                    coef = 0.01f;
                    break;
                case "m":
                    coef = 0.01f;
                    break;
                case "km":
                default:
                    coef = 1;
                    break;
            }
            return coef;
        }
    }
}
