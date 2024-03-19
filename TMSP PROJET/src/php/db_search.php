
<!--
Auteur      : Liandro Gameiro
Date        : 08.02.2024
Description : PERMET DE RECHERCHER L'INFORMATION DANS LA BASE DE DONNEES PAR RAPPORT A LA RECHERCHE
-->

<?php
include "../connection/databases.php";

// Vérifier s'il y a une recherche
if (isset($_GET['search'])) {
    $searchTerm = $_GET['search'];

    // Requête pour récupérer les résultats de la recherche
    $query = "
        SELECT id, 'Wethenew' as source, brand, model, price, url, image 
        FROM Wethenew 
        WHERE brand LIKE '%$searchTerm%' OR model LIKE '%$searchTerm%'
        UNION
        SELECT id, 'Stockx' as source, brand, model, price, url, image 
        FROM Stockx
        WHERE brand LIKE '%$searchTerm%' OR model LIKE '%$searchTerm%'
        LIMIT 50
    ";

    $sth = $db->query($query);
    
    // Inclure Results.php pour afficher les résultats de la recherche
    include "Results.php";

} else {
    echo "Aucun terme de recherche fourni.";
}

$db = null;
?>

