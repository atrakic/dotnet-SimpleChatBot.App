
using System.Net.Http.Json;

var client = new HttpClient();
var server = Environment.GetEnvironmentVariable("SERVER") ?? "http://localhost:5000";

client.BaseAddress = new Uri(server);

while (true)
{
    Console.WriteLine("Question: ");

    var question = Console.ReadLine();

    await foreach (var msg in client.GetFromJsonAsAsyncEnumerable<string>($"api/Chat?question={question}"))
    {
        Console.WriteLine(msg);
    }
    Console.WriteLine();
}
