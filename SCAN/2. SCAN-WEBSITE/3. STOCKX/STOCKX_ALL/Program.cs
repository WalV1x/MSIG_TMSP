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

    "https://stockx.com/shoes",
    "https://stockx.com/shoes?page=2",
    "https://stockx.com/shoes?page=3",
    "https://stockx.com/shoes?page=4",
    "https://stockx.com/shoes?page=5",
    "https://stockx.com/shoes?page=6",
    "https://stockx.com/shoes?page=7",
    "https://stockx.com/shoes?page=8",
    "https://stockx.com/shoes?page=9",
    "https://stockx.com/shoes?page=10",
    "https://stockx.com/shoes?page=11",
    "https://stockx.com/shoes?page=12",
    "https://stockx.com/shoes?page=13",
    "https://stockx.com/shoes?page=14",
    "https://stockx.com/shoes?page=15",
    "https://stockx.com/shoes?page=16",
    "https://stockx.com/shoes?page=17",
    "https://stockx.com/shoes?page=18",
    "https://stockx.com/shoes?page=19",
    "https://stockx.com/shoes?page=20",
    "https://stockx.com/shoes?page=21",
    "https://stockx.com/shoes?page=22",
    "https://stockx.com/shoes?page=23",
    "https://stockx.com/shoes?page=24",
    "https://stockx.com/shoes?page=25"

    // Ajoutez d'autres URLs au besoin
};

// Dossier local pour stocker les données extraites
string folder = @"D:\3. ECOLE\MSIG\2. PROGRAMMATION\0. TMSP\SCAN\2. DATA-SCAN\STOCKX";
// Chemin du fichier avec le nom incluant la date actuelle
string pathfile = Path.Combine(folder, ($"SNKRS-STOCKX-{DateTime.Now:yyyyMMdd}.csv"));

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
        Thread.Sleep(2000);

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
        //string html = "\"model\":\"Nike Dunk Low\",\"name\":\"Nike Dunk Low Photon Dust(Women's)\",\"releaseDate\":\"2021-05-19\",\"sku\":\"796882e1-685f-4bbb-8720-075b76db84dd\",\"url\":\"https://stockx.com/nike-dunk-low-photon-dust-w\",\"offers\":{\"@type\":\"AggregateOffer\",\"lowPrice\":80,";// await response.Content.ReadAsStringAsync();
        //string html = File.ReadAllText("data.txt");
        string html = await response.Content.ReadAsStringAsync();

        //"model":"Nike Dunk Low","name":"Nike Dunk Low Photon Dust (Women's)","releaseDate":"2021-05-19","sku":"796882e1-685f-4bbb-8720-075b76db84dd","url":"https://stockx.com/nike-dunk-low-photon-dust-w","offers":{"@type":"AggregateOffer","lowPrice":80,
        //Regex regex = new Regex("\"model\":\"([^\"]+).*\"name\":\"([^\"]+)\".*\"releaseDate\":\"([^\"]+)\".*\"lowPrice\":(\\d+)",options);


            //"@context":"https://schema.org/","@type":"OfferCatalog","itemListElement":[{ "@type":"ListItem","position":1,"item":{ "@type":"Product",
            //"brand":"Jordan","color":"--","description":"The Jordan 1 Retro High OG in Yellow Ochre/Black/Sail is a
            //vibrant celebration of the Air Jordan legacy. This edition's Yellow Ochre colorway brings an electrifying
            //twist to the classic silhouette, making it a standout piece in any sneaker collection.<br><br>Accented with
            //Black and Sail, the sneaker's bold Yellow Ochre hue ensures that it's not just another pair of Jordans; it's
            //a fashion statement. Crafted with excellence, this sneaker combines high-quality materials and comfort, embodying
            //the spirit of Michael Jordan's legendary performance.<br><br>Released on January 13, 2024, with a retail tag of $180,
            //the Jordan 1 Retro High OG Yellow Ochre is a piece of sneaker history, a canvas of artistry, and a symbol of enduring
            //style. It's a must-have for those who value both legacy and contemporary fashion.",
            //"image":"https://images.stockx.com/images/Air-Jordan-1-Retro-High-OG-Yellow-Ochre-Product.jpg?fit=fill&bg=FFFFFF&w=1200&h=857&fm=avif&auto=compress&dpr=2&trim=color&updated_at=1704312218&q=60","itemCondition":"https://schema.org/NewCondition","model":"Jordan 1 Retro High OG","name":"Jordan 1 Retro High OG Yellow Ochre","releaseDate":"2024-01-13","sku":"f038c7aa-1eff-4ae6-a8b9-803f05b35c44","url":"https://stockx.com/air-jordan-1-retro-high-og-yellow-ochre","offers":{ "@type":"AggregateOffer","lowPrice":145,"highPrice":145,"priceCurrency":"CHF","url":"https://stockx.com/air-jordan-1-retro-high-og-yellow-ochre"} } },
        List<List<string>> shoes = new List<List<string>>();

        var modelMatch = Regex.Matches(html, "\"model\":\"([^\"]+)");
        if (modelMatch.Count > 0)
        {
            foreach (Match match2 in modelMatch)
            {
                string subPart = html.Substring(match2.Index);

                var brandMatch = Regex.Match(subPart, "\"brand\":\"([^\"]+)\"");
                string brand = brandMatch.Groups[1].Value;

                var lowPriceMatch = Regex.Match(subPart, "\"lowPrice\":(\\d+)");
                string lowPrice = lowPriceMatch.Groups[1].Value;

                var urlMatch = Regex.Match(subPart, "\"url\":\"([^\"]+)\"");
                string urlsnkrs = urlMatch.Groups[1].Value;

                var imageMatch = Regex.Match(subPart, "\"image\":\"([^\"]+)\"");
                string imagesnkrs = imageMatch.Groups[1].Value;

                if (!name.ToLower().Contains("retail"))
                {
                    shoes.Add(new List<string>() { match2.Groups[1].Value, brand, name, lowPrice, urlsnkrs, imagesnkrs });
                }

            }
        }
        foreach (var shoe in shoes)
        {
            Console.WriteLine($"{shoe[1]} ; {shoe[0]} ; {shoe[2]} ; {shoe[3]} ; {shoe[4]}");
            File.AppendAllText(pathfile, $"{shoe[1]};{shoe[0]};{shoe[2]};{shoe[3]};{shoe[4]}" + "\n");

        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }
}