using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace restful_client
{
    public static class ProductController
    {
        public static HttpClient client = new HttpClient();
        private static bool client_setted = false;

        public static void SettingClient(string address)
        {
            client.BaseAddress = new Uri(address);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            client_setted = true;
            Console.WriteLine($"Host location has been setted ad {address}");
        }

        private static void ClientNotSetted()
        {
            Console.WriteLine("Sorry, the Http Client is not Setted, run Setting Client with address as parameter");
        }
        /// <summary>
        ///to print out the content of product class
        /// </summary>
        /// <param name="product"></param>
        public static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: " +
                $"{product.Price}\tCategory: {product.Category}");
        }

        /// <summary>
        ///Post command (creat a new product class) 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task<Uri> CreateProductAsync(Product product)
        {
            if (!client_setted)
            {
                SettingClient("http://localhost:64195/");
            }
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", product);
            response.EnsureSuccessStatusCode();

            //return the uri of created resource
            return response.Headers.Location;

        }

        /// <summary>
        /// Get command (get the production class)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<Product> GetProductAsync(string path)
        {
            if (!client_setted)
            {
                SettingClient("http://localhost:64195/");
            }
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            else
            {
                Console.WriteLine($"cannot get the object, the error code is {response.EnsureSuccessStatusCode()}");
            }
            return product;
        }

        public static async Task<List<Product>> GetProductsAsync(string hostAddress)
        {
            List<Product> products = new List<Product>();
            HttpResponseMessage response = await client.GetAsync(hostAddress + "api/products/");
            products = await response.Content.ReadAsAsync<List<Product>>();
            return products;
        }

        /// <summary>
        /// Put command to update the data
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task<Product> UpdateProductAsync(Product product)
        {
            if (!client_setted)
            {
                SettingClient("http://localhost:64195/");
            }
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            //Deserialize the updated product from the response body
            product = await response.Content.ReadAsAsync<Product>();

            return product;
        }

        /// <summary>
        /// Use DELETE command to delete 
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            if (!client_setted)
            {
                SettingClient("http://localhost:64195/");
            }
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }




    }
}
