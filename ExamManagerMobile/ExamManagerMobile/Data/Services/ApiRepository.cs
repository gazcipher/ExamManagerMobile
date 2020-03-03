using ExamManagerMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagerMobile.Data.Services
{
    public class ApiRepository
    {

        //===Register User Method========================================================================================================================================
        public async Task<bool> RegisterAsync(string email, string password, string confirmpassword, string matric, string firstname, string lastname, Guid departmentID)
        {
            var client = new HttpClient();

            var model = new RegisterModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmpassword,
                MatricNumber = matric,
                FirstName = firstname,
                LastName = lastname,
                DepartmentID = departmentID
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://e-manager.azurewebsites.net/api/v1/auth/signup", content);

            return response.IsSuccessStatusCode;
        }
        //===End Register  Method========================================================================================================================================

        //=========================Login Method==========================================================================================================================
        public async Task LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password"),
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://e-manager.azurewebsites.net/api/v1/auth/signin");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);
        }

        //================================Ends Login Method==============================================================================================================

    }
}
