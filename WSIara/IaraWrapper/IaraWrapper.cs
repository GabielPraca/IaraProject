using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IaraWrapper
{
    public class IaraWrapper
    {
        private HttpClient client = new HttpClient();
        private static string apiMainPath = "api/IaraDB";
        private static string apiPersonalTaskPath = String.Concat(apiMainPath, "/PersonalTask");
        private static string apiUserPath = String.Concat(apiMainPath, "/User");
        public static bool authenticated = false;
        private static string _auth = String.Empty;

        public IaraWrapper(string email, string pass)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri("http://169.254.80.80:80/IaraAPI/");//IIS Port = http://169.254.80.80:80, local = http://localhost:53795/
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(5);

            _auth = UserAuthentication(email, pass);
        }

        #region User
        public bool? SaveUser(IaraModels.User user)
        {
            try
            {
                if(GetUser(user.email) != null)
                {
                    return null;
                }

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(Path.Combine(apiUserPath, "SaveUser"), content).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    if (!authenticated)
                        _auth = UserAuthentication(user.email, user.password);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUser(IaraModels.User user)
        {
            try
            {
                if (authenticated)
                {
                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(Path.Combine(apiUserPath, "UpdateUser"), content).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(string email)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = client.GetAsync(Path.Combine(apiUserPath, email, "DeleteUser")).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IaraModels.User GetUser(string email)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(Path.Combine(apiUserPath, email, "GetUser")).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    //return response.Content.ReadAsAsync<IaraModels.User>().Result;
                    return JsonConvert.DeserializeObject<IaraModels.User>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string UserAuthentication(string email, string pass)
        {
            try
            {
                IaraModels.User ret = GetUser(email);

                if (ret == null)
                {
                    authenticated = false;
                    return "Usuário não existe!";
                }
                else if (ret.email == email && ret.password != pass)
                {
                    authenticated = false;
                    return "Login e ou Senha Inválida!";
                }
                else
                {
                    authenticated = true;
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                authenticated = false;
                return "Erro";
            }
        }
        #endregion

        #region PersonalTask
        public bool SavePersonalTask(IaraModels.PersonalTask task)
        {
            try
            {
                if (authenticated)
                {
                    var json = JsonConvert.SerializeObject(task);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(Path.Combine(apiPersonalTaskPath, "SavePersonalTask"), content).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SavePersonalTasks(List<IaraModels.PersonalTask> tasks)
        {
            try
            {
                if (authenticated)
                {
                    var json = JsonConvert.SerializeObject(tasks);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(Path.Combine(apiPersonalTaskPath, "SavePersonalTasks"), content).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<IaraModels.PersonalTask> GetAllPersonalTasks(string email)
        {
            try
            {

                if (authenticated)
                {
                    HttpResponseMessage response = client.GetAsync(Path.Combine(apiPersonalTaskPath, email, "GetAllPersonalTasks")).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<List<IaraModels.PersonalTask>>(response.Content.ReadAsStringAsync().Result);
                        //return response.Content.ReadAsAsync<List<IaraModels.PersonalTask>>().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<IaraModels.PersonalTask> GetAllActivePersonalTasks(string email)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = client.GetAsync(Path.Combine(apiPersonalTaskPath, email, "GetAllActivePersonalTasks")).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<List<IaraModels.PersonalTask>>(response.Content.ReadAsStringAsync().Result);
                        //return response.Content.ReadAsAsync<List<IaraModels.PersonalTask>>().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdatePersonalTask(IaraModels.PersonalTask task)
        {
            try
            {
                if (authenticated)
                {
                    var json = JsonConvert.SerializeObject(task);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(Path.Combine(apiPersonalTaskPath, "UpdatePersonalTask"), content).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePersonalTask(IaraModels.PersonalTask task)
        {
            try
            {
                if (authenticated)
                {
                    var json = JsonConvert.SerializeObject(task);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(Path.Combine(apiPersonalTaskPath, "DeletePersonalTask"), content).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePersonalTasks(List<IaraModels.PersonalTask> tasks)
        {
            try
            {
                if (authenticated)
                {
                    var json = JsonConvert.SerializeObject(tasks);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(Path.Combine(apiPersonalTaskPath, "DeletePersonalTasks"), content).Result;
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
