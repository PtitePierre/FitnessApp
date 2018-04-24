using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Linq;
using PCLStorage;
using System.Threading.Tasks;

namespace FitnessApp.Portable
{
    public class SaveAndLoad
    {
        static List<Unit> Units;
        static List<SportType> SportTypes;
        static List<Session> Sessions;

        static string unitFile = "unit.json";
        static string sportFile = "sport.json";
        static string sessionFile = "session.json";

        #region Loading part
        public async static Task<List<Unit>> LoadUnits()
        {
            try
            {
                Units = Units ?? await WSConsumer.GetUnits();
            }
            catch(Exception e)
            {
                Units = Units ?? await LoadList("unit.json", Units);
            }
            return Units;
        }

        public async static Task<List<SportType>> LoadSports()
        {
            try
            {
                SportTypes = SportTypes ?? await WSConsumer.GetSports();
            }
            catch (Exception e)
            {
                SportTypes = SportTypes ?? await LoadList("sport.json", SportTypes);
            }
            return SportTypes;
        }

        public async static Task<List<Session>> LoadSessions()
        {
            Sessions = Sessions ?? await LoadList("session.json", Sessions);
            return Sessions;
        }

        public async static Task<List<T>> LoadList<T>(string filename, List<T> list)
        {
            // open filename
            // read content
            // deserialize content as a list of T in list
            IFileSystem fileSystem = FileSystem.Current;
            IFolder folder = fileSystem.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);

            list = JsonConvert.DeserializeObject<List<T>>(await file.ReadAllTextAsync());

            return list;
        }
        #endregion

        #region Saving part
        public static void SaveUnit(Unit unit)
        {
            if (Units == null)
                Units = new List<Unit>();

            Units.Add(unit);
            SaveList(Units, unitFile);
        }

        public static void SaveSport(SportType sport)
        {
            if (SportTypes == null)
                SportTypes = new List<SportType>();

            SportTypes.Add(sport);
            SaveList(SportTypes, sportFile);
        }

        public static void SaveSession(Session session)
        {
            if (Sessions == null)
                Sessions = new List<Session>();

            Sessions.Add(session);
            SaveList(Sessions, sessionFile);
        }

        public async static Task<bool> SaveList<T>(List<T> list, string filename)
        {
            IFileSystem fileSystem = FileSystem.Current;
            IFolder folder = fileSystem.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);

            await file.WriteAllTextAsync(JsonConvert.SerializeObject(list));

            return true;
        }
        #endregion
    }
}
