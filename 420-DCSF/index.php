<?php
require ("config.php");
include ("functions.php");

$conn = new mysqli(DB_HOST,DB_USER, DB_PASSWORD, DB_DATABASE);
if ($conn->connect_error) die($conn->connect_error);
if (!$conn->set_charset("utf8")) {
    printf("Erreur lors du chargement du jeu de caractères utf8 : %s\n", $conn->error);
    exit();
} else {
    $conn->character_set_name();
}

$queryEval = $conn->query("SELECT * FROM `evaluations`");
$rowsEval = $queryEval->fetch_all();

$queryCour = $conn->query("SELECT * FROM `cours`");
$rowsCour = $queryCour->fetch_all();

$queryResult = $conn->query("SELECT * FROM `resultats`");
$moyenne = "0%";
if($queryResult->num_rows > 0) {
    $rowsResult = $queryResult->fetch_all();
    $moyenne = calculMoyenneTotal($rowsResult);
}

if(isset($_POST) && !empty($_POST)){
    foreach ($_POST as $key => $value){
        if (strpos($key, 'desc_') !== false) {
            $key = explode("_", $key)[1];
            addToDatabase($conn, array($key, $value), $rowsEval, "desc");
        } else {
            addToDatabase($conn, array($key, $value), $rowsEval, "eval");
        }
    }

    unset($_POST);
    header('Location: '.$_SERVER['PHP_SELF']);
    die;
}

?>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js" integrity="sha384-OgVRvuATP1z7JjHLkuOU7Xw704+h835Lr+6QL9UvYjZE3Ipu6Tp75j7Bh/kR0JKI" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="style.css">
</head>
<body>
<div class="container">
    <div class="row">
        <div class="col-md-12">
            <h3>Notes globales</h3>
            <p># Étudiant: 633222150</p>
            <p>Prénom & Nom: Jean-Maxime Côté</p>
            <p>Programme: Programmeur-Analyste orienté internet - LEA.9C</p>
            <p>Moyenne: <span id="moyenne"><?php echo $moyenne ?></span></p>
            <form class="form" method="get">
                <input class="btn btn-info" type="submit" name="content" value="Notes">
                <input class="btn btn-info" type="submit" name="content" value="Temps">
            </form>
        </div>
    </div>
    <hr>
<?php
if(isset($_GET["content"]) && $_GET["content"] == "Temps"){
    echo '<div class="row">'.showTotalHours($rowsCour, $rowsEval).'</div>';
} else {
    echo '<div class="row"><div class="col-md-5"><form class="form" method="post"><button type="button" id="modifDesc" class="btn btn-warning">Modif. Desc.</button><input type="submit" class="btn btn-success" class="form-control" value="Enregistrer">
    <table class="table table-dark"><thead><tr><th scope="col">ID</th><th scope="col">Description</th><th scope="col">Note</th></tr></thead><tbody>';
    if ($queryResult->num_rows > 0) {
        foreach ($rowsResult as $value) {
            if ($value[0] == null) {
                continue;
            }
            $note = $value[1];
            echo "<tr><th scope='row'>$value[0]</th><td class='desc'>" . findDescription($rowsCour, $value[0]) . "</td><td class='note note-cour'>$note</td></tr>";
        }
    } else {
        foreach ($rowsCour as $value) {
            $note = getNote($rowsEval, $value[0]);
            updateResults($conn, array($value[0], $note));
            echo "<tr><th scope='row'>$value[0]</th><td class='desc'>$value[1]</td><td class='note note-cour'>$note</td></tr>";
        }
    }
    echo '</form></tbody></table></div>';

    echo '<div class="col-md-7"><table class="table table-dark"><thead><tr><th scope="col">Code</th><th scope="col">Description</th><th scope="col">Évaluation</th><th scope="col">Note</th></tr></thead><tbody>
    <button class="btn btn-primary" id="showAll">Afficher Tout</button><button class="btn btn-primary" id="showDone">Afficher Complété</button><form class="form" method="post"><input type="submit" class="btn btn-success" class="form-control" value="Enregistrer">';
    foreach ($rowsEval as $value) {
        echo "<tr><th scope='row'>$value[1]</th><td>" . findDescription($rowsCour, $value[1]) . "</td><td>$value[3]</td><td><input type='text' name='$value[0]' value='$value[4]' class='form-control note'></td></tr>";
    }
    echo '</form></tbody></table></div></div>';
}
$conn->close();
?>
</div>
<script type="text/javascript" src="script.js"></script>
</body>
</html>
