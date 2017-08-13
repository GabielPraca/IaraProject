using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            client.BaseAddress = new Uri("http://localhost:53795/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            //client.DefaultRequestHeaders.Add("Pragma", "no-cache");

            _auth = UserAuthentication(email, pass);
        }

        #region User
        public bool SaveUser(IaraModels.User user)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync(Path.Combine(apiUserPath, "SaveUser"), user).Result;
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
                    HttpResponseMessage response = client.PostAsJsonAsync(Path.Combine(apiUserPath, "UpdateUser"), user).Result;
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
                    return response.Content.ReadAsAsync<IaraModels.User>().Result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string UserAuthentication(string email, string pass)
        {
            try
            {
                IaraModels.User ret = this.GetUser(email);

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
                    HttpResponseMessage response = client.PostAsJsonAsync(Path.Combine(apiPersonalTaskPath, "SavePersonalTask"), task).Result;
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
                        return response.Content.ReadAsAsync<List<IaraModels.PersonalTask>>().Result;
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
                        return response.Content.ReadAsAsync<List<IaraModels.PersonalTask>>().Result;
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
                    HttpResponseMessage response = client.PostAsJsonAsync(Path.Combine(apiPersonalTaskPath, "UpdatePersonalTask"), task).Result;
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
                    HttpResponseMessage response = client.PostAsJsonAsync(Path.Combine(apiPersonalTaskPath, "DeletePersonalTask"), task).Result;
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
        #endregion
    }
}
