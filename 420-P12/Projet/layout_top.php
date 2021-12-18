<?php
require ("config.php");
include ("functions.php");
require_once ("classes/database.php");
require_once ("classes/membres.php");
require_once ("classes/documents.php");
require_once ("classes/prets.php");
require_once ("classes/reservations.php");

$conn = new mysqli(DB_HOST,DB_USER, DB_PASSWORD, DB_DATABASE);
if ($conn->connect_error) die($conn->connect_error);
if (!$conn->set_charset("utf8")) {
    printf("Erreur lors du chargement du jeu de caractères utf8 : %s\n", $conn->error);
    exit();
} else {
    $conn->character_set_name();
}
session_start();

?>

<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <!-- development version, includes helpful console warnings -->
    <script src="https://cdn.jsdelivr.net/npm/vue@2/dist/vue.js"></script>
    <!-- production version, optimized for size and speed -->
<!--    <script src="https://cdn.jsdelivr.net/npm/vue@2"></script>-->

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.6.0.js" integrity="sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk=" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="<?php echo siteURL(); ?>css/style.css">
    <link rel="shortcut icon" href="<?php echo siteURL(); ?>images/bootstrap-solid.svg" />
    <title><?php echo $title; ?></title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.13.0/jquery-ui.js"></script>

</head>
<body>
<div class="container" style="min-width: 100%;">
    <!-- Image and text -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <a class="navbar-brand" href="<?php echo siteURL(); ?>index.php">
            <img src="<?php echo siteURL(); ?>images/bootstrap-solid.svg" width="30" height="30" class="d-inline-block align-top" alt="">
            Bibliothèque Port-Cartier
        </a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
            <div class="navbar-nav">
                <a class="nav-item nav-link" href="<?php echo siteURL(); ?>index.php">Accueil</a>
                <a class="nav-item nav-link" href="<?php echo siteURL(); ?>collection.php">Collection de livres</a>
            </div>
            <div class="navbar-nav ml-auto">
                <?php
                if(!estConnecter()){
                    echo '<a class="nav-item nav-link" href="'.siteURL().'session/connexion.php">Connexion</a>';
                } else {
                    $Membre = unserialize($_SESSION['Membre']);

                    if($Membre->Get_Niveau_Permissions() == USER_DEFAULT){
                        echo '<a class="nav-item nav-link" href="'.siteURL().'session/info_membre.php">Page membre</a>';
                    } elseif($Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE){
                        echo '<a class="nav-item nav-link" href="'.siteURL().'employe.php">Page employé</a>';
                    }

                    echo '<a class="nav-item nav-link" href="'.siteURL().'session/deconnexion.php">Déconnexion</a>';
                }
                ?>
            </div>
        </div>
    </nav>
