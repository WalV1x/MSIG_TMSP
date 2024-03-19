
<!--
Auteur      : Liandro Gameiro
Date        : 08.02.2024
Description : PAGE PERMETTANT TOUT LE SYSTEME DE PAGINATION DU SITE WEB
-->

<?php
include "../connection/databases.php";

$recordsPerPage = 24;
$current_page = isset($_GET['page']) ? max(1, intval($_GET['page'])) : 1;
$offset = ($current_page - 1) * $recordsPerPage;

$query = "";
$totalRecords = 0;

// Vérifier s'il y a une recherche de marque
if(isset($_GET['brand'])) {
    $selectedBrand = $_GET['brand'];

    $query = "
        SELECT COUNT(id) as totalRecords FROM (
            SELECT id FROM Wethenew WHERE brand = :brand
            UNION
            SELECT id FROM Stockx WHERE brand = :brand
        ) AS combined";

    $stmt = $db->prepare($query);
    $stmt->bindParam(':brand', $selectedBrand, PDO::PARAM_STR);
    $stmt->execute();
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    $totalRecords = $row['totalRecords'];

} else {
    // Si aucune recherche de marque, compter tous les enregistrements
    $query = "SELECT COUNT(id) as totalRecords FROM (SELECT id FROM Wethenew UNION SELECT id FROM Stockx) AS combined";

    $stmt = $db->query($query);
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    $totalRecords = $row['totalRecords'];
}

$totalPages = ceil($totalRecords / $recordsPerPage);

$db = null;

// Construction du lien pour la page précédente
$previousPageLink = "?page=" . max(1, $current_page - 1);
if(isset($_GET['brand'])) {
    $previousPageLink .= "&brand=" . urlencode($selectedBrand);
}
if(isset($_GET['order'])) {
    $previousPageLink .= "&order=" . urlencode($_GET['order']);
}

// Construction du lien pour la page suivante
$nextPageLink = "?page=" . min($totalPages, $current_page + 1);
if(isset($_GET['brand'])) {
    $nextPageLink .= "&brand=" . urlencode($selectedBrand);
}
if(isset($_GET['order'])) {
    $nextPageLink .= "&order=" . urlencode($_GET['order']);
}
?>

<div class="bg-white">
    <div class="max-w-screen-xl mx-auto bt-12 px-4 text-gray-600 md:px-8">
        <div class="flex items-center justify-between text-sm text-gray-600 font-medium">
            <a href="<?= $previousPageLink ?>" class="px-4 py-2 border rounded-lg duration-150 hover:bg-gray-50">Previous</a>
            <div>
                Page <?= $current_page ?> of <?= $totalPages ?>
            </div>
            <a href="<?= $nextPageLink ?>" class="px-4 py-2 border rounded-lg duration-150 hover:bg-gray-50">Next</a>
        </div>
    </div>
</div>

<div class="bg-white">
    <br>
    <br>
</div>