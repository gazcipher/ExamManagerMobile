using ExamManagerMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagerMobile.Data
{
    public class RestService
    {
        HttpClient client;
        string grant_type = "password";

        public RestService()
        {
            client = new HttpClient();

            //set the buffer size
            client.MaxResponseContentBufferSize = 256000;

            //default content type given
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded' "));
        }

        public async Task<Token> Login(User user)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("grant_type", grant_type));
            postData.Add(new KeyValuePair<string, string>("username", user.UserName));
            postData.Add(new KeyValuePair<string, string>("password", user.Password));

            var content = new FormUrlEncodedContent(postData);
            //var web_url = "https://exam-manager-api.azurewebsites.net";
            var response = await PostResponseLogin<Token>(Constants.Login_Url, content); //now we serialize the json into token object

            DateTime dt = new DateTime();
            dt = DateTime.Today;             //expire data

            //This will let us know if our token has expired, you will have to relogin and get a new token (though a refresh token could be implemented)
            response.expire_date = dt.AddSeconds(response.expire_in);

            //return token. Note you can store the token in your db if you want
            return response;
        }

        //====================================Making the post function more generic======================================================================
        public async Task<T> PostResponseLogin<T>(string web_url, FormUrlEncodedContent content) where T : class //Token object, Json Serializer param
        {
            var response = await client.PostAsync(web_url, content);
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var responseObjectType = JsonConvert.DeserializeObject<T>(jsonResult);

            return responseObjectType;
        }


        public async Task<T> PostResponse<T>(string web_url, string json_string) where T : class
        {
            var Token = App.TokenDatabase.GetToken();
            string ContentType = "application/json";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.access_token); //get jwt
            try
            {
                var Result = await client.PostAsync(web_url, new StringContent(json_string, Encoding.UTF8, ContentType));
                if(Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResult = Result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResponse = JsonConvert.DeserializeObject<T>(jsonResult);
                        return ContentResponse;

                    }
                    catch (Exception e)
                    {
                        return null;
                       // throw;
                    }
                }


            }
            catch (Exception e)
            {
                //throw;
                return null;
            }
            return null;
        
        }
        //============================================Ends Generic Post Method==========================================================================

        //===========================================Get  Calls=========================================================================================
        public async Task<T> GetResponse<T>(string web_url) where T : class
        {
            var Token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.access_token); //get jwt

            try
            {
                var response = await client.GetAsync(web_url);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("jsonResult:" + jsonResult);

                    try
                    {
                        var ContentResponse = JsonConvert.DeserializeObject<T>(jsonResult);
                        return ContentResponse;
                    }
                    catch (Exception e)
                    {
                        return null;
                        //throw;
                    }

                }
            }
            catch (Exception e)
            {
                return null;
                //throw;
            }
            return null;
        }
        //===========================================Ends Get  Calls====================================================================================
    }
}
