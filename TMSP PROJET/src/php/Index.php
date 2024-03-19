
<!--
Auteur      : Liandro Gameiro
Date        : 08.02.2024
Description : PAGE PRINCIPAL DU SITE, PERMETTANT DE NAVIGUER A TRAVERS LES DIFFERENTS PARAMETRES
-->

<?php
include "../connection/databases.php";

$recordsPerPage = 24;
$current_page = isset($_GET['page']) ? max(1, intval($_GET['page'])) : 1;
$offset = ($current_page - 1) * $recordsPerPage;

$order = isset($_GET['order']) && ($_GET['order'] === 'desc') ? 'DESC' : 'ASC';

$selectedBrand = isset($_GET['brand']) ? $_GET['brand'] : '';

if(isset($_GET['brand'])) {
    $selectedBrand = $_GET['brand'];

    $query = "
        SELECT id, 'Wethenew' as source, brand, model, price, url, image FROM Wethenew WHERE brand = :brand
        UNION
        SELECT id, 'Stockx' as source, brand, model, price, url, image FROM Stockx WHERE brand = :brand";

    if(isset($_GET['order'])) {
        $query .= " ORDER BY price $order";
    }

    $query .= " LIMIT $offset, $recordsPerPage";

    $stmt = $db->prepare($query);
    $stmt->bindParam(':brand', $selectedBrand, PDO::PARAM_STR);
    $stmt->execute();
    $results = $stmt->fetchAll(PDO::FETCH_ASSOC);

} else {
    $query = "
        SELECT id, 'Wethenew' as source, brand, model, price, url, image FROM Wethenew
        UNION
        SELECT id, 'Stockx' as source, brand, model, price, url, image FROM Stockx";

    if(isset($_GET['order'])) {
        $query .= " ORDER BY price $order";
    }

    $query .= " LIMIT $offset, $recordsPerPage";

    $stmt = $db->prepare($query);
    $stmt->execute();
    $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
}

include "Header.php";
include "../html/Filters.html";
?>

<!-- SOURCE : https://htmlcodex.com/online-shop-website-template/ -->
<head>
    <meta charset="utf-8">
    <title>MySneakerApp - The best comparator</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">

    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap" rel="stylesheet">

    <!-- Libraries Stylesheet -->
    <link href="../css/animate.min.css" rel="stylesheet">
    <link href="../css/owl.carousel.min.css" rel="stylesheet">

    <!-- Customized Bootstrap Stylesheet -->
    <link href="../css/style.css" rel="stylesheet">
</head>

<!-- Shop Start -->
<div class="container-fluid">
    <div class="row justify-content-center px-xl-5">
        <!-- Shop Product Start -->
        <div class="col-xl-11 col-lg-9 col-md-8">
            <div class="row pb-3">
                <?php $counter = 0; ?>
                <?php foreach ($results as $row): ?>
                <?php if ($counter % 4 == 0): ?>
            </div><div class="row pb-3">
                <?php endif; ?>
                <div class="col-lg-3 col-md-3 col-sm-6 pb-1">
                    <div class="product-item bg-light mb-4">
                        <div class="product-img position-relative overflow-hidden">
                            <img class="img-fluid w-100" src="<?= $row['image'] ?>" alt="<?= $row['model'] ?>">
                            <div class="product-action">
                                <a class="btn btn-outline-dark btn-square" href="<?= $row['url'] ?>"><i class="fa fa-link"></i></a>

                                <!-- CHATGPT AIDE -->
                                <a class="btn btn-outline-dark btn-square" href="Like.php?model=<?= urlencode($row['model']) ?>&price=<?= urlencode($row['price']) ?>&brand=<?= urlencode($row['brand']) ?>&url=<?= urlencode($row['url']) ?>&image=<?= urlencode($row['image']) ?>">
                                    <i class="far fa-heart animate-heart"></i>
                                </a>

                            </div>
                        </div>
                        <div class="text-center py-4">
                            <span class="text-muted"><?= $row['source'] ?></span>
                            <br>
                            <a class="h6 text-decoration-none text-truncate" href="<?= $row['url'] ?>"><?= $row['model'] ?></a>
                            <div class="d-flex align-items-center justify-content-center mt-2">
                                <h5><?= $row['price'] ?> CHF</h5>
                            </div>
                        </div>
                    </div>
                </div>
                <?php $counter++; ?>
                <?php endforeach; ?>
            </div>
        </div>
    </div>
</div>
<!-- Shop End -->

<?php
include "Pagination.php";
include "../html/Footer.html";

$db = null;
?>