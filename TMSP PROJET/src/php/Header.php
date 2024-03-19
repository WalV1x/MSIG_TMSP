
<!--
Auteur      : Liandro Gameiro
Date        : 08.02.2024
Description : PAGE QUI A POUR BUT D'AFFICHER LE HAUT DE PAGE
-->

<?php
include "../connection/databases.php";

$query = $db->query("SELECT COUNT(*) AS total FROM `Like`");
$row = $query->fetch(PDO::FETCH_ASSOC);
$total = $row['total'];
?>

<!-- SOURCE : https://daisyui.com/components/button/ -->
<link href="https://cdn.jsdelivr.net/npm/daisyui@4.6.0/dist/full.min.css" rel="stylesheet" type="text/css" />
<script src="https://cdn.tailwindcss.com"></script>
<div class="navbar bg-base-100 flex justify-between items-center">
    <form action="Index.php">
        <button style="color: white; font-weight: bold" type="submit" class="btn btn-ghost text-xl">MySneaker App</button>
    </form>
    <!-- SOURCE : https://htmlcodex.com/online-shop-website-template/ -->
    <div class="flex justify-center items-center">
        <a href="Like.php" class="btn px-0">
            <i class="fas fa-heart text-white"></i>
            <span class="badge text-secondary border border-secondary rounded-full" style="padding: 2px;"><?php echo $total; ?></span>
        </a>
    </div>
</div>