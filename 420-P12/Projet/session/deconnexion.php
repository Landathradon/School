<?php $title = 'Bibliothèque Port-Cartier';
include("../layout_top.php");

session_unset();
session_destroy();

header("Refresh: 3;url=".siteURL());

?>

    <section class="container">
        <div class="col-md-12 login">
            Vous êtes maintenant déconnecté
        </div>
    </section>

<?php include("../layout_bottom.php"); ?>