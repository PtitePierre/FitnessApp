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
        static User user;

        static string unitFile = "unit.json";
        static string sportFile = "sport.json";
        static string sessionFile = "session.json";
        static string userFile = "user.json";

        #region Loading part
        /// <summary>
        /// Load in the list Units the list of units from the Web service
        /// Or from the local file if the WS is not available
        /// </summary>
        /// <param name="forced">force the loading from the web service even if Units contains something</param>
        /// <returns>the static list Units of Unit</returns>
        public async static Task<List<Unit>> LoadUnits(bool forced = false)
        {
            try
            {
                if (forced)
                {
                    Units = await WSConsumer.GetUnits();
                }
                else
                {
                    Units = Units ?? await WSConsumer.GetUnits();
                }
            }
            catch(Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
                Units = Units ?? await LoadList("unit.json", Units);
            }
            return Units;
        }

        /// <summary>
        /// Load SportTypes from the WebService
        /// Or from the local file
        /// </summary>
        /// <param name="forced">force to reload</param>
        /// <returns>SportTypes: list of SportType</returns>
        public async static Task<List<SportType>> LoadSports(bool forced = false)
        {
            try
            {
                if (forced)
                {
                    SportTypes = await WSConsumer.GetSports();
                }
                else
                {
                    SportTypes = SportTypes ?? await WSConsumer.GetSports();
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
                SportTypes = SportTypes ?? await LoadList("sport.json", SportTypes);
            }
            return SportTypes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forced">force reloading from web service</param>
        /// <returns>Sessions: list of Session</returns>
        public async static Task<List<Session>> LoadSessions(bool forced = false)
        {
            if (SportTypes == null)
                await LoadSports();
            if (Units == null)
                await LoadUnits();

            try
            {
                if (forced)
                {
                    Sessions = await WSConsumer.GetSessions(user.Id);
                }
                else
                {
                    Sessions = Sessions ?? await WSConsumer.GetSessions(user.Id);
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
                Sessions = Sessions ?? await LoadList("session.json", Sessions);
            }

            Sessions.Sort((x, y) => x.CompareTo(y));
            return Sessions;
        }

        /// <summary>
        /// Load a list of Generic objects from the file filename
        /// </summary>
        /// <typeparam name="T">Type of the element of the list to load</typeparam>
        /// <param name="filename">Name of the target file</param>
        /// <param name="list"></param>
        /// <returns></returns>
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

        /// <summary>
        /// load the user corresponding to user_name
        /// return it
        /// </summary>
        /// <returns>loaded user</returns>
        public async static Task<User> LoadUser(string user_name, string pwd)
        {
            if (user == null)
            {
                user = new User();
                user.Name = user_name;
                user.Password = pwd;

                try
                {
                    user = await WSConsumer.GetUser(user_name);
                }
                catch (Exception e)
                {
                    DependencyService.Get<IMessage>().longtime(e.Message);
                }
            }

            return user;
        }

        /// <summary>
        /// return the current user of the app if loaded
        /// </summary>
        /// <returns>user instance</returns>
        public static User GetUser()
        {
            return user;
        }
        #endregion

        #region Saving part
        /// <summary>
        /// Add a new unit to the webservice and reload ws units list
        /// Or Add it to the local units list
        /// save the list localy
        /// </summary>
        /// <param name="unit">new unit to add in Units</param>
        public async static void SaveUnit(Unit unit)
        {
            try
            {
                if (Units == null)
                    await LoadUnits();

                Units = await WSConsumer.AddUnit(unit);
            }
            catch(Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
                if (Units == null)
                    Units = new List<Unit>();

                Units.Add(unit);
            }
            SaveList(Units, unitFile);
        }

        /// <summary>
        /// Add a new sportType to the webservice and reload ws sports list
        /// Or Add it to the local sportType list
        /// save the list localy
        /// </summary>
        /// <param name="sport">new sportType to add in SportTypes</param>
        public static async void SaveSport(SportType sport)
        {
            try
            {
                if (SportTypes == null)
                    await LoadSports();

                SportTypes = await WSConsumer.AddSport(sport);
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
                if (SportTypes == null)
                    SportTypes = new List<SportType>();

                SportTypes.Add(sport);
            }
            SaveList(SportTypes, sportFile);
        }

        /// <summary>
        /// Add a new sesion to the static list of sessions of the class
        /// Save the list in the corresponding file
        /// </summary>
        /// <param name="session">new session to add</param>
        public async static void SaveSession(Session session)
        {
            try
            {
                if (Sessions == null)
                    Sessions = new List<Session>();

                Sessions = await WSConsumer.AddSession(session, user.Id);
            }
            catch(Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
                if (Sessions == null)
                    Sessions = new List<Session>();

                Sessions.Add(session);
            }
            SaveList(Sessions, sessionFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="old"></param>
        /// <param name="session"></param>
        public async static void UpDateSession(Session old, Session session)
        {
            try
            {
                if (Sessions.Contains(old))
                {
                    Sessions[Sessions.IndexOf(old)] = session;
                }
                session.Saved = await SaveList(Sessions, sessionFile);
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime(e.Message);
            }
        }

        /// <summary>
        /// Save all the data localy in their corresponding files
        /// </summary>
        public static async void SaveLocaly()
        {
            await SaveList(SportTypes, sportFile);
            await SaveList(Units, unitFile);
            await SaveList(Sessions, sessionFile);
        }

        /// <summary>
        /// Save a list of generic T object in the file desidnated by filename
        /// </summary>
        /// <typeparam name="T">Type of the objects in list</typeparam>
        /// <param name="list">list to save</param>
        /// <param name="filename">file where to save list</param>
        /// <returns>boolean to signal the end of the function</returns>
        public async static Task<bool> SaveList<T>(List<T> list, string filename)
        {
            IFileSystem fileSystem = FileSystem.Current;
            IFolder folder = fileSystem.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);

            await file.WriteAllTextAsync(JsonConvert.SerializeObject(list));

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        public async static void SaveUser(User newUser)
        {
            try
            {
                user = await WSConsumer.SaveUser(newUser);

                try
                {
                    IFileSystem fileSystem = FileSystem.Current;
                    IFolder folder = fileSystem.LocalStorage;
                    IFile file = await folder.CreateFileAsync(userFile, CreationCollisionOption.OpenIfExists);

                    await file.WriteAllTextAsync(JsonConvert.SerializeObject(user));
                }
                catch (Exception e2)
                {
                    DependencyService.Get<IMessage>().shorttime("ERROR : " + e2.Message);
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().shorttime(e.Message);
            }
        }
        #endregion

        public static Unit GetUnit(int id)
        {
            foreach(Unit u in Units)
            {
                if (u.Id == id)
                    return u;
            }
            return null;
        }

        public static SportType GetSport(int id)
        {
            foreach(SportType s in SportTypes)
            {
                if (s.Id == id)
                    return s;
            }
            return null;
        }
    }
}
