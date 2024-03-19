using Microsoft.Data.Sqlite;
using System.Diagnostics;

/**
 * ETML - MSIG
 * Auteur : Liandro Gameiro
 * Date : 08.02.2024
 * Description : PROGRAMME C# PERMETTANT DE NAVIGUER DANS UN COMPARATEUR CONSOLE OU WEB (A CHOIX)
 */

DisplayWelcomeScreen();
DisplayMainMenu();

#region StartEnd

void DisplayWelcomeScreen()
{
    // Couleur du site web / le nom
    Console.Title = "MySneakers";
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.White;
    Console.CursorVisible = false;

    // Page de chargement avant le début du site
    // Source du texte ASCII : https://patorjk.com/software/taag/#p=display&f=Graffiti&t=salut%20
    string textLoading = @"
 ███    ███ ██    ██ ███████ ███    ██ ███████  █████  ██   ██ ███████ ██████  ███████ 
 ████  ████  ██  ██  ██      ████   ██ ██      ██   ██ ██  ██  ██      ██   ██ ██      
 ██ ████ ██   ████   ███████ ██ ██  ██ █████   ███████ █████   █████   ██████  ███████ 
 ██  ██  ██    ██         ██ ██  ██ ██ ██      ██   ██ ██  ██  ██      ██   ██      ██ 
 ██      ██    ██    ███████ ██   ████ ███████ ██   ██ ██   ██ ███████ ██   ██ ███████ 

                                                                              
{Console Edition v 1.56}
";

    string[] linesLoading = textLoading.Split("\n");
    int heightLoading = Console.WindowHeight / 2 - 5;



    for (int i = 0; i < linesLoading.Length; i++)
    {
        Console.CursorLeft = Console.WindowWidth / 2 - (linesLoading[i].Length / 2);
        Console.CursorTop = heightLoading + i;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(linesLoading[i]);
    }

    while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
    {
        string textEnter = ("APPUYER SUR <ENTER> POUR ENTRER");
        int widthEnter = Console.WindowWidth;
        int leftEnter = (widthEnter - textEnter.Length) / 2;

        int windowHeightEnter = Console.WindowHeight;
        int centerEnter = (windowHeightEnter + 10) / 2;
        Console.SetCursorPosition((Console.WindowWidth - textEnter.Length) / 2, centerEnter);
        Console.SetCursorPosition(leftEnter, Console.CursorTop);

        Console.SetCursorPosition(leftEnter, centerEnter);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(textEnter);
        Thread.Sleep(500);

        Console.SetCursorPosition(leftEnter, centerEnter);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(textEnter);
        Thread.Sleep(500);
    }
}

void Exitprogram()
{
    Console.Clear();
    Console.Title = "MySneakers - Quit";

    Console.ForegroundColor = ConsoleColor.White;

    // Source du texte ASCII : https://patorjk.com/software/taag/#p=display&f=Graffiti&t=salut%20
    Console.CursorVisible = false;
    string goodbyeText = @"
         █████  ██    ██     ██████  ███████ ██    ██  ██████  ██ ██████  
        ██   ██ ██    ██     ██   ██ ██      ██    ██ ██    ██ ██ ██   ██ 
        ███████ ██    ██     ██████  █████   ██    ██ ██    ██ ██ ██████  
        ██   ██ ██    ██     ██   ██ ██       ██  ██  ██    ██ ██ ██   ██ 
        ██   ██  ██████      ██   ██ ███████   ████    ██████  ██ ██   ██ 
        ";
    string[] goodbyeScreen = goodbyeText.Split("\n");
    int goodbyeScreenHeight = Console.WindowHeight / 2 - 6;

    for (int i = 0; i < goodbyeScreen.Length; i++)
    {
        Console.CursorLeft = Console.WindowWidth / 2 - (goodbyeScreen[i].Length / 2 - 2);
        Console.CursorTop = goodbyeScreenHeight + i;
        Console.WriteLine(goodbyeScreen[i]);
    }

    Thread.Sleep(500);
    Environment.Exit(0);
}

#endregion

#region MenuApp

void DisplayMainMenu()
{
    while (true)
    {
        // MENU SOURCE : https://www.youtube.com/watch?v=YyD1MRJY0qI
        Console.Title = "MySneakers - Menu";
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.White;

        int optionWelcome = 1;
        bool isSelectedWelcome = false;
        string colorWelcome = "    \u001B[32m";

        while (!isSelectedWelcome)
        {
            int topWelcome = Console.WindowHeight / 2;
            // Source du texte ASCII : https://patorjk.com/software/taag/#p=display&f=Graffiti&t=salut%20
            string welcome = @"
██████  ██ ███████ ███    ██ ██    ██ ███████ ███    ██ ██    ██ ███████ 
██   ██ ██ ██      ████   ██ ██    ██ ██      ████   ██ ██    ██ ██      
██████  ██ █████   ██ ██  ██ ██    ██ █████   ██ ██  ██ ██    ██ █████   
██   ██ ██ ██      ██  ██ ██  ██  ██  ██      ██  ██ ██ ██    ██ ██      
██████  ██ ███████ ██   ████   ████   ███████ ██   ████  ██████  ███████ 
";

            string[] welcomeLines = welcome.Split("\n");
            int welcomeHeight = Console.WindowHeight / 2 - 6;

            for (int i = 0; i < welcomeLines.Length; i++)
            {
                Console.CursorLeft = Console.WindowWidth / 2 - (welcomeLines[i].Length / 2 - 4);
                Console.CursorTop = welcomeHeight + i;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(welcomeLines[i]);
            }

            Console.SetCursorPosition((Console.WindowWidth - "Recherchez une paire".Length) / 2, topWelcome + 3);
            Console.WriteLine($"{(optionWelcome == 1 ? colorWelcome : "    ")}Recherchez une paire\u001b[0m");

            Console.SetCursorPosition((Console.WindowWidth - "Quitter".Length) / 2, topWelcome + 4);
            Console.WriteLine($"{(optionWelcome == 2 ? colorWelcome : "    ")}Quitter\u001b[0m");

            var keyWelcome = Console.ReadKey(true);

            switch (keyWelcome.Key)
            {
                case ConsoleKey.UpArrow:
                    optionWelcome = optionWelcome == 1 ? 2 : optionWelcome - 1;
                    break;

                case ConsoleKey.DownArrow:
                    optionWelcome = optionWelcome == 2 ? 1 : optionWelcome + 1;
                    break;

                case ConsoleKey.Enter:
                    isSelectedWelcome = true;
                    break;
            }
        }
        
        
        if (optionWelcome == 1)
        {
            DisplayStoreMenu();
        }
        else if (optionWelcome == 2)
        {
            Exitprogram();
        }
    }
}

void DisplayStoreMenu()
{
    // MENU SOURCE : https://www.youtube.com/watch?v=YyD1MRJY0qI
    while (true)
    {
        Console.Clear();
        Console.Title = "MySneakers - Store";

        int optionStore = 1;
        bool isSelectedStore = false;
        string colorStore = "    \u001b[32m";
        // Source du texte ASCII : https://patorjk.com/software/taag/#p=display&f=Graffiti&t=salut%20
        string settings = @"
███    ███  █████   ██████   █████  ███████ ██ ███    ██ 
████  ████ ██   ██ ██       ██   ██ ██      ██ ████   ██ 
██ ████ ██ ███████ ██   ███ ███████ ███████ ██ ██ ██  ██ 
██  ██  ██ ██   ██ ██    ██ ██   ██      ██ ██ ██  ██ ██ 
██      ██ ██   ██  ██████  ██   ██ ███████ ██ ██   ████ 
";

        string[] storeLines = settings.Split("\n");
        int storeHeight = Console.WindowHeight / 2 - 6;

        for (int i = 0; i < storeLines.Length; i++)
        {
            Console.CursorLeft = Console.WindowWidth / 2 - (storeLines[i].Length / 2 - 4);
            Console.CursorTop = storeHeight + i;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(storeLines[i]);
        }

        while (!isSelectedStore)
        {
            int topStoreMenu = Console.WindowHeight / 2 - 1;
            Console.SetCursorPosition((Console.WindowWidth - "Recherche sur la console".Length) / 2, topStoreMenu + 3);
            Console.WriteLine($"{(optionStore == 1 ? colorStore : "    ")}Recherche sur la console\u001b[0m");

            Console.SetCursorPosition((Console.WindowWidth - "Recherche sur le web".Length) / 2, topStoreMenu + 4);
            Console.WriteLine($"{(optionStore == 2 ? colorStore : "    ")}Recherche sur le web\u001b[0m");

            Console.SetCursorPosition((Console.WindowWidth - "Retourner en arrière".Length) / 2, topStoreMenu + 5);
            Console.WriteLine($"{(optionStore == 3 ? colorStore : "    ")}Retourner en arrière\u001b[0m");

            var keyStore = Console.ReadKey(true);

            switch (keyStore.Key)
            {
                case ConsoleKey.UpArrow:
                    optionStore = optionStore == 1 ? 3 : optionStore - 1;
                    break;

                case ConsoleKey.DownArrow:
                    optionStore = optionStore == 3 ? 1 : optionStore + 1;
                    break;

                case ConsoleKey.Enter:
                    isSelectedStore = true;
                    break;
            }
        }

        if (optionStore == 1)
        {
            Console.Clear();
            Console.Write("Voulez-vous appliquer des filtres ? (o/n)");
            string applyFilterOption = Console.ReadLine().ToLower();

            if (applyFilterOption == "o" || applyFilterOption == "oui")
            {
                ApplyFilters();
            }
            else
            {
                Console.WriteLine("Filtres non appliqués. Affichages des tous les résultats.");
                Thread.Sleep(500);
                Console.Clear();
                DisplayResults();
            }

        }
        else if (optionStore == 2)
        {
            Console.Clear();
            PHPSearch();
        }
        else if (optionStore == 3)
        {
            break;
        }
    }
}

#endregion

void PHPSearch()
{
    // AFONSO MA FOURNIT CE CODE
    ProcessStartInfo processStartInfo = new ProcessStartInfo
    {
        UseShellExecute = true,
        FileName = $"https://msig.section-inf.ch/tmsp23/liandro/TMSP%20PROJET/src/php/Index.php",
    };
    Process.Start(processStartInfo);
}

#region StoreSearch

void ApplyFilters()
{
    Console.Clear();
    Console.CursorVisible = false;

    string[] filterLines = @"
            Rechercher (couleur, marque, ...) : 
            Prix minimum :
            Prix maximum :
            ".Split("\n");

    int filterHeight = Console.WindowHeight / 2 - (filterLines.Length / 2);

    for (int i = 0; i < filterLines.Length; i++)
    {
        Console.CursorLeft = Console.WindowWidth / 2 - (filterLines[i].Length / 2);
        Console.CursorTop = filterHeight + i;
        Console.WriteLine(filterLines[i]);
    }
    Console.Clear();

    Console.CursorLeft = Console.WindowWidth / 2 - 14;
    Console.CursorTop = filterHeight;
    Console.Write("Rechercher (couleur, marque, ...) : ");
    string searchTerm = Console.ReadLine();
    Console.Clear();

    Console.CursorLeft = Console.WindowWidth / 2 - 14;
    Console.CursorTop = filterHeight;
    Console.Write("Prix minimum : ");
    string inputminPrice = Console.ReadLine();

    if (!int.TryParse(inputminPrice, out int minPrice))
    {
        minPrice = int.MinValue;
    }
    Console.Clear();

    Console.CursorLeft = Console.WindowWidth / 2 - 14;
    Console.CursorTop = filterHeight + 1;
    Console.Write("Prix maximum : ");
    string inputmaxPrice = Console.ReadLine();

    if (!int.TryParse(inputmaxPrice, out int maxPrice))
    {
        maxPrice = int.MaxValue;
    }
    Console.Clear();

    Console.CursorLeft = Console.WindowWidth / 2 - 14;
    Console.CursorTop = filterHeight + 1;
    Console.Write("Revendeur (Stockx, Wethenew) : ");
    string inputReseller = Console.ReadLine();
    Console.Clear();

    string resellerCondition;

    // AIDE DE CHATGPT POUR CES REQUÊTE SQL / DEBUT SQL EN BASE DE DONNEE
    if (string.IsNullOrEmpty(inputReseller))
    {
        resellerCondition = "AND source IN ('Stockx', 'Wethenew')";
    }
    else
    {
        resellerCondition = "AND source = @inputReseller";
    }

    string request = $@"
            SELECT id, source, model, price, url
            FROM (
            SELECT id, 'Stockx' as source, model, price, url
            FROM Stockx
            WHERE (brand LIKE @searchTerm OR model LIKE @searchTerm)
                    AND price BETWEEN @minPrice AND @maxPrice

            UNION

            SELECT id, 'Wethenew' as source, model, price, url
            FROM Wethenew
            WHERE (brand LIKE @searchTerm OR model LIKE @searchTerm)
                    AND price BETWEEN @minPrice AND @maxPrice

            ) AS CombinedResellers
            WHERE source IN ('Stockx', 'Wethenew') {resellerCondition}";

    // MODIFIER EN METTANT LE CHEMIN D'ACCES DE LA BASE DE DONNEES
    using var connection = new SqliteConnection(@"Data Source = D:\3. ECOLE\MSIG\2. PROGRAMMATION\0. TMSP\SCAN\1. OUTILS\MySneakers");
    connection.Open();

    using SqliteCommand command = new SqliteCommand(request, connection);
    command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
    command.Parameters.AddWithValue("@minPrice", minPrice);
    command.Parameters.AddWithValue("@maxPrice", maxPrice);

    try
    {
        int PageSize
        = 22;

        using SqliteDataReader reader = command.ExecuteReader();
        Console.WriteLine("Voici tous les résultats de votre recherche : \n");

        Console.WriteLine("Reseller   model\t\t\t\t\tPrice\t  URL\n");

        int count = 0;

        while (reader.Read())
        {
            // Si Count divisé par PageSize donne zéro (nombre entier de Pagesize) et que Count n'est pas égale à zéro
            if (count % PageSize == 0 && count != 0)
            {
                Console.WriteLine("\nAppuyez sur <ENTER> pour afficher la page suivante...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Voici tous les résultats de votre recherche : \n");
                Console.WriteLine("Reseller   model\t\t\t\t\tPrice\t  URL\n");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{reader["source"],-12}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            string name = reader["model"].ToString();

            // PERMET DE LIMITER LE NOMBRE DE CHARACTERE PAR UN NOMBRE DEFINIT
            if (name.Length <= 36)
            {
                Console.Write($"{name,-36}");
            }
            else
            {
                Console.Write($"{name.Substring(0, 30)}...   ");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{reader["price"],-10}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            string url = reader["url"].ToString();

            if (url.Length <= 54)
            {
                Console.WriteLine($"{url,-55}");
            }
            else
            {
                Console.WriteLine($"{url.Substring(0, 49)}...   ");
            }
            Console.ResetColor();

            count++;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
    }
}

void DisplayResults()
{
    string request = $@"
SELECT id, 'Stockx' as source, model, price, url FROM Stockx

UNION

SELECT id, 'Wethenew' as source, model, price, url FROM Wethenew";

    // MODIFIER EN METTANT LE CHEMIN D'ACCES DE LA BASE DE DONNEES
    using var connection = new SqliteConnection(@"Data Source = D:\3. ECOLE\MSIG\2. PROGRAMMATION\0. TMSP\SCAN\1. OUTILS\MySneakers");
    connection.Open();

    using SqliteCommand command = new SqliteCommand(request, connection);
    Console.WriteLine("Filtres non appliqués. Affichage de tous les résultats.");

    try
    {
        int PageSize = 22;

        using SqliteDataReader reader = command.ExecuteReader();
        Console.WriteLine("Voici tous les résultats de votre recherche : \n");

        Console.WriteLine("Reseller   model\t\t\t\t\tPrice\t  URL\n");

        int count = 0;

        while (reader.Read())
        {
            // Si Count divisé par PageSize donne zéro (nombre entier de Pagesize) et que Count n'est pas égale à zéro
            if (count % PageSize == 0 && count != 0)
            {
                Console.WriteLine("\nAppuyez sur <ENTER> pour afficher la page suivante...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Voici tous les résultats de votre recherche : \n");
                Console.WriteLine("Reseller   model\t\t\t\t\tPrice\t  URL\n");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{reader["source"],-12}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            string name = reader["model"].ToString();

            if (name.Length <= 36)
            {
                Console.Write($"{name,-36}");
            }
            else
            {
                Console.Write($"{name.Substring(0, 30)}...   ");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{reader["price"],-10}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            string url = reader["url"].ToString();

            if (url.Length <= 54)
            {
                Console.WriteLine($"{url,-55}");
            }
            else
            {
                Console.WriteLine($"{url.Substring(0, 49)}...   ");
            }
            Console.ResetColor();

            count++;
        }

        ConsoleKeyInfo key = Console.ReadKey();

        if (key.Key == ConsoleKey.UpArrow)
        {

        }
        else if (key.Key == ConsoleKey.DownArrow)
        {

        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
    }
}

#endregion