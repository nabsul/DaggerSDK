using GraphQlClientGenerator;
using System.Text;

var port = Environment.GetEnvironmentVariable("DAGGER_SESSION_PORT");
var token = Environment.GetEnvironmentVariable("DAGGER_SESSION_TOKEN");
token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token + ":"));
var url = $"http://localhost:{port}/query";

var headers = new Dictionary<string, string>
{
    { "Authorization", $"Basic {token}" },
    { "Accept", "application/json" }
};

var schema = await GraphQlGenerator.RetrieveSchema(HttpMethod.Get, url, headers);
var csharpCode = new GraphQlGenerator().GenerateFullClientCSharpFile(schema, "DaggerSDK.CodeGen");
await File.WriteAllTextAsync("DaggerClient.cs", csharpCode);
