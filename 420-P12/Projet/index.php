<?php $title = 'Bibliothèque Port-Cartier';
include("layout_top.php"); ?>

    <!-- Search -->
    <div class="row" style="margin-top: 50px;">
        <div class="col-md-12" id="search-image">

            <div class="col-md-6" id="search-bar">
                <form method="get" action="collection.php">
                    <input class="form-control mr-sm-2 text-center" type="search" placeholder="Vous cherchez un livre ?" aria-label="Search" name="Livre">
                    <input type="submit" style="position: absolute; left: -9999px; width: 1px; height: 1px;" tabindex="-1" />
                </form>
            </div>

        </div>
        <div class="col-md-12 row">
            <div class="col-md-3"></div>
            <div class="col-md-6 login">
                <h3>Qu'est-ce que le Réseau BIBLIO du Québec?</h3>
                <hr>
                <h5>MISSION</h5>
                <p>Le Réseau BIBLIO du Québec est un regroupement national qui vise à unir les ressources des Réseaux BIBLIO
                    régionaux pour maintenir et développer leur réseau de bibliothèques et de les représenter auprès des
                    diverses instances sur des dossiers d’intérêts communs.</p>

                <h5>VISION</h5>
                <p>Être la référence et le partenaire privilégié des associations et organismes nationaux pour le développement
                    et la consolidation d’un réseau québécois de bibliothèques de qualité.</p>

                <h5>VALEURS</h5>
                <p>Le Réseau BIBLIO du Québec mise sur trois valeurs primordiales qui sont le respect, l’intégrité, ainsi que la solidarité.</p>
            </div>

        </div>
    </div>

<?php include("layout_bottom.php"); ?>