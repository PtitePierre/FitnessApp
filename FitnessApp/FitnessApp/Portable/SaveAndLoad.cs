using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Linq;

namespace FitnessApp.Portable
{
    public static class SaveAndLoad
    {
        public static List<Session> Sessions { get; set; }
        public static List<Unit> Units { get; set; }
        public static List<SportType> Sports { get; set; }

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

        public static List<Unit> LoadUnits(System.Reflection.Assembly assembly)
        {
            if (Units == null || Units.Count() == 0)
            {
                // TO DO : Fill units
                // open unit.json file
                // deserialize json object got from unit.json

                string name = "unit.json";

                Units = LoadList<Unit>(name, assembly);
            }

            return Units;
        }

        public static List<SportType> LoadSportTypes(System.Reflection.Assembly assembly)
        {
            if (Sports == null || Sports.Count() == 0)
            {
                // TO DO : Fill units
                // open unit.json file
                // deserialize json object got from unit.json

                string name = "sport.json";

                Sports = LoadList<SportType>(name, assembly);
            }

            return Sports;
        }

        public static List<Session> LoadSessions(System.Reflection.Assembly assembly)
        {
            if (Sessions == null || Sessions.Count() == 0)
            {
                // TO DO : Fill units
                // open unit.json file
                // deserialize json object got from unit.json

                string name = "session.json";

                Sessions = LoadList<Session>(name, assembly);
            }

            // TO DO : Fill sessions

            return Sessions;
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

        public static List<T> LoadList<T>(string filename, System.Reflection.Assembly assembly)
        {
            List<T> list = new List<T>();

            #region How to load an Json file embedded resource

            //string name = "sport.json";
            //var assembly = this.GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(filename, StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);

            try
            {
                using (var reader = new System.IO.StreamReader(stream))
                {

                    var json = reader.ReadToEnd();
                    var rootobject = JsonConvert.DeserializeObject<List<T>>(json);

                    list = rootobject;
                }

            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime(e.Message);
            }
            finally
            {
                DependencyService.Get<IMessage>().shorttime("End of loading");
            }

            #endregion

            return list;
        }

    }
}
