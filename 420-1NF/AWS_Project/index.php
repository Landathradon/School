<?php
define ("DB_USER", "admin");
define ("DB_PASSWORD", "I8ixpicahr9t0zWg");
define ("DB_HOST", "school.cpfd5rvnoecv.us-east-2.rds.amazonaws.com");
define ("DB_DATABASE", "school");

$bdd = new PDO('mysql:host='.DB_HOST.';dbname='.DB_DATABASE, DB_USER, DB_PASSWORD, array(PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));

echo "<h3>Voici la liste des instructeurs</h3><ul>";

foreach($bdd->query('SELECT * FROM `instructeurs`') as $instructeur) {
    echo "<li>" . $instructeur[1] . " " . $instructeur[2] . "</li>";
}

echo "</ul>";