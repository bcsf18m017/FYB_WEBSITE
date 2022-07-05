using IqbalElectronics.DB.Models;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace MyProject.Api_Calls
{
    public class ApiCalls
    {
        private static readonly HttpClient httpClient=new HttpClient();
        private static JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static async Task<List<Product>> getAllProducts()
        {
            var response = await httpClient.GetAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/products");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var Products = JsonSerializer.Deserialize<List<Product>>(json, options);
            return Products;
        }

        public static async Task<List<Person>> getAllPerson()
        {
            var response = await httpClient.GetAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Person");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var persons = JsonSerializer.Deserialize<List<Person>>(json, options);
            return persons;
        }
        public static async Task<List<Person>> getAllPersonByRole(string role)
        {
            var response = await httpClient.GetAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Person/GetByRole/" + role);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var persons = JsonSerializer.Deserialize<List<Person>>(json, options);
            return persons;
        }

        public static async Task<List<Order>> getAllOrders()
        {
            var response = await httpClient.GetAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/orders");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<Order>>(json, options);
            return orders;
        }
        public static async Task<List<OrderDetail>> getAllOrdersDetails()
        {
            var response = await httpClient.GetAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/orderdetails");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<List<OrderDetail>>(json, options);
            return orders;
        }

        public static async Task<List<Category>> getAllCategories()
        {
            var response = await httpClient.GetAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Categories");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var Categories = JsonSerializer.Deserialize<List<Category>>(json, options);
            return Categories;
        }
        public static async Task<Product> postProduct(ProductModel model)
        {
            var response = await httpClient.PostAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Products", model);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                return null;
            }
            
            var json = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<Product>(json, options);
            return product;
        }
        public static async Task<Person> addPerson(string role,PersonModel model)
        {
            var response = await httpClient.PostAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Person/Register/" + role, model);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<Person>(json, options);
            return person;
        }
        public static async Task<bool> addPersonGuarantee(PersonGuaranteeModel model)
        {
            var response = await httpClient.PostAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/PersonGuarantees", model);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> addTransactionHistory(TransactionHistoryModel model)
        {
            var response = await httpClient.PostAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/TransactionHistories", model);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static async Task<Person> validateUser(LoginModel login)
        {
            var response = await httpClient.PostAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/authentication/login", login);
            try
            {

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<Person>(json, options);
            return person;
        }

        public static async Task<bool> deleteProduct(String id)
        {
            var response = await httpClient.DeleteAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Products/" + id);
            try
            {

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> deletePerson(String id)
        {
            var response = await httpClient.DeleteAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Person/" + id);
            try
            {

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool>updateProduct(string id,ProductModel p)
        {
            var response = await httpClient.PutAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Products/" + id, p);
            try
            {

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
        public static async Task<Person> updatePerson(string id, PersonModel p)
        {
            var response = await httpClient.PutAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Person/" + id, p);
            try
            {

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<Person>(json, options);
            return person;

        }


        public static async Task<bool> updateOrder(int id, OrderModel p)
        {
            var response = await httpClient.PutAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/orders/" + id, p);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> updateOrderDetails(string id, OrderDetailModel p)
        {
            var response = await httpClient.PutAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/orderdetails/" + id, p);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool>addCategory(String description)
        {
            var response = await httpClient.PostAsJsonAsync("https://iqbalelectronicswebapi.azurewebsites.net/api/Categories", description);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}