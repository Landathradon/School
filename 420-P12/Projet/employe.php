<?php $title = 'Espace employé - Bibliothèque Port-Cartier';
include("layout_top.php");

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

    <!-- Search -->
    <div class="container">
        <div class="col-md-5"></div>
        <div class="col-md-12 login" style="margin-top: 50px;">

            <ul class="list-group col-md-5">
                <li><a class="list-group-item list-group-item-action list-group-item-info" href="<?php echo siteURL(); ?>session/action_employe.php?action=Membres">Liste des membres</a></li>
                <li><a class="list-group-item list-group-item-action list-group-item-info" href="<?php echo siteURL(); ?>session/action_employe.php?action=Retards">Liste des retards</a></li>
                <li><a class="list-group-item list-group-item-action list-group-item-info" href="<?php echo siteURL(); ?>session/action_employe.php?action=Documents">Liste de tous les documents</a></li>
                <li><a class="list-group-item list-group-item-action list-group-item-info" href="<?php echo siteURL(); ?>session/action_employe.php?action=DocumentsReserver">Liste des documents réservés</a></li>
                <li><a class="list-group-item list-group-item-action list-group-item-info" href="<?php echo siteURL(); ?>session/action_employe.php?action=DocumentsPreter">Liste des documents prêtés</a></li>
                <?php
                if ($Membre->Get_Niveau_Permissions() == USER_ADMIN) {
                    echo '<li><a class="list-group-item list-group-item-action list-group-item-info" href="' . siteURL() . 'session/ajout_livres.php">Ajouter des livres</a></li>
                          <li><a class="list-group-item list-group-item-action list-group-item-info" href="' . siteURL() . 'session/ajout_compte.php">Ajouter des nouveaux comptes</a></li>';
                }
                ?>
            </ul>
        </div>

    </div>

<?php include("layout_bottom.php"); ?>