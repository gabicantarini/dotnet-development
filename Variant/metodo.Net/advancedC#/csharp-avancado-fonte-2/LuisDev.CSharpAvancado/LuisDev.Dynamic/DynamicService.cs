using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LuisDev.Dynamic
{
    internal class DynamicService
    {
        public void Example1()
        {
            string jsonData = GetDataFromExternalApi();

            dynamic parsedData = JsonConvert.DeserializeObject<dynamic>(jsonData)!;

            string name = parsedData.name;
            int age = parsedData.age;

            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Age: {age}");
        }

        private static string GetDataFromExternalApi()
        {
            string apiUrl = "http://localhost:5032/Product/v1";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        return json;
                    }
                    else
                    {
                        Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                        throw new HttpRequestException($"Erro na solicitação: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro na solicitação: {ex.Message}");
                    throw new HttpRequestException($"Erro na solicitação: {ex.Message}");
                }
            }
        }

        public void Example2()
        {
            string jsonData = GetDataFromExternalApiV2();

            List<dynamic> products = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

            foreach (var product in products)
            {
                Console.WriteLine($"Product Type: {product.type}");

                switch (product.type.ToString())
                {
                    case "book":
                        Book book = JsonConvert.DeserializeObject<Book>(product.ToString());
                        Console.WriteLine($"Type: {book.Type}");
                        Console.WriteLine($"Title: {book.Title}");
                        Console.WriteLine($"Author: {book.Author}");
                        break;
                    case "electronics":
                        Console.WriteLine($"Name: {product.name}");
                        Console.WriteLine($"Brand: {product.brand}");
                        Console.WriteLine($"Price: ${product.price}");
                        break;
                    case "clothing":
                        Console.WriteLine($"Item: {product.item}");
                        Console.WriteLine($"Size: {product.size}");
                        Console.WriteLine($"Color: {product.color}");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static string GetDataFromExternalApiV2()
        {

            string apiUrl = "http://localhost:5032/Product/v2";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        return json;
                    }
                    else
                    {
                        Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                        throw new HttpRequestException($"Erro na solicitação: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro na solicitação: {ex.Message}");
                    throw new HttpRequestException($"Erro na solicitação: {ex.Message}");
                }
            }
        }

        public void Example3()
        {
            dynamic person = new ExpandoObject();

            person.Name = "Alice";
            person.Age = 30;

            Console.WriteLine($"Name: {person.Name}");
            Console.WriteLine($"Age: {person.Age}");

            person.City = "New York";

            Console.WriteLine($"City: {person.City}");

            ((IDictionary<string, object>)person).Remove("City");

            Console.WriteLine($"City: {person.City}");
        }

    }
}
