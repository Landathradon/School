<?php $title = 'Bibliothèque Port-Cartier';
include("../layout_top.php"); ?>

<?php

if(strpos($_SERVER['HTTP_REFERER'],"connexion.php") !== false){
    $result = membres::Connexion($_POST['courriel'], $_POST['password']);

    if($result){
        header("Refresh: 0;url=".siteURL());
    } else {
        echo "<div class='col-md-4 login'>Ce compte n'existe pas. Veuillez réessayer.</div>";
        header("Refresh: 5;url=".siteURL()."session/connexion.php");
    }
}
elseif(strpos($_SERVER['HTTP_REFERER'],"ajout_compte.php") !== false){
    if($_POST['password'] !== $_POST['passwordConf']){
        echo "<div class='col-md-4 login'>Vos mot de passe ne sont pas identiques.</div>";
        header("Refresh: 5;url=".siteURL()."session/ajout_compte.php");
    }

    $result = membres::Ajout_Compte($_POST);

    if($result){
        header("Refresh: 0;url=".siteURL());
    } else {
        echo "<div class='col-md-4 login'>Un problème est arrivé. Veuillez réessayer.</div>";
        header("Refresh: 3;url=".siteURL()."session/connexion.php");
    }
}
elseif(strpos($_SERVER['HTTP_REFERER'],"ajout_livres.php") !== false){
    $result = documents::Ajout_Document($_POST);

    if(!$result){
        echo "<div class='col-md-4 login'>Un problème est arrivé avec le livre. Veuillez réessayer.</div>";
    } else {
        echo "<div class='col-md-4 login'>Ce livre à bien été ajouté</div>";
    }

    header("Refresh: 3;url=".siteURL()."session/ajout_livres.php");
}
elseif(strpos($_SERVER['HTTP_REFERER'],"collection.php") !== false) {
    $Membre = unserialize($_SESSION['Membre']);
    $MembreRechercher = membres::Get_User($_POST['membreRechercher']);

    if (($_POST['code_action'] == "pret" || $_POST['code_action'] == "reservation") && $Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE){
        $_POST['code_membre'] = $MembreRechercher->Get_Code_membre();
    } else {
        $_POST['code_membre'] = $Membre->Get_Code_membre();
    }

    if($_POST['code_action'] == "pret"){
        $result = prets::Ajout_Pret($_POST);
        $Reservation = reservations::Get_Reservation($_POST['code_livre']);
        if($Reservation != null){
            reservations::Annuler_Reservation($Reservation->Get_NoReservation());
        }
        if(!$result){
            echo "<div class='col-md-4 login'>Un problème est arrivé avec le livre. Veuillez réessayer.</div>";
        } else {
            echo "<div class='col-md-4 login'>Un prêt pour ce livre à été ajouter.</div>";
        }
    } elseif($_POST['code_action'] == "reservation"){
        $result = reservations::Ajout_Reservation($_POST);
        if(!$result){
            echo "<div class='col-md-4 login'>Un problème est arrivé avec le livre. Veuillez réessayer.</div>";
        } else {
            echo "<div class='col-md-4 login'>Une réservation pour ce livre à été ajouter.</div>";
        }
    }

    header("Refresh: 3;url=".siteURL()."collection.php");
}
elseif(isset($_GET['RetourPret'])) {
    $NoPret = $_GET['RetourPret'];
    $result = prets::Retour_Pret($NoPret);

    if($result != null){
        $Membre = membres::Get_User_From_Code($result->Get_Code_membre());
        $Document = documents::Get_Document_By_Code($result->Get_Code_Document());
        echo "<div class='col-md-5 login'>Il y a présentement une réservation pour le livre: <strong>".$Document->Get_Titre()."</strong>, au nom de: ". $Membre->Get_NomComplet() ."<br><br>
                <a class='btn btn-primary' href='action_employe.php?action=DocumentsReserver'>Accédé à la page des réservations</a></div>";
    } else {
        echo "<div class='col-md-4 login'>Ce prêt à été retourné/annulé et ce livre peux maintenant être emprunté à nouveaux.</div>";
        header("Refresh: 3;url=".siteURL()."employe.php");
    }


}
elseif(isset($_GET['AnnulerReservation'])) {
    $NoReservation = $_GET['AnnulerReservation'];
    $result = reservations::Annuler_Reservation($NoReservation);

    if(!$result){
        echo "<div class='col-md-4 login'>Un problème est arrivé avec cette annulation de réservation. Veuillez réessayer.</div>";
    } else {
        echo "<div class='col-md-4 login'>Cette réservation à été annulé et ce livre peux maintenant être réservé à nouveaux.</div>";
    }

    header("Refresh: 3;url=".siteURL()."employe.php");
} else {
    header("Refresh: 0;url=".siteURL()."index.php");
}
?>

<?php include("../layout_bottom.php"); ?>