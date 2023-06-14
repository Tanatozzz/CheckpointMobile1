using CheckpointMobile1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using CheckpointMobile1.Singletons;

namespace CheckpointMobile1.API
{
    internal class HttpQuery
    {
        private readonly HttpClient _httpClient;

        public HttpQuery()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://94.228.124.99:12345/api/DataBase/");
        }

        public async Task<Employee> Login(string username, string password)
        {
            var loginData = new { Username = username, Password = password };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Login_check", loginData);
                if (response.IsSuccessStatusCode)
                {
                    // Вход выполнен успешно, получаем информацию о сотруднике
                    var json = await response.Content.ReadAsStringAsync();
                    var employee = JsonConvert.DeserializeObject<Employee>(json);
                    return employee;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Ошибка аутентификации
                    return null;
                }
                else
                {
                    // Обработка других ошибок, если необходимо
                    throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка отправки запроса на сервер(Login Query). Обратитесь к администратору системы.", ex.ToString());
                return null;
            }
        }
        public async Task<bool> CheckAccess(int idCheckpoint, int idEmployee)
        {
            try
            {
                var response = await _httpClient.GetAsync($"CheckAccess/{idCheckpoint}/{idEmployee}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    bool hasAccess = JsonConvert.DeserializeObject<bool>(json);
                    return hasAccess;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Проход не найден
                    return false;
                }
                else
                {
                    // Обработка других ошибок, если необходимо
                    throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка отправки запроса на сервер(CheckAccess Query). Обратитесь к администратору системы.", ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<CheckpointAccessedList>> GetEmployeeAccessibleCheckpoints(int employeeId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"EmployeeDoors/{employeeId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var checkpoints = JsonConvert.DeserializeObject<IEnumerable<CheckpointAccessedList>>(json);
                    return checkpoints;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<CheckpointAccessedList>();
                }
                else
                {
                    throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка отправки запроса на сервер(GetEmployeeAccessibleCheckpoints Query). Обратитесь к администратору системы.", ex.ToString());
                return null;
            }
        }

    }
}
