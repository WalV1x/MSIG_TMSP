
<!--
Auteur      : Liandro Gameiro
Date        : 08.02.2024
Description : PERMET D'AFFICHER LES LIKES DE L'UTILISATEURS SUR UNE BASE DE DONNEE
-->

<?php
include "../connection/databases.php";

if (
    isset($_GET['model']) &&
    isset($_GET['price']) &&
    isset($_GET['brand']) &&
    isset($_GET['url']) &&
    isset($_GET['image'])
) {
    $model = $_GET['model'];
    $price = $_GET['price'];
    $brand = $_GET['brand'];
    $url = $_GET['url'];
    $image = $_GET['image'];

    $check_query = "SELECT * FROM `Like` WHERE url = ?";
    $exit_stmt = $db->prepare($check_query);
    $exit_stmt->execute([$url]);
    $exist = $exit_stmt->fetchAll(PDO::FETCH_ASSOC);

    if (!$exist) {
        $query = "INSERT INTO `Like` (brand, model, price, url, image, date) 
          VALUES (?, ?, ?, ?, ?, CURRENT_DATE)";

        $sth = $db->prepare($query);
        $sth->execute([$brand, $model, $price, $url, $image]);
    }
}

if (isset($_POST['remove_id'])) {
    $remove_id = $_POST['remove_id'];
    $delete_query = "DELETE FROM `Like` WHERE id = ?";
    $delete_stmt = $db->prepare($delete_query);
    $delete_stmt->execute([$remove_id]);
}

$query = "SELECT * FROM `Like`";
$sth = $db->query($query);
$rows = $sth->fetchAll(PDO::FETCH_ASSOC);

include "Header.php";
?>

<!-- SOURCE : https://htmlcodex.com/online-shop-website-template/ -->
<head>
    <meta charset="utf-8">
    <title>MySneakerApp - Like</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta content="Free HTML Templates" name="keywords">
    <meta content="Free HTML Templates" name="description">

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap" rel="stylesheet">

    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">

    <!-- Libraries Stylesheet -->
    <link href="../css/animate.min.css" rel="stylesheet">
    <link href="../css/owl.carousel.min.css" rel="stylesheet">

    <!-- Customized Bootstrap Stylesheet -->
    <link href="../css/style.css" rel="stylesheet">
</head>

<div class="container-fluid">
    <div class="row">
        <div class="col-12">
            <div class="table-responsive">
                <table class="table table-light table-borderless table-hover text-center">
                    <thead class="thead-dark">
                    <tr>
                        <th>Image</th>
                        <th>Marque</th>
                        <th>Modèle</th>
                        <th>Prix</th>
                        <th>Url</th>
                        <th>Supprimer</th>
                    </tr>
                    </thead>
                    <tbody>
                    <?php foreach ($rows as $row): ?>
                        <tr>
                            <td><img src="<?= $row['image'] ?>" alt="" style="width: 50px;"></td>
                            <td><?= $row['brand'] ?></td>
                            <td><?= $row['model'] ?></td>
                            <td><?= $row['price'] ?></td>
                            <td><a href="<?= $row['url'] ?>" target="_blank">Lien du produit</a></td>
                            <td>
                                <form method="post">
                                    <input type="hidden" name="remove_id" value="<?= $row['id'] ?>">
                                    <button type="submit" class="btn btn-sm btn-danger"><i class="fa fa-times"></i></button>
                                </form>
                            </td>
                        </tr>
                    <?php endforeach; ?>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>


<?php
include "../html/Footer.html";
?>