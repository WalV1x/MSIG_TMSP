using System;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

HttpClient client = new HttpClient();

string baseUrl = "https://wethenew.com/collections/all-sneakers?page=";
int totalPages = 91;

List<string> urls = new List<string>();

for (int page = 1; page <= totalPages; page++)
{
    string urlWeb = baseUrl + page;
    urls.Add(urlWeb);
}

// Dossier local pour stocker les données extraites
string folder = @"C:\Users\pt50cuy\Desktop\PHP\SCAN\2. DATA-SCAN\WETHENEW";
// Chemin du fichier avec le nom incluant la date actuelle
string pathfile = Path.Combine(folder, ($"SNKRS-WETHENEW-{DateTime.Now:yyyyMMdd}.csv"));

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
        request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

        // Envoi de la requête et récupération de la réponse
        using HttpResponseMessage response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        string html = await response.Content.ReadAsStringAsync();

        List<List<string>> shoes = new List<List<string>>();

        var brandMatch = Regex.Matches(html, "\"vendor\":\"([^\"]+)\"");
        if (brandMatch.Count > 0)
        {
            foreach (Match match2 in brandMatch)
            {
                string subPart = html.Substring(match2.Index);

                var ModelMatch = Regex.Match(subPart, "\"title\":\"([^\"]+)");
                string Model = ModelMatch.Groups[1].Value;

                var priceMatch = Regex.Match(subPart, "\"price\":(\\d+)");
                string price = priceMatch.Groups[1].Value;

                var imageMatch = Regex.Match(subPart, "\"src\":\"([^\"]+)\"");
                string image = imageMatch.Groups[1].Value;

                var urlMatch = Regex.Match(subPart, "\"href\":\"([^\"]+)\"");
                string urlsnkrs = urlMatch.Groups[1].Value;

                if (!Model.Contains("Wethenew") &&
                !Model.Contains("voir aussi") &&
                !Model.Contains("Collection") &&
                !Model.Contains("Baskets tendances") &&
                !Model.Contains("Toutes les Sneakers"))
                {
                    shoes.Add(new List<string>() { match2.Groups[1].Value, Model, price, "https://wethenew.com" + urlsnkrs, image });
                }
            }
        }
        foreach (var shoe in shoes)
        {
            Console.WriteLine($"{shoe[0]} ; {shoe[1]} ; {shoe[2]}; {shoe[3]}; {shoe[4]}");
            File.AppendAllText(pathfile, $"{shoe[0]};{shoe[1]};{shoe[2]};{shoe[3]};{shoe[4]}" + "\n");

        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }
}