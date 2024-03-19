
<!--
Auteur      : Liandro Gameiro
Date        : 08.02.2024
Description : PERMET DE CONNECTER LA BASE DE DONNEE
-->

<?php
try {
    $db = new PDO("mysql:host=msig.section-inf.ch;port=3306;dbname=db_msig_liandro", "lilianandrdroo", "49R8zs@b3");
} catch (PDOException $e) {
    echo "Erreur de connexion à la base de données : " . $e->getMessage();
    exit();
}
?>