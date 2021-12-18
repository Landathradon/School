<?php $title = 'Bibliothèque Port-Cartier';
include("../layout_top.php"); ?>

    <section class="container">
        <div class="col-md-5"></div>
        <div class="col-md-12 login">
            <form method="post" action="validation.php">
                <div class="form-group row">
                    <label for="titre" class="col-md-2 col-form-label">Titre</label>
                    <input type="text" class="form-control col-md-4" id="titre" name="titre" placeholder="" required>
                </div>
                <div class="form-group row">
                    <label for="auteur" class="col-md-2 col-form-label">Auteur</label>
                    <input type="text" class="form-control col-md-4" id="auteur" name="auteur" placeholder="" required>
                </div>
                <div class="form-group row">
                    <label for="annee" class="col-md-2 col-form-label">Année</label>
                    <input type="number" class="form-control col-md-4" id="annee" name="annee" placeholder="" required>
                </div>
                <div class="form-group row">
                    <label for="FormSelectCategory" class="col-md-2 col-form-label">Catégorie</label>
                    <select class="form-control col-md-4" id="FormSelectCategory" name="FormSelectCategory">
                        <option value="Art musique et cinéma">Art musique et cinéma</option>
                        <option value="Bandes dessinées">Bandes dessinées</option>
                        <option value="Développement personnel">Développement personnel</option>
                        <option value="Dictionnaires & langues">Dictionnaires & langues</option>
                        <option value="Droit & économie">Droit & économie</option>
                        <option value="Essais et documents">Essais et documents</option>
                        <option value="Guide pratiques">Guide pratiques</option>
                        <option value="Histoire">Histoire</option>
                        <option value="Humour">Humour</option>
                        <option value="Informatique et internet">Informatique et internet</option>
                        <option value="Jeunesse">Jeunesse</option>
                        <option value="Littérature">Littérature</option>
                        <option value="Policier, suspense, thrillers">Policier, suspense, thrillers</option>
                        <option value="Religion et spiritualité">Religion et spiritualité</option>
                        <option value="Roman">Roman</option>
                        <option value="Sciences sociales">Sciences sociales</option>
                        <option value="Sciences, techniques & médecine">Sciences, techniques & médecine</option>
                        <option value="Scolaire">Scolaire</option>
                        <option value="SF, Fantasy">SF, Fantasy</option>
                        <option value="Sports loisirs et vie pratique">Sports loisirs et vie pratique</option>
                        <option value="Théâtre">Théâtre</option>
                        <option value="Tourisme et voyages">Tourisme et voyages</option>
                    </select>
                </div>
                <div class="form-group row">
                    <label for="FormSelectType" class="col-md-2 col-form-label">Type</label>
                    <select class="form-control col-md-4" id="FormSelectType" name="FormSelectType">
                        <option value="Enfant">Enfant</option>
                        <option value="Adolescent">Adolescent</option>
                        <option value="Adulte">Adulte</option>
                    </select>
                </div>
                <div class="form-group row">
                    <label for="FormSelectGenre" class="col-md-2 col-form-label">Genre</label>
                    <select class="form-control col-md-4" id="FormSelectGenre" name="FormSelectGenre">
                        <option value="Aventures">Aventures</option>
                        <option value="Comédie">Comédie</option>
                        <option value="Drame">Drame</option>
                        <option value="Horreur">Horreur</option>
                        <option value="Sci-fi">Sci-fi</option>
                        <option value="Documentaire">Documentaire</option>
                    </select>
                </div>
                <div class="form-group row">
                    <label for="description" class="col-md-2 col-form-label">Description</label>
                    <textarea class="form-control col-md-4" id="description" name="description" placeholder="" style="min-height: 100px;" required></textarea>
                </div>
                <div class="form-group row">
                    <label for="isbn" class="col-md-2 col-form-label">ISBN</label>
                    <input type="text" class="form-control col-md-4" id="isbn" name="isbn" placeholder="">
                </div>
                <button type="submit" id="ajout_compteBtn" class="btn btn-primary">Envoyer</button>
            </form>
        </div>
    </section>

<?php include("../layout_bottom.php"); ?>