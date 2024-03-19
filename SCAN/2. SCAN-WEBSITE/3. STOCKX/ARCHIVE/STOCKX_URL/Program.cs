using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

HttpClient client = new HttpClient();

var urls = new string[]
{
    "https://stockx.com/sneakers",
    "https://stockx.com/sneakers?page=2",
    "https://stockx.com/sneakers?page=3",
    "https://stockx.com/sneakers?page=4",
    "https://stockx.com/sneakers?page=5",
    "https://stockx.com/sneakers?page=6",
    "https://stockx.com/sneakers?page=7",
    "https://stockx.com/sneakers?page=8",
    "https://stockx.com/sneakers?page=9",
    "https://stockx.com/sneakers?page=10",
    "https://stockx.com/sneakers?page=11",
    "https://stockx.com/sneakers?page=12",
    "https://stockx.com/sneakers?page=13",
    "https://stockx.com/sneakers?page=14",
    "https://stockx.com/sneakers?page=15",
    "https://stockx.com/sneakers?page=16",
    "https://stockx.com/sneakers?page=17",
    "https://stockx.com/sneakers?page=18",
    "https://stockx.com/sneakers?page=19",
    "https://stockx.com/sneakers?page=20",
    "https://stockx.com/sneakers?page=21",
    "https://stockx.com/sneakers?page=22",
    "https://stockx.com/sneakers?page=23",
    "https://stockx.com/sneakers?page=24",
    "https://stockx.com/sneakers?page=25",

    // Ajoutez d'autres URLs au besoin
};

var productsUrl = new List<string>();


// Dossier local pour stocker les données extraites
string folder = @"D:\MSIG\PROGRAMMATION\0. TMSP\SCAN\3. DATA-SCAN\STOCKX";
// Chemin du fichier avec le nom incluant la date actuelle
string pathfile = Path.Combine(folder, ($"SNKRS-STOCKX_URL-{DateTime.Now:yyyyMMdd}.csv"));

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

        // JUSTE SCANNER LES NOMS / LES PRIX
        Regex regex = new Regex("\"url\":\"([^\"]+)\"");
        var m = regex.Match(html);
        while (m.Success)
        {
            Thread.Sleep(1);

            for (int i = 1; i <= 2; i++)
            {
                Group g = m.Groups[i];
                CaptureCollection cc = g.Captures;
                for (int j = 0; j < cc.Count; j++)
                {
                    Capture c = cc[j];

                    string productUrl = c.ToString();

                    if (productsUrl.Contains(productUrl))
                    {
                        //
                    }
                    else
                    {
                        File.AppendAllText(pathfile, productUrl + "\n");
                        productsUrl.Add(productUrl);
                    }
                }
            }
            m = m.NextMatch();
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }
}

try
{
    // Lire le contenu du fichier
    List<string> lines = new List<string>(File.ReadAllLines(pathfile));

    // Définir les données à supprimer
    List<string> deleteData = new List<string>
    {
@"https://images.contentstack.io/v3/assets/blt818b0c67cf450811/blt14176d5ded65d322/5e8ca91d5a326f2a9a34571f/browse-headersSneakers.jpg",
@"https://stockx.com/nike/air-max",
@"https://stockx.com/adidas/ultra-boost",
@"https://stockx.com/nike-air-max-90-off-white-desert-ore",
@"https://images.contentstack.io/v3/assets/blt818b0c67cf450811/bltb4e7dea3d4e6df68/5db20b27a6470d6ab91cf05b/social-media.jpg",
@"https://stockx-assets.imgix.net/social-media.jpg"
    };
    // Supprimer les données spécifiques du fichier
    lines.RemoveAll(deleteData.Contains);

    // Réécrire le contenu modifié dans le fichier
    File.WriteAllLines(pathfile, lines);

    Console.WriteLine("Données spécifiques du fichier supprimées avec succès.");
}
catch (Exception ex)
{
    Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
}