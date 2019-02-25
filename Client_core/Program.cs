using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client_core
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task ProcessRepositories()
        {
            //first 3 lines to setting up http client
            client.DefaultRequestHeaders.Accept.Clear();
            //I want to get the data of application/vnd.github.v3 and it's a form of json
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //Add user agent header 
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //get data from this url as string (this is a string but not avaliable now)
            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            //when the stringTask is avaliable, it will become a string
            var msg = await stringTask;
            Console.Write(msg);
        }

        static void Main(string[] args)
        {
            //you cannot exit this program until before all the tasks be finished
            ProcessRepositories().Wait();
        }
    }
}
