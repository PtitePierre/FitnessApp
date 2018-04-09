using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Xamarin.Forms;
using System.Reflection;
using System.IO;

namespace FitnessApp.Portable
{
    public static class DAO
    {
        public static void saveNewUnit()
        {
            // TO DO
            // save a new unit in unit.json
        }

        public static void saveNewSportType()
        {
            // TO DO
            // save a new sport type in sport.json
        }

        public static void saveSessions()
        {
            // TO DO
            // save all the sessions in session.json
        }

        public static void loadUnits(Unit[] units)
        {

            // TO DO : Fill units
            // open unit.json file
            // use it as jArray
            // for each element from the JArray
                // create an object of unit class


        }

        public static SportType[] loadSportTypes()
        {
            SportType[] sports = null;

            // TO DO : Fill sports

            return sports;
        }

        public static Session[] loadSessions()
        {
            Session[] sessions = null;

            // TO DO : Fill sessions

            return sessions;
        }

        
    }
}
