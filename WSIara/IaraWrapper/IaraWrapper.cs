using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IaraDAO
{
    public class IaraWrapper
    {
        private static HttpClient client = new HttpClient();
        private static string apiPersonalTaskPath = "/PersonalTask";
        private static string apiUserPath = "/User";
        public static bool authenticated = false;
        private static string _auth = String.Empty;

        public IaraWrapper(string email, string pass)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");

            _auth = UserAuthentication(email, pass).Result;
        }

        #region User
        public async Task<bool> SaveUser(IaraModels.User user)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(Path.Combine(apiUserPath, "SaveUser"), user);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    if (!authenticated)
                        _auth = UserAuthentication(user.email, user.password).Result;

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

        public async Task<bool> UpdateUser(IaraModels.User user)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(Path.Combine(apiUserPath, "UpdateUser"), user);
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

        public async Task<bool> DeleteUser(string email)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = await client.GetAsync(Path.Combine(apiUserPath, email, "DeleteUser"));
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

        public async Task<IaraModels.User> GetUser(string email)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(Path.Combine(apiUserPath, email, "GetUser"));
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

        public async Task<string> UserAuthentication(string email, string pass)
        {
            try
            {
                IaraModels.User ret = await GetUser(email);

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
            catch (Exception)
            {
                authenticated = false;
                return "Erro";
            }
        }
        #endregion

        #region PersonalTask
        public async Task<bool> SavePersonalTask(IaraModels.PersonalTask task)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(Path.Combine(apiPersonalTaskPath, "SavePersonalTask"), task);
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

        public async Task<List<IaraModels.PersonalTask>> GetAllPersonalTasks(string email)
        {
            try
            {

                if (authenticated)
                {
                    HttpResponseMessage response = await client.GetAsync(Path.Combine(apiPersonalTaskPath, email, "GetAllPersonalTasks"));
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

        public async Task<List<IaraModels.PersonalTask>> GetAllActivePersonalTasks(string email)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = await client.GetAsync(Path.Combine(apiPersonalTaskPath, email, "GetAllActivePersonalTasks"));
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

        public async Task<bool> UpdatePersonalTask(IaraModels.PersonalTask task)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(Path.Combine(apiPersonalTaskPath, "UpdatePersonalTask"), task);
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

        public async Task<bool> DeletePersonalTask(IaraModels.PersonalTask task)
        {
            try
            {
                if (authenticated)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(Path.Combine(apiPersonalTaskPath, "DeletePersonalTask"), task);
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
