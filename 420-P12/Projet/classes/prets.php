<?php

class prets
{
    private $NoPret;
    private $CodeDocument;
    private $CodeMembre;
    private $DatePret;
    private $DateRetour;

    public function __construct(array $Pret) //From Get_Document
    {
        $this->NoPret = $Pret['code_pret'];
        $this->CodeDocument = $Pret['code_document'];
        $this->CodeMembre = $Pret['code_membre'];
        $this->DatePret = $Pret['date_pret'];
        $this->DateRetour = $Pret['date_retour'];
    }

    function Get_NoPret(): string
    {
        return $this->NoPret;
    }
    function Get_Code_membre(): string
    {
        return $this->CodeMembre;
    }
    function Get_Code_Document(): string
    {
        return $this->CodeDocument;
    }
    function Get_DatePret(): string
    {
        return date("m/d/Y",strtotime($this->DatePret));
    }
    function Get_DateRetour(): string
    {
        return date("m/d/Y",strtotime($this->DateRetour));
    }

    public static function Get_Pret(int $code_document): ?prets
    {
        $db = Database::getConnection();
        $stmt = $db->prepare("SELECT * FROM prets WHERE code_document = ? AND actif = 1");
        $stmt->bind_param("i", $code_document);
        $stmt->execute();
        $result = $stmt->get_result();

        if($result->num_rows < 1){
            return null;
        } else {
            return new prets($result->fetch_array());
        }
    }
    public static function Get_Pret_By_NoPret(int $NoPret): ?prets
    {
        $db = Database::getConnection();
        $stmt = $db->prepare("SELECT * FROM prets WHERE code_pret = ?");
        $stmt->bind_param("i", $NoPret);
        $stmt->execute();
        $result = $stmt->get_result();

        if($result->num_rows < 1){
            return null;
        } else {
            return new prets($result->fetch_array());
        }
    }

    public static function Get_AllPret($code_membre = ""): array
    {

        $array = [];
        $db = Database::getConnection();
        $db->set_charset("utf8");
        if($code_membre != ""){
            $stmt = $db->prepare("SELECT * FROM prets WHERE code_membre = ? AND actif = 1");
            $stmt->bind_param("i", $code_membre);
        } else {
            $stmt = $db->prepare("SELECT * FROM prets WHERE actif = 1");
        }
        $stmt->execute();
        $result = $stmt->get_result();

        while ($pret = $result->fetch_array()) {
            $pret = new prets($pret);
            array_push($array, $pret);
        }

        return $array;
    }

    public static function Get_AllRetards(): array
    {
        $array = [];
        $db = Database::getConnection();
        $db->set_charset("utf8");
        $stmt = $db->prepare("SELECT * FROM prets WHERE DATE(NOW()) > date_retour AND actif = 1");
        $stmt->execute();
        $result = $stmt->get_result();

        while ($pret = $result->fetch_array()) {
            $pret = new prets($pret);
            array_push($array, $pret);
        }

        return $array;
    }

    public static function Ajout_Pret(array $post): bool
    {
        $CodeDocument = $post['code_livre'];
        $CodeMembre = $post['code_membre'];
        $DatePret = date('Y-m-d H:i:s',strtotime($post['datePret']));
        $DateRetour = date('Y-m-d H:i:s',strtotime($post['dateFin']));

        $db = Database::getConnection();
        $stmt = $db->prepare("INSERT INTO prets (code_document, code_membre, date_pret, date_retour)
                                    VALUES (?, ?, ?, ?)");
        $stmt->bind_param("iiss", $CodeDocument, $CodeMembre, $DatePret, $DateRetour);
        return $stmt->execute();
    }

    public static function Retour_Pret(int $NoPret): ?reservations
    {
        $Pret = prets::Get_Pret_By_NoPret($NoPret);

        $db = Database::getConnection();
        $db->set_charset("utf8");
        $stmt = $db->prepare("UPDATE prets SET actif = 0 WHERE code_pret = ?");
        $stmt->bind_param("i", $NoPret);
        $stmt->execute();

        $Reservation = reservations::Get_Reservation($Pret->Get_Code_Document());
        if($Reservation != null){
            return $Reservation;
        } else {
            return null;
        }
    }
}