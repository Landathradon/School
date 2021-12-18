<?php $title = 'Info membre - Bibliothèque Port-Cartier';
include("../layout_top.php");

$Membre = "";
if(estConnecter()){
    $Membre = unserialize($_SESSION['Membre']);
//    if($Membre->Get_Niveau_Permissions() < USER_EMPLOYEE){
//        header("Refresh: 1;url=".siteURL()."index.php");
//    }
} else {
    header("Refresh: 1;url=".siteURL()."index.php");
}
?>

    <!-- Search -->
    <div class="container">
        <div class="col-md-5"></div>
        <div class="col-md-12 login" style="margin-top: 50px;">
            <?php
            if($Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE){
                $codeMembre = filter_var($_GET['code'], FILTER_SANITIZE_NUMBER_INT);
            } else {
                $codeMembre = $Membre->Get_Code_membre();
            }

            $MembreInfo = membres::Get_User_From_Code($codeMembre);
            $ListeReservations = reservations::Get_AllReservation($MembreInfo->Get_Code_membre());
            $ListePrets = prets::Get_AllPret($MembreInfo->Get_Code_membre());
            ?>
            <div>
                <h3><?php echo $MembreInfo->Get_NomComplet() ?></h3>
                <hr>
                <section class="row">
                    <div class="col-md-6">
                        <h5>Liste des réservations</h5>
                        <ul class="list-group col-md-12"><?php
                            if($ListeReservations != null){
                                foreach ($ListeReservations as $Reservation) {
                                    $Document = documents::Get_Document_By_Code($Reservation->Get_Code_Document());
                                    $Pret = prets::Get_Pret($Reservation->Get_Code_Document());

                                    echo '<li class="list-group-item list-group-item-action list-group-item-info"><p>No réservation: ' . $Reservation->Get_NoReservation() . ' | '.
                                            $Document->Get_Titre() .'<br>En date du: '.$Reservation->Get_DateReservation().'</p>';
                                    if($Pret == null && $Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE) {
                                        echo '<a class="btn btn-warning" href="validation.php?AnnulerReservation='.$Reservation->Get_NoReservation().'">Annuler cette réservation</a>
                                              <a class="btn btn-success" href="../collection.php?Livre='.$Document->Get_Titre().'" style="margin-left: 5px;">Prêter ce livre</a>';
                                    }
                                    echo '</li>';
                                }
                            }
                        ?></ul>
                    </div>
                    <div class="col-md-6" style="border-left: 1px solid white;">
                        <h5>Liste des erupts</h5>
                        <ul class="list-group col-md-12">
                            <?php
                            foreach ($ListePrets as $Pret) {
                                $Document = documents::Get_Document_By_Code($Pret->Get_Code_Document());
                                echo '<li class="list-group-item list-group-item-action list-group-item-info"><p>No prêt: ' . $Pret->Get_NoPret() . ' | '.
                                        $Document->Get_Titre() .'<br>En vigueur du '.$Pret->Get_DatePret().' au '.$Pret->Get_DateRetour().'</p>';
                                if($Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE){
                                    echo '<a class="btn btn-warning" href="validation.php?RetourPret='.$Pret->Get_NoPret().'">Retourné ou annulé ce livre</a>';
                                }
                                echo '</li>';
                            }
                            ?>
                        </ul>
                    </div>
                </section>
                <?php
                if($Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE){
                    echo '<a class="btn btn-danger" href="'. siteURL() .'session/action_employe.php?action=Membres">Retour</a>';
                }
                ?>
            </div>

        </div>

    </div>

<?php include("../layout_bottom.php"); ?>