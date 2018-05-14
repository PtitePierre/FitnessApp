﻿using System;
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
            List<Unit> units = new List<Unit>();
            try
            {
                // Get the json data from the web service
                // deserialize the data into units
                units = await GetList<Unit>("http://psotty.pythonanywhere.com/units");
                
                DependencyService.Get<IMessage>().shorttime("Units loaded from Web Service");
            }
            catch(Exception e)
            {
                //DependencyService.Get<IMessage>().longtime("ERR: "+e.Message);
                throw e;
            }
            return units;
        }

        /// <summary>
        /// send unit to the web service to save it as a new one
        /// </summary>
        /// <param name="unit">new Unit</param>
        /// <returns></returns>
        public async static Task<List<Unit>> AddUnit(Unit unit)
        {
            // TO DO:
            // serialize unit in json format
            // send the serialized unit to the web service
            // get the new list
            List<Unit> units = new List<Unit>();
            try
            {
                string jsonUnit = JsonConvert.SerializeObject(unit);
                units = await Send<Unit>("http://psotty.pythonanywhere.com/units", jsonUnit);
                
                DependencyService.Get<IMessage>().shorttime("Unit added to the Web Service");
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR save unit: " + e.Message);
            }
            return units;
        }

        /// <summary>
        /// get the list of SportType from the Web Service
        /// </summary>
        /// <returns>list of SportType</returns>
        public async static Task<List<SportType>> GetSports()
        {
            List<SportType> sports = new List<SportType>();
            try
            {
                // Get the json data from the web service
                // deserialize the data into sports
                sports = await GetList<SportType>("http://psotty.pythonanywhere.com/sports");

                DependencyService.Get<IMessage>().shorttime("Sports loaded from Web Service");
            }
            catch (Exception e)
            {
                //DependencyService.Get<IMessage>().longtime("ERR: " + e.Message);
                throw e;
            }
            return sports;
        }

        /// <summary>
        /// send a new SportType to the Web Service to save it
        /// </summary>
        /// <param name="sport">new SportType</param>
        /// <returns>list of SportType</returns>
        public static async Task<List<SportType>> AddSport(SportType sport)
        {
            List<SportType> sports = new List<SportType>();
            // TO DO:
            // serialize sport in json format
            // send the serialized sport to the web service
            // get the new list
            try
            {
                string jsonSport = JsonConvert.SerializeObject(sport);
                sports = await Send<SportType>("http://psotty.pythonanywhere.com/sports", jsonSport);

                DependencyService.Get<IMessage>().longtime(jsonSport);
                //DependencyService.Get<IMessage>().shorttime("Sport added to the Web Service");
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().longtime("ERR save unit: " + e.Message);
            }
            return sports;
        }

        /// <summary>
        /// Get the list of sessions for user (-> user_id)
        /// </summary>
        /// <returns>list of Session</returns>
        public async Task<List<Session>> GetSessions(int user_id)
        {
            List<Session> sessions = new List<Session>();
            // TO DO:
            // Get the json data from the web service
            // deserialize the data into sessions
            try
            {
                sessions = await GetList<Session>("http://psotty.pythonanywhere.com/sessions/"+user_id);

                DependencyService.Get<IMessage>().shorttime("Sessions loaded from Web Service");
            }
            catch (Exception e)
            {
                throw e;
            }
            return sessions;
        }

        public void AddSession(Session session)
        {
            // TO DO:
            // serialize session in json format
            // send the serialized session to the web service
            // get the new list
        }

        public void SaveUser(User user)
        {
            // TO DO:
            // serialize user in json format
            // send the serialized user to the web service
            // get the id generated by the WS
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
                // clean the string to just have the array
                value = value.Remove(0, value.IndexOf("[") - 1);
                value = value.Remove(value.LastIndexOf("]") + 1);

                list = JsonConvert.DeserializeObject<List<T>>(value);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static async Task<List<T>> Send<T>(string url, string jsonObject)
        {
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
                DependencyService.Get<IMessage>().longtime("save : " + result.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
            List<T> list = await GetList<T>(url);
            return list;

        }
    }
}
