<?php $title = 'Bibliothèque Port-Cartier';
include("../layout_top.php"); ?>

<section class="container">
    <div class="col-md-4"></div>
    <div class="col-md-12 login">
        <form method="post" action="validation.php">
            <div class="form-group">
                <label for="courriel">Adresse courriel</label>
                <input type="email" class="form-control" id="courriel" name="courriel" placeholder="Entrez votre courriel" required>
            </div>
            <div class="form-group">
                <label for="password">Mot de passe</label>
                <input type="password" class="form-control" id="password" name="password" placeholder="Entrez votre mot de passe" required>
            </div>
            <button type="submit" class="btn btn-primary">Envoyer</button>
        </form>
    </div>
</section>

<?php include("../layout_bottom.php"); ?>