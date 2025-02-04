using System.Text;
using System.Text.Json;
using MASBusiness.DTO;

class Program
{
    private static HttpClient _httpClient = new HttpClient();

    static async Task Main()
    {
        var config = LoadConfig();
        var baseUrl = config.BaseUrl;

        while (true)
        {
            Console.WriteLine("Select an API to call:");
            for (int i = 0; i < config.Endpoints.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {config.Endpoints[i].Name} - {baseUrl}{config.Endpoints[i].Url} ({config.Endpoints[i].HttpMethod})");
            }
            Console.WriteLine("0. Exit");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > config.Endpoints.Length)
            {
                Console.WriteLine("Invalid selection. Try again.");
                continue;
            }

            if (choice == 0) break;

            var endpoint = config.Endpoints[choice - 1];
            string requestUrl = baseUrl + endpoint.Url;

            if (endpoint.HttpMethod != "POST")
            {
                requestUrl = await AppendUrlParams(requestUrl, endpoint.UrlParams);
            }
            string jsonBody = string.Empty;
            if (endpoint.HttpMethod == "POST" || endpoint.HttpMethod == "PUT")
            {
                var bodyStructure = await CollectDtoValues(endpoint.Url);
                jsonBody = JsonSerializer.Serialize(bodyStructure);
            }

            await SendRequest(endpoint.HttpMethod, requestUrl, jsonBody);
        }
    }

    static Config LoadConfig()
    {
        var json = File.ReadAllText("./appsettings.json");
        return JsonSerializer.Deserialize<Config>(json);
    }

    static async Task SendRequest(string httpMethod, string requestUrl, string jsonBody)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage();

        switch (httpMethod.ToUpper())
        {
            case "POST":
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                break;
            case "GET":
                requestMessage.Method = HttpMethod.Get;
                break;
            case "PUT":
                requestMessage.Method = HttpMethod.Put;
                requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                break;
            case "DELETE":
                requestMessage.Method = HttpMethod.Delete;
                break;
            default:
                Console.WriteLine("Unsupported HTTP Method.");
                return;
        }

        requestMessage.RequestUri = new Uri(requestUrl);

        try
        {
            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine();
    }

    static async Task<string> AppendUrlParams(string url, Dictionary<string, string> urlParams)
    {
        foreach (var param in urlParams)
        {
            Console.WriteLine($"Enter value for parameter '{param.Key}':");
            string value = Console.ReadLine();
            url = url.Replace(param.Value, value);
        }
        return url;
    }

    static async Task<object> CollectDtoValues(string endpointUrl)
    {
        Console.WriteLine($"Enter values for {endpointUrl}:");

        var dtoType = typeof(VehicleDto);
        var dtoInstance = Activator.CreateInstance(dtoType);

        var jsonObject = new Dictionary<string, object>();
        await CollectProperties(dtoType, dtoInstance, jsonObject);

        return jsonObject;
    }

    static async Task CollectProperties(Type dtoType, object dtoInstance, Dictionary<string, object> jsonObject)
    {
        var properties = dtoType.GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(dtoInstance);
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                Console.WriteLine($"Enter values for nested DTO '{property.Name}':");
                var nestedObject = Activator.CreateInstance(property.PropertyType);
                var nestedJsonObject = new Dictionary<string, object>();
                await CollectProperties(property.PropertyType, nestedObject, nestedJsonObject);
                jsonObject[property.Name] = nestedJsonObject;
            }
            else
            {
                Console.WriteLine($"Enter value for {property.Name} ({property.PropertyType.Name}):");
                var input = Console.ReadLine();
                jsonObject[property.Name] = input;
            }
        }
    }
}

class Config
{
    public string BaseUrl { get; set; }
    public Endpoint[] Endpoints { get; set; }
}

class Endpoint
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string HttpMethod { get; set; }
    public Dictionary<string, string> UrlParams { get; set; }
}