<?php

class documents
{
    private $Code;
    private $Titre;
    private $Auteur;
    private $Annee;
    private $Categorie;
    private $Type;
    private $Genre;
    private $Description;
    private $ISBN;

    public function __construct(array $Document) //From Get_Document
    {
        $this->Code = $Document['code'];
        $this->Titre = $Document['titre'];
        $this->Auteur = $Document['auteur'];
        $this->Annee = $Document['annee'];
        $this->Categorie = $Document['categorie'];
        $this->Type = $Document['type'];
        $this->Genre = $Document['genre'];
        $this->Description = $Document['description'];
        $this->ISBN = $Document['isbn'];
    }

    function Get_Titre(): string
    {
        return $this->Titre;
    }

    public static function Get_Document(string $titre): ?documents
    {
        $titre = filter_var($titre, FILTER_SANITIZE_STRING);

        $db = Database::getConnection();
        $stmt = $db->prepare("SELECT * FROM documents WHERE titre = ?");
        $stmt->bind_param("s", $titre);
        $stmt->execute();
        $result = $stmt->get_result();

        if($result->num_rows < 1){
            return null;
        } else {
            return new documents($result->fetch_array());
        }

    }

    public static function Get_Document_By_Code(int $Code): ?documents
    {

        $db = Database::getConnection();
        $stmt = $db->prepare("SELECT * FROM documents WHERE code = ?");
        $stmt->bind_param("i", $Code);
        $stmt->execute();
        $result = $stmt->get_result();

        if($result->num_rows < 1){
            return null;
        } else {
            return new documents($result->fetch_array());
        }

    }

    public static function Get_All_Documents(): array
    {
        $array = [];
        $db = Database::getConnection();
        $db->set_charset("utf8");
        $stmt = $db->prepare("SELECT * FROM documents");
        $stmt->execute();
        $result = $stmt->get_result();

        while($Document = $result->fetch_array()){
            $disponibilite_rsvp = true;
            $disponibilite_pret = true;
            $Nom_membre_reserver = "";
            $dateDebut = "";
            $dateFin = "";

            $Document = new documents($Document);
            $Reservation = reservations::Get_Reservation($Document->Code);
            $Pret = prets::Get_Pret($Document->Code);

            if(!is_null($Pret)){
                $Nom_membre_reserver = membres::Get_User_From_Code($Pret->Get_Code_membre())->Get_NomComplet();
                $disponibilite_rsvp = false;
                $disponibilite_pret = false;
                $dateDebut = $Pret->Get_DatePret();
                $dateFin = $Pret->Get_DateRetour();
            } else if(!is_null($Reservation)){
                $Nom_membre_reserver = membres::Get_User_From_Code($Reservation->Get_Code_membre())->Get_NomComplet();
                $disponibilite_rsvp = false;
                $dateDebut = $Reservation->Get_DateReservation();
            }

            $obj = [
                'Code' => $Document->Code,
                'Titre' => $Document->Titre,
                'Auteur' => $Document->Auteur,
                'Annee' => $Document->Annee,
                'Categorie' => $Document->Categorie,
                'Type' => $Document->Type,
                'Genre' => $Document->Genre,
                'Description' => $Document->Description,
                'ISBN' => $Document->ISBN,
                'Dispo_rsvp' => $disponibilite_rsvp,
                'Dispo_pret' => $disponibilite_pret,
                'DateDebut' => $dateDebut,
                'DateFin' => $dateFin,
                'Nom_membre_reserver' => $Nom_membre_reserver
            ];
            array_push($array, $obj);
        }

        return $array;
    }

    public static function Ajout_Document(array $post): bool
    {
        $titre = filter_var($post['titre'], FILTER_SANITIZE_STRING);
        $auteur = filter_var($post['auteur'], FILTER_SANITIZE_STRING);
        $annee = $post['annee'];
        $categorie = filter_var($post['FormSelectCategory'], FILTER_SANITIZE_STRING);
        $type = filter_var($post['FormSelectType'], FILTER_SANITIZE_STRING);
        $genre = filter_var($post['FormSelectGenre'], FILTER_SANITIZE_STRING);
        $description = filter_var($post['description'], FILTER_SANITIZE_STRING);
        $isbn = $post['isbn'];

        $db = Database::getConnection();
        $stmt = $db->prepare("INSERT INTO documents (titre, auteur, annee, categorie, type, genre, description, isbn)
                                    VALUES (?, ?, ?, ?, ?, ?, ?, ?)");
        $stmt->bind_param("ssisssss", $titre, $auteur, $annee, $categorie, $type, $genre, $description, $isbn);
        return $stmt->execute();
    }
}