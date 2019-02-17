using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace restful_client
{
    class ClientMain
    {
        static HttpClient client = new HttpClient();
        static string address = "http://localhost:64195/";
        // Update port # in the following line.
        static async Task RunAsync()
        {
            
            try
            {
                ProductController.SettingClient(address);
                // give the Object parameters, there we use the product, of course, we can also use other different
                Product product = new Product
                {
                    Name = "Gizmo",
                    Price = 100,
                    Category = "Widgets"
                };

                // let's try a post at first
                var url = await ProductController.CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                //Let's try to get the product we made
                product = await ProductController.GetProductAsync(url.PathAndQuery);
                ProductController.ShowProduct(product);

                //Try to update the product
                Console.WriteLine("Update price of the product");
                product.Price = 80;
                await ProductController.UpdateProductAsync(product);

                //Let's check out does this update function work well by using a get fuction
                product = await ProductController.GetProductAsync(url.PathAndQuery);
                ProductController.ShowProduct(product);

                // the last step, delete the product in the server side
                var statusCode = await ProductController.DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
            }
            catch (Exception e)
            {
                Console.WriteLine($"error, the error code is {e.Message}");
            }

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
    }
}
