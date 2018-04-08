using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace FitnessApp.Portable
{
    class DAO
    {
        public void saveNewUnit()
        {
            // TO DO
            // save a new unit in unit.json
            
        }

        public void saveNewSportType()
        {
            // TO DO
            // save a new sport type in sport.json
        }

        public void saveSessions()
        {
            // TO DO
            // save all the sessions in session.json
        }

        public Unit[] loadUnits()
        {
            Unit[] units = null;

            // TO DO : Fill units from unit.json
            //JObject oUnit = JObject.Parse(File.ReadAllText("unit.json"));

            return units;
        }

        public SportType[] loadSportTypes()
        {
            SportType[] sports = null;

            // TO DO : Fill sports from sport.json

            return sports;
        }

        public Session[] loadSessions()
        {
            Session[] sessions = null;

            // TO DO : Fill sessions from session.json

            return sessions;
        }
    }
}
