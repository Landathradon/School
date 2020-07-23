<?php
session_start();

$username = "Jean";
$password = "arc5fg8l";

if (isset($_POST['logout'])) {
    session_destroy();
    header('Location: '.$_SERVER['PHP_SELF']);
    die;
}

if(isset($_POST["username"]) && isset($_POST["password"])){
    if($_POST["username"] == $username && $_POST["password"] == $password){
        $_SESSION['name'] = $username;
    } else {
        echo "Mauvaise combinaison, veuillez réessayer.";
    }
}

if(!isset($_SESSION['name'])){
    echo '<form method="post">
        <label for="username">Entrez votre nom:</label>
        <input type="text" name="username" id="username" />
        <label for="password">Entrez votre mot de passe:</label>
        <input type="password" name="password" id="password" />
        <input type="submit" class="" name="" id="" value="Confirmer" />
    </form>';
} else {
    if (isset($_POST['WriteFile']) && !is_null($_POST['WriteFile'])) {
        writeFile();
    }

    if (isset($_FILES['UploadFileField'])) {
        $UploadName = $_FILES['UploadFileField']['name'];
        $UploadName = mt_rand(100000, 999999) . $UploadName;
        $UploadTmp = $_FILES['UploadFileField']['tmp_name'];
        $UploadType = $_FILES['UploadFileField']['type'];
        $FileSize = $_FILES['UploadFileField']['size'];

        $UploadName = preg_replace("#[^a-z0-9.]#i", "", $UploadName);

        if (($FileSize > 500000)) {
            die("Erreure - Fichier trop grand");
        }
// Checks a File has been Selected and Uploads them into a Directory on your Server
        if (!$UploadTmp) {
        } else {
            move_uploaded_file($UploadTmp, "uploads/$UploadName");
            unset($UploadTmp);
            unset($_FILES['UploadFileField']['tmp_name']);
            echo "Image enregistrer ici -> <a href='uploads/$UploadName'>Lien</a>";
        }
    }
}

function writeFile()
{
    $cours = array(
        'id' => array(
            'ARP',
            'AWB',
            'BD1',
            'BD2',
            'DCS',
            'DGP',
            'DM1',
            'DM2',
            'DW1',
            'DW2',
            'INF',
            'NTE',
            'P11',
            'P12',
            'PFE',
            'PO1',
            'PO2',
            'PPA',
            'PWB',
            'TDD'
        ),
        'desc' => array(
            'APPROCHE STRUCTURÉE À LA RÉSOLUTION DE PROBLÈMES',
            'ANIMATION WEB',
            'BASE DE DONNÉES 1',
            'BASE DE DONNÉES 2',
            'DÉVELOPPEMENT WEB CÔTÉ SERVEUR',
            'DÉVELOPPEMENT ET GESTION DE PROJETS',
            'DÉVELOPPEMENT APPLICATIONS MOBILES 1',
            'DÉVELOPPEMENT APPLICATIONS MOBILES 2',
            'DÉVELOPPEMENT WEB 1',
            'DÉVELOPPEMENT WEB 2',
            'INFONUAGIQUE',
            'NOUVELLES TECHNOLOGIES',
            'PROJET D’INTÉGRATION 1 – PROGRAMMATION ORIENTÉ OBJET',
            'PROJET D’INTÉGRATION 2 – PROGRAMMATION WEB',
            'PROJET DE FIN D’ÉTUDES – INTÉGRATION',
            'PROGRAMMATION ORIENTÉE OBJET 1',
            'PROGRAMMATION ORIENTÉE OBJET 2',
            'PROFESSION DE PROGRAMMEUR-ANALYSTE',
            'PROGRAMMATION WEB',
            'TRAITEMENT DE DONNÉES'
        )
    );
    $fp = fopen("cours.txt", "wb");
    foreach ($cours['id'] as $key => $value) {
        fwrite($fp, $key . " - " . $value . " - " . $cours['desc'][$key] . "\n");
    }
    echo "Le ficher 'cours.txt' à bien été crée. Cliquer ici pour le voir -> <a href='cours.txt'>Lien</a>";
    unset($_POST['WriteFile']);
}
?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Examen Intra</title>
</head>

<body>
<?php
if(isset($_SESSION['name'])) {
    echo "<br>Bienvenue ".$_SESSION['name'];
    echo '<form method="post">
        <button type="submit" name="logout" value="logout">Déconnexion</button>
    </form>';
    echo '<div>
    <h3>Voulez vous ajouter un fichier?</h3>
    <form method="post">
        <button type="submit" name="WriteFile" value="WriteFile">Crée liste de cours</button>
    </form>
</div><div>
    <h3>Téléchargez votre image sur notre site</h3>
    <form method="post" enctype="multipart/form-data" name="FileUploadForm" id="FileUploadForm">
        <label for="UploadFileField"></label>
        <input type="file" name="UploadFileField" id="UploadFileField" />
        <input type="submit" class="" name="UploadButton" id="UploadButton" value="Enregistrer" />
    </form>
</div>';
}
?>
</body>

<footer>

</footer>
</html>