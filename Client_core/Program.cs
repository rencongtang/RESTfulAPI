using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
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

            //when the stringTask is avaliable, it will become a string`
            var msg = await stringTask;
            //Console.Write(msg);

            var serializer = new DataContractJsonSerializer(typeof(List<Repo>));

            // now let's have get the stream of the json data, why we use stream?
            // because the ReadObject function can only read stream, of course we need to
            // use await to make sure this stream be fully readed
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<Repo>;
            
            //now let us have a look at the results, we suppose to see all the names of each repo
            foreach (var repo in repositories)
            {
                Console.WriteLine($"the currnet object name is: {repo.name}");
            }
        }

        static void Main(string[] args)
        {
            //you cannot exit this program until before all the tasks be finished
            ProcessRepositories().Wait();
        }
    }
}
