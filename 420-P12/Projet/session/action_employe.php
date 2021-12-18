<?php $title = 'Espace employé - Bibliothèque Port-Cartier';
include("../layout_top.php");

$Membre = "";
if(estConnecter()){
    $Membre = unserialize($_SESSION['Membre']);
    if($Membre->Get_Niveau_Permissions() < USER_EMPLOYEE){
        header("Refresh: 1;url=".siteURL()."index.php");
    }
} else {
    header("Refresh: 1;url=".siteURL()."index.php");
}
?>

    <div class="container">
        <div class="col-md-5"></div>
        <div class="col-md-12 login" style="margin-top: 50px;">

            <?php
            echo '<div>';
            if ($_GET['action'] == "Membres") {
                $ListeMembres = $Membre->Get_AllUser();

                echo '<h4>Liste des membres</h4><ul class="list-group col-md-6">';
                foreach ($ListeMembres as $Membre) {
                    echo '<a href="info_membre.php?code='.$Membre->Get_Code_membre().'"><li class="list-group-item list-group-item-action list-group-item-info">' . $Membre->Get_NomComplet() . '</li></a>';
                }
                echo '</ul>';
            } elseif ($_GET['action'] == "Retards") {
                $ListeRetards = prets::Get_AllRetards();

                echo '<h4>Liste des retards</h4><ul class="list-group col-md-6">';
                foreach ($ListeRetards as $Retard) {
                    $Membre = membres::Get_User_From_Code($Retard->Get_Code_membre());
                    $Document = documents::Get_Document_By_Code($Retard->Get_Code_Document());
                    echo '<li class="list-group-item list-group-item-action list-group-item-warning">No prêt: ' . $Retard->Get_NoPret() . ' | '. $Document->Get_Titre() .'<br>Prêter à '.
                        $Membre->Get_NomComplet() .' du '. date('Y-m-d',strtotime($Retard->Get_DatePret())) .' au '. date('Y-m-d',strtotime($Retard->Get_DateRetour())) .'</li>';
                }
                echo '</ul>';
            } elseif ($_GET['action'] == "Documents") {
                $ListeDocuments = documents::Get_All_Documents();

                echo '<h4>Liste des documents</h4><ul class="list-group col-md-6">';
                foreach ($ListeDocuments as $Document) {
                    echo '<a href="../collection.php?Livre='.$Document['Titre'].'"><li class="list-group-item list-group-item-action list-group-item-info">' . $Document['Titre'] . '</li></a>';
                }
                echo '</ul>';
            } elseif ($_GET['action'] == "DocumentsReserver") {
                $ListeReservations = reservations::Get_AllReservation();

                echo '<h4>Liste des réservations</h4><ul class="list-group col-md-6">';
                foreach ($ListeReservations as $Reservation) {
                    $Membre = membres::Get_User_From_Code($Reservation->Get_Code_membre());
                    $Document = documents::Get_Document_By_Code($Reservation->Get_Code_Document());
                    $Pret = prets::Get_Pret($Reservation->Get_Code_Document());

                    echo '<li class="list-group-item list-group-item-action list-group-item-info"><p>No réservation: ' . $Reservation->Get_NoReservation() . ' | '. $Document->Get_Titre() .'<br>Réservé par '.
                            $Membre->Get_NomComplet() .' pour le '. date('Y-m-d',strtotime($Reservation->Get_DateReservation()))
                            .'</p><a class="btn btn-warning" href="validation.php?AnnulerReservation='.$Reservation->Get_NoReservation().'">Annuler cette réservation</a>';
                    if($Pret == null) {
                        echo '<a class="btn btn-success" href="../collection.php?Livre='.$Document->Get_Titre().'" style="margin-left: 5px;">Prêter ce livre</a>';
                    }
                    echo '</li>';
                }
                echo '</ul>';
            } elseif ($_GET['action'] == "DocumentsPreter") {
                $ListePrets = prets::Get_AllPret();

                echo '<h4>Liste des prêts</h4><ul class="list-group col-md-6">';
                foreach ($ListePrets as $Pret) {
                    $Membre = membres::Get_User_From_Code($Pret->Get_Code_membre());
                    $Document = documents::Get_Document_By_Code($Pret->Get_Code_Document());
                    echo '<li class="list-group-item list-group-item-action list-group-item-info"><p>No prêt: ' . $Pret->Get_NoPret() . ' | '. $Document->Get_Titre() .'<br>Prêter à '.
                        $Membre->Get_NomComplet() .' du '. date('Y-m-d',strtotime($Pret->Get_DatePret())) .' au '. date('Y-m-d',strtotime($Pret->Get_DateRetour()))
                        .'</p><a class="btn btn-warning" href="validation.php?RetourPret='.$Pret->Get_NoPret().'">Retourné ou annulé ce livre</a></li>';
                }
                echo '</ul>';
            }


            echo '</div>';
            ?>
            <div style="margin: 10px 0;"><a class="btn btn-danger" href="../employe.php">Retour</a></div>
        </div>

    </div>

<?php include("../layout_bottom.php"); ?>