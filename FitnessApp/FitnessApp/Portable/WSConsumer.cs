using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FitnessApp.Portable
{
    public class WSConsumer
    {
        private static HttpClient restClient;

        /// <summary>
        /// get the list of unit from the web service
        /// throw an exception if any
        /// </summary>
        /// <returns>list of Unit</returns>
        public async static Task<List<Unit>> GetUnits()
        {
            DependencyService.Get<IMessage>().shorttime("Loading Units");
            List<Unit> units = new List<Unit>();
            try
            {
                // Get the json data from the web service
                // deserialize the data into units
                units = await GetList<Unit>("http://psotty.pythonanywhere.com/units");
            }
            catch(Exception e)
            {
                //DependencyService.Get<IMessage>().longtime("ERR: "+e.Message);
                throw e;
            }
            return units;
        }

        /// <summary>
        /// get the list of SportType from the Web Service
        /// </summary>
        /// <returns>list of SportType</returns>
        public async static Task<List<SportType>> GetSports()
        {
            DependencyService.Get<IMessage>().shorttime("Loading Sports");
            List<SportType> sports = new List<SportType>();
            try
            {
                // Get the deserialized data into sports list from the web service
                sports = await GetList<SportType>("http://psotty.pythonanywhere.com/sports");
            }
            catch (Exception e)
            {
                throw e;
            }
            return sports;
        }

        /// <summary>
        /// Get the list of sessions for user_name
        /// </summary>
        /// <returns>list of Session</returns>
        /// <param name="user_name">user identifier</param>
        public static async Task<List<Session>> GetSessions(int user_id)
        {
            DependencyService.Get<IMessage>().shorttime("Loading Sessions");
            string url = "http://psotty.pythonanywhere.com/sessions/" + user_id;
            List<Session> sessions = new List<Session>();
            try
            {
                // Get the deserialized data from the web service
                sessions = await GetList<Session>(url);

                foreach(Session s in sessions){s.Saved = true;}
            }
            catch (Exception e)
            {
                throw e;
            }
            return sessions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetList<T>(string url)
        {
            List<T> list = new List<T>();
            string value = "";
            // connection to the web service
            string request = "";
            restClient = new HttpClient();
            try
            {
                restClient.BaseAddress = new Uri(url);
                restClient.DefaultRequestHeaders.Clear();
                restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
                value = await restClient.GetStringAsync(request);

                if(value != null && value != "[]\n")
                {
                    // clean the string to just have the array
                    if(value.IndexOf("[") > 0)
                    {
                        value = value.Remove(0, value.IndexOf("[") - 1);
                        value = value.Remove(value.LastIndexOf("]") + 1);
                    }

                    list = JsonConvert.DeserializeObject<List<T>>(value);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        /// <summary>
        /// complet the user object with its id from the WS
        /// </summary>
        /// <param name="user">user to complete</param>
        /// <returns>completed user</returns>
        public static async Task<User> GetUser(string user_name)
        {
            User user;
            string url = "http://psotty.pythonanywhere.com/users/" + user_name;
            string request = "";
            restClient = new HttpClient();
            try
            {
                restClient.BaseAddress = new Uri(url);
                restClient.DefaultRequestHeaders.Clear();
                restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
                string value = await restClient.GetStringAsync(request);

                user = JsonConvert.DeserializeObject<User>(value);
            }
            catch (Exception e)
            {
                throw e;
            }
            return user;
        }

        /// <summary>
        /// send unit to the web service to save it as a new one
        /// </summary>
        /// <param name="unit">new Unit</param>
        /// <returns></returns>
        public async static Task<List<Unit>> AddUnit(Unit unit)
        {
            List<Unit> units = new List<Unit>();
            try
            {
                string url = "http://psotty.pythonanywhere.com/units";
                // serialize unit in json format
                string jsonUnit = JsonConvert.SerializeObject(unit);
                // send the serialized unit to the web service
                await Send(url, jsonUnit);
                // get the new list
                units = await GetList<Unit>(url);
                
                DependencyService.Get<IMessage>().shorttime("Unit added to the Web Service");
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR save unit: " + e.Message);
            }
            return units;
        }

        /// <summary>
        /// send a new SportType to the Web Service to save it
        /// </summary>
        /// <param name="sport">new SportType</param>
        /// <returns>list of SportType</returns>
        public static async Task<List<SportType>> AddSport(SportType sport)
        {
            List<SportType> sports = new List<SportType>();
            try
            {
                string url = "http://psotty.pythonanywhere.com/sports";
                // serialize sport in json format
                string jsonSport = JsonConvert.SerializeObject(sport);
                // send the serialized sport to the web service
                await Send(url, jsonSport);

                // get the new list
                sports = await GetList<SportType>(url);

                DependencyService.Get<IMessage>().longtime(jsonSport);
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR save unit: " + e.Message);
            }
            return sports;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public static async Task<List<Session>> AddSession(Session session, int user_id)
        {
            List<Session> sessions = new List<Session>();
            try
            {
                string url = "http://psotty.pythonanywhere.com/sessions";
                // serialize session in json format
                string jsonSession = session.ToJson();
                // send the serialized session to the web service
                await Send(url, jsonSession);

                // get the new list
                sessions = await GetSessions(user_id);

                DependencyService.Get<IMessage>().shorttime("Session added to the Web Service");
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR save unit: " + e.Message);
            }
            return sessions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public static async Task<User> SaveUser(User user)
        {
            try
            {
                // serialize user in json format
                string jsonUser = JsonConvert.SerializeObject(user);
                // send the serialized user to the web service
                user.Saved = await Send("http://psotty.pythonanywhere.com/users", jsonUser);
                if (user.Saved)
                    user = await GetUser(user.Name);
                else
                    DependencyService.Get<IMessage>().longtime("User not saved on WS");

            }
            catch (Exception e)
            {
                throw e;
            }
            return user;
        }

        /// <summary>
        /// send a serialized object to url via POST method
        /// </summary>
        /// <param name="url">where to add the object</param>
        /// <param name="jsonObject">json representation of the added object</param>
        /// <returns></returns>
        public static async Task<bool> Send(string url, string jsonObject)
        {
            bool ok = true;
            // connection to the web service
            restClient = new HttpClient();
            try
            {
                // def header
                restClient.BaseAddress = new Uri(url);
                restClient.DefaultRequestHeaders.Clear();
                restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));

                // def body
                var content = new StringContent(jsonObject.ToLower(), Encoding.UTF8, "application/json");
                var answer = await restClient.PostAsync(url, content);
                var result = answer.Content;
                // get the result : new json representation of corresponding list
                //DependencyService.Get<IMessage>().longtime("save : " + result.ToString());
            }
            catch (Exception e)
            {
                ok = false;
                throw e;
            }
            return ok;
        }
    }
}
