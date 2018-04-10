using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Linq;

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

        public static string LoadText(string filename, System.Reflection.Assembly assembly)
        {

            #region How to load a text file embedded resource
            
            //var assembly = this.GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(filename, StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);

            string text = "";
            try
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().shorttime(e.Message);
                text = "Echec de chargement.";
            }
            finally
            {
                DependencyService.Get<IMessage>().shorttime("End of loading");
            }
            #endregion

            return text;
        }

    }
}
