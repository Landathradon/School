<?php $title = 'Bibliothèque Port-Cartier';
include("../layout_top.php"); ?>

    <section class="container">
        <div class="col-md-5"></div>
        <div class="col-md-12 login">
            <form method="post" action="validation.php">
                <div class="form-group row">
                    <label for="nom" class="col-md-2 col-form-label">Nom</label>
                    <input type="text" class="form-control col-md-4" id="nom" name="nom" placeholder="Entrez votre nom" required>
                </div>
                <div class="form-group row">
                    <label for="prenom" class="col-md-2 col-form-label">Prénom</label>
                    <input type="text" class="form-control col-md-4" id="prenom" name="prenom" placeholder="Entrez votre prenom" required>
                </div>
                <div class="form-group row">
                    <label for="telephone" class="col-md-2 col-form-label">Téléphone</label>
                    <input type="tel" class="form-control col-md-4" id="telephone" name="telephone" placeholder="Entrez votre # de téléphone" aria-describedby="aideTelephone" pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}" required>
                    <small id="aideTelephone" class="form-text" style="margin-left: 10px;">Format: 123-456-7890</small>
                </div>
                <div class="form-group row">
                    <label for="courriel" class="col-md-2 col-form-label">Adresse courriel</label>
                    <input type="email" class="form-control col-md-4" id="courriel" name="courriel" placeholder="Entrez votre courriel" required>
                </div>
                <hr>
                <div class="form-group row">
                    <label for="password" class="col-md-2 col-form-label">Mot de passe</label>
                    <input type="password" class="form-control col-md-4" id="password" name="password" placeholder="Entrez votre mot de passe" required>
                </div>
                <div class="form-group row">
                    <label for="passwordConf" class="col-md-2 col-form-label">Confirmer</label>
                    <input type="password" class="form-control col-md-4" id="passwordConf" name="passwordConf" placeholder="Entrez votre mot de passe de nouveau" required>
                </div>
                <hr>
                <div class="form-group row">
                    <label for="adresse_numero" class="col-md-2 col-form-label">No. Civique</label>
                    <input type="number" class="form-control col-md-4" id="adresse_numero" name="adresse_numero" placeholder="#" required>

                    <label for="adresse_ville" class="col-md-2 col-form-label">Ville</label>
                    <input type="text" class="form-control col-md-3" id="adresse_ville" name="adresse_ville" placeholder="Votre ville" required>

                    <label for="adresse_rue" class="col-md-2 col-form-label">Rue</label>
                    <input type="text" class="form-control col-md-4" id="adresse_rue" name="adresse_rue" placeholder="Votre rue" required>

                    <label for="adresse_province" class="col-md-2 col-form-label">Province</label>
                    <input type="text" class="form-control col-md-3" id="adresse_province" name="adresse_province" placeholder="Votre province" required>
                </div>
                <?php
                $Membre = unserialize($_SESSION['Membre']);
                if ($Membre->Get_Niveau_Permissions() == USER_ADMIN) {
                    echo '<div class="form-group">
                            <label for="FormSelectUserLevel"></label>
                            <select class="form-control" id="FormSelectUserLevel" name="FormSelectUserLevel">
                                <option value="' . USER_DEFAULT . '">Utilisateur normal</option>
                                <option value="' . USER_EMPLOYEE . '">Employé</option>
                                <option value="' . USER_ADMIN . '">Administrateur</option>
                            </select>
                        </div>';
                }

                ?>
                <button type="submit" id="ajout_compteBtn" class="btn btn-primary">Envoyer</button>
            </form>
        </div>
    </section>

<?php include("../layout_bottom.php"); ?>