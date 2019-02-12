using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace restful_client
{
    class Client
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(Product product)
        {
            Console.WriteLine("$Name: {product.Name}\tPrice: " +
                "${product.Price}\tCategory: {product.Category}");
        }

        static async Task<Uri> CreateProductAsync(Product product)
        { 
            HttpResponseMessage  reponse = await client.PostAsJsonAsync()
        }
    }
}
