using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

HttpClient client = new HttpClient();

var urls = new string[]
{
    "https://wethenew.com/collections/all-sneakers?page=2"

    // Ajoutez d'autres URLs au besoin
};


// Dossier local pour stocker les données extraites
string folder = @"D:\MSIG\PROGRAMMATION\0. TMSP\SCAN\3. DATA-SCAN\WETHENEW";
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

        var modelMatch = Regex.Matches(html, "\"model\":\"([^\"]+)");
        if (modelMatch.Count > 0)
        {
            foreach (Match match2 in modelMatch)
            {
                string subPart = html.Substring(match2.Index);

                var nameMatch = Regex.Match(subPart, "\"name\":\"([^\"]+)\"");
                string name = nameMatch.Groups[1].Value;

                var brandMatch = Regex.Match(subPart, "\"brand\":\"([^\"]+)\"");
                string brand = brandMatch.Groups[1].Value;

                var releaseDateMatch = Regex.Match(subPart, "\"releaseDate\":\"([^\"]+)\"");
                string releaseDate = releaseDateMatch.Groups[1].Value;

                var lowPriceMatch = Regex.Match(subPart, "\"lowPrice\":(\\d+)");
                string lowPrice = lowPriceMatch.Groups[1].Value;

                var urlMatch = Regex.Match(subPart, "\"url\":\"([^\"]+)\"");
                string urlsnkrs = urlMatch.Groups[1].Value;

                if (!name.ToLower().Contains("retail"))
                {
                    shoes.Add(new List<string>() { match2.Groups[1].Value, brand, name, releaseDate, lowPrice, urlsnkrs });
                }

            }
        }
        foreach (var shoe in shoes)
        {
            Console.WriteLine($"{shoe[1]} ; {shoe[0]} ; {shoe[2]} ; {shoe[3]} ; {shoe[4]} CHF ; {shoe[5]}");
            File.AppendAllText(pathfile, $"{shoe[1]};{shoe[0]};{shoe[2]};{shoe[3]};{shoe[4]} CHF;{shoe[5]}" + "\n");

        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }
}