<?php $title = 'Bibliothèque Port-Cartier';
include("layout_top.php");

$Membre = "";
if(estConnecter()){
    $Membre = unserialize($_SESSION['Membre']);
}

?>

    <!-- Search -->
    <div id="VueApp" class="row" style="margin-top: 50px;">
        <div class="col-md-12" id="search-image">

            <div class="col-md-6" id="search-bar">
                <input class="form-control mr-sm-2 text-center" type="search" placeholder="Vous cherchez un livre ?" aria-label="Search" v-model="searchQuery">
            </div>
        </div>

        <div class="col-md-1" ></div>
        <div class="card-deck col-md-10" style="margin-top: 50px;margin-bottom:200px;">
            <div v-for="(Document, index) in filteredList()">
                <div class="card text-light border-light bg-dark mb-3" id="book-card" style="width: 350px;height: 350px;">
                    <div class="card-header">{{ Document.Auteur }}, {{ Document.Annee }}</div>
                    <div class="card-body">
                        <h5 class="card-title">{{ Document.Titre }}</h5>
                        <hr style="border-top: 1px solid white;">
                        <p class="card-text">Catégorie: {{ Document.Categorie }}</p>
                        <p class="card-text">Type: {{ Document.Type }}</p>
                        <p class="card-text">Genre: {{ Document.Genre }}</p>
                        <div v-if="selectedBook >= 0">
                            <p id="docDateDebut" hidden>{{ Document.DateDebut }}</p>
                            <p id="docDateFin" hidden>{{ Document.DateFin }}</p>
                            <p class="card-text">Description: {{ Document.Description }}</p>
                            <p class="card-text">ISBN: {{ Document.ISBN }}</p>
                            <p class="card-text text-danger" v-if="!Document.Dispo_pret"><?php
                                if (!empty($Membre) && $Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE) {
                                    echo 'Ce document est emprunté par <span id="Nom_membre_reserver">{{ Document.Nom_membre_reserver }}</span>.';
                                } else {
                                    echo 'Ce document n\'est pas disponible.';
                                }
                                ?></p>
                            <p class="card-text text-warning" v-else-if="!Document.Dispo_rsvp && Document.Dispo_pret">Ce document est réserver <?php
                                if (!empty($Membre) && $Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE) {
                                    echo 'par <span id="Nom_membre_reserver">{{ Document.Nom_membre_reserver }}</span>.';
                                }
                            ?></p>
                            <p class="card-text text-success" v-else>Ce document est disponible.<span id="Nom_membre_reserver"></span></p>
                        </div>
                    </div>
                    <div class="card-footer" v-if="selectedBook >= 0">
                        <a class="btn btn-info" v-on:click="loadList()">Retour</a>
                        <?php
                        if (!empty($Membre) && $Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE) {
                            echo '<button class="btn btn-warning" style="float:right;margin-left:10px;" v-if="isLoggedOn && Document.Dispo_pret" data-toggle="modal" data-target="#pretModal" v-bind:data-code_livre="Document.Code">Emprunté ce livre</button>';
                        }
                        ?>

                        <span v-if="!Document.Dispo_pret || (Document.Dispo_rsvp && Document.Dispo_pret)">
                            <button class="btn btn-success" style="float:right;" v-if="isLoggedOn" data-toggle="modal" data-target="#reserverModal" v-bind:data-code_livre="Document.Code">Réserver ce livre</button>
                            <a class="btn btn-success disabled" style="float:right;" v-else>Connectez-vous pour réserver</a>
                        </span>
                    </div>
                    <div class="card-footer" v-else>
                        <a class="btn btn-info" v-on:click="showBook(Document.Code)">Infos sur le livre</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="reserverModal" tabindex="-1" role="dialog" aria-labelledby="reserverModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="reserverModalLabel">Réservation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <form method="post" action="session/validation.php">
                    <div class="modal-body">
                        <?php
                        if(!empty($Membre) && $Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE) {
                            $ListeMembres = $Membre->Get_AllUser();
                            echo '<div class="form-group">
                                <label for="membreRechercher" class="col-form-label col-md-3">Membre</label>
                                <select class="col-md-6" id="membreRechercher" name="membreRechercher">';

                            foreach ($ListeMembres as $Membre) {
                                echo '<option value="' . $Membre->Get_Courriel() . '">' . $Membre->Get_NomComplet() . '</option>';
                            }

                            echo '        
                                </select>
                            </div>';
                        }
                        ?>

                        <div class="form-group">
                            <label for="dateDebut" class="col-form-label col-md-3">Date début:</label>
                            <input type="text" class="datepicker col-md-6" name="dateDebut" id="dateDebut">
                        </div>
                        <input type="hidden" id="code_livre" name="code_livre">
                        <input type="hidden" name="code_action" value="reservation">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fermer</button>
                        <input type="submit" class="btn btn-primary" value="Confirmer">
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="modal fade" id="pretModal" tabindex="-1" role="dialog" aria-labelledby="pretModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pretModalLabel">Prêt</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <form method="post" action="session/validation.php">
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="membreRechercher" class="col-form-label col-md-3">Membre</label>
                            <select class="col-md-6" id="membreRechercher" name="membreRechercher">
                                <?php
                                if(!empty($Membre) && $Membre->Get_Niveau_Permissions() >= USER_EMPLOYEE){
                                    $ListeMembres = $Membre->Get_AllUser();

                                    foreach ($ListeMembres as $Membre){
                                        echo '<option value="'. $Membre->Get_Courriel() .'">'. $Membre->Get_NomComplet() .'</option>';
                                    }
                                }

                                ?>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="datePret" class="col-form-label col-md-3">Date début:</label>
                            <input type="text" class="datepicker col-md-6" name="datePret" id="datePret">
                        </div>
                        <div class="form-group">
                            <label for="dateFin" class="col-form-label col-md-3">Date fin:</label>
                            <input type="text" class="datepicker col-md-6" name="dateFin" id="dateFin">
                        </div>
                        <input type="hidden" id="code_livre" name="code_livre">
                        <input type="hidden" name="code_action" value="pret">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fermer</button>
                        <input type="submit" class="btn btn-primary" value="Confirmer">
                    </div>
                </form>
            </div>
        </div>
    </div>

    <?php echo '<script type="application/javascript">let arrayData = '.json_encode(documents::Get_All_Documents()).'</script>'; ?>
<!--    --><?php //echo '<script type="application/javascript">let arrayData = '. (estConnecter() ? json_encode(documents::Get_All_Documents($Membre->Get_Code_membre())):json_encode(documents::Get_All_Documents())) .'</script>'; ?>
    <?php echo '<script type="application/javascript">let estConnecter = '. (estConnecter() ? 1:0) .'</script>'; ?>

<?php include("layout_bottom.php"); ?>