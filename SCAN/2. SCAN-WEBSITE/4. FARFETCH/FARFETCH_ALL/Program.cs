using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

HttpClient client = new HttpClient();

string baseUrl = "https://www.farfetch.com/ch/shopping/men/shoes-2/items.aspx?page=";
int totalPages = 2;

List<string> urls = new List<string>();

for (int page = 1; page <= totalPages; page++)
{
    string urlWeb = baseUrl + page + 1 + "&view=96&sort=3&scale=282";
    urls.Add(urlWeb);
}

// Dossier local pour stocker les données extraites
string folder = @"D:\MSIG\PROGRAMMATION\0. TMSP\SCAN\2. DATA-SCAN\FARFETCH";
// Chemin du fichier avec le nom incluant la date actuelle
string pathfile = Path.Combine(folder, ($"SNKRS-FARFETCH-{DateTime.Now:yyyyMMdd}.csv"));

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

        var nameMatches = Regex.Matches(html, "\"name\":\"([^\"]+)\"");
        foreach (Match nameMatch in nameMatches)
        {
            string subPart = html.Substring(nameMatch.Index);

            var brandMatch = Regex.Match(subPart, "\"brand\":\\{\"@type\":\"Brand\",\"name\":\"([^\"]+)\"");
            string brand = brandMatch.Groups[1].Value;

            var priceMatch = Regex.Match(subPart, "\"price\":(\\d+)");
            string price = priceMatch.Groups[1].Value;

            var urlMatch = Regex.Match(subPart, "\"url\":\"([^\"]+)\"");
            string productUrl = urlMatch.Groups[1].Value;

            if (!(nameMatch.Groups[1].Value.ToLower().Contains("men") ||
                      nameMatch.Groups[1].Value.ToLower().Contains("s") ||
                      nameMatch.Groups[1].Value.ToLower().Contains("y-3")))
            {
                shoes.Add(new List<string> { nameMatch.Groups[1].Value, brand, price, "https://www.farfetch.com/" + productUrl });
            }
        }

        foreach (var shoe in shoes)
        {
            Console.WriteLine($"{shoe[1]} ; {shoe[0]} ; {shoe[2]} CHF ; {shoe[3]}");
            File.AppendAllText(pathfile, $"{shoe[1]};{shoe[0]};{shoe[2]} CHF;{shoe[3]}\n");
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }
}