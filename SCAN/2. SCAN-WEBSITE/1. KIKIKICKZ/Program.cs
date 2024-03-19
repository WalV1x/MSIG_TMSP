using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;

HttpClient client = new HttpClient();

string baseUrl = "https://kikikickz.com/collections/all?page=";
int totalPages = 16;

List<string> urls = new List<string>();

for (int page = 1; page <= totalPages; page++)
{
    string urlWeb = baseUrl + page;
    urls.Add(urlWeb);
}

// Dossier local pour stocker les données extraites
string folder = @"D:\MSIG\PROGRAMMATION\0. TMSP\SCAN\2. DATA-SCAN\KIKIKICKZ";
// Chemin du fichier avec le nom incluant la date actuelle
string pathfile = Path.Combine(folder, ($"SNKRS-KIKIKICKZ-{DateTime.Now:yyyyMMdd}.csv"));

// Création du dossier s'il n'existe pas déjà
if (!Directory.Exists(folder))
{
    Directory.CreateDirectory(folder);
}

if (File.Exists(pathfile))
{
    File.Delete(pathfile);
}

foreach (var url in urls)
{
    try
    {
        Thread.Sleep(1000);
        // Envoi d'une requête HTTP GET pour récupérer le contenu de la page
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
        // Simulation d'un ordinateur
        request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
        request.Headers.TryAddWithoutValidation("Accept-Encoding", "deflate");
        request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

        request.Version = Version.Parse("2.0");

        // Envoi de la requête et récupération de la réponse
        using HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string html = await response.Content.ReadAsStringAsync();

        List<List<string>> shoes = new List<List<string>>();

        var nameMatches = Regex.Matches(html, "\"title\":\"([^\"]+)\"");
        foreach (Match name in nameMatches)
        {
            string subPart = html.Substring(name.Index);

            var priceMatch = Regex.Match(subPart, "\"amount\":(\\d+)");
            string price = priceMatch.Groups[1].Value;

            var urlMatch = Regex.Match(subPart, "\"url\":\"([^\"]+)\"");
            string productUrl = urlMatch.Groups[1].Value;

            if (!name.Groups[1].Value.Contains("EU") &&
                !name.Groups[1].Value.Contains("French") &&
                !name.Groups[1].Value.Contains("German") &&
                !name.Groups[1].Value.Contains("English") &&
                !name.Groups[1].Value.Contains("Nike") &&
                !name.Groups[1].Value.Contains("Spanish") &&
                !name.Groups[1].Value.Contains("Italian") &&
                !name.Groups[1].Value.Contains("Selecteur menu Desktop \\/ Tablette") &&
                !name.Groups[1].Value.Contains("Euro") &&
                !name.Groups[1].Value.Contains("Sneakers tendances"))
            {

                    shoes.Add(new List<string> { name.Groups[1].Value, price,  productUrl });

            }
        }

        foreach (var shoe in shoes)
        {

            Console.WriteLine($"{shoe[0]}; {shoe[1]} CHF ; https://kikikickz.com/{shoe[2]}");
            File.AppendAllText(pathfile, $"{shoe[0]};{shoe[1]} CHF;https://kikikickz.com/{shoe[2]}\n");
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }
}