<?php

class reservations
{
    private $NoReservation;
    private $CodeDocument;
    private $CodeMembre;
    private $DateReservation;

    public function __construct(array $Reservation) //From Get_Document
    {
        $this->NoReservation = $Reservation['code_reservation'];
        $this->CodeDocument = $Reservation['code_document'];
        $this->CodeMembre = $Reservation['code_membre'];
        $this->DateReservation = $Reservation['date_reservation'];
    }

    function Get_NoReservation(): string
    {
        return $this->NoReservation;
    }
    function Get_Code_membre(): string
    {
        return $this->CodeMembre;
    }
    function Get_Code_Document(): string
    {
        return $this->CodeDocument;
    }
    function Get_DateReservation(): string
    {
        return date("m/d/Y",strtotime($this->DateReservation));
    }

    public static function Get_Reservation(int $code_document): ?reservations
    {
        $db = Database::getConnection();
        $stmt = $db->prepare("SELECT * FROM reservations WHERE code_document = ? AND actif = 1");
        $stmt->bind_param("i", $code_document);
        $stmt->execute();
        $result = $stmt->get_result();

        if($result->num_rows < 1){
            return null;
        } else {
            return new reservations($result->fetch_array());
        }

    }

    public static function Get_AllReservation($code_membre = ""): array
    {

        $array = [];
        $db = Database::getConnection();
        $db->set_charset("utf8");
        if($code_membre != ""){
            $stmt = $db->prepare("SELECT * FROM reservations WHERE code_membre = ? AND actif = 1");
            $stmt->bind_param("i", $code_membre);
        } else {
            $stmt = $db->prepare("SELECT * FROM reservations WHERE actif = 1");
        }
        $stmt->execute();
        $result = $stmt->get_result();

        while ($reservation = $result->fetch_array()) {
            $reservation = new reservations($reservation);
            array_push($array, $reservation);
        }

        return $array;
    }

    public static function Ajout_Reservation(array $post): bool
    {
        $CodeDocument = $post['code_livre'];
        $CodeMembre = $post['code_membre'];
        $DateReservation = date('Y-m-d H:i:s',strtotime($post['dateDebut']));

        $db = Database::getConnection();
        $stmt = $db->prepare("INSERT INTO reservations (code_document, code_membre, date_reservation)
                                    VALUES (?, ?, ?)");
        $stmt->bind_param("iis", $CodeDocument, $CodeMembre, $DateReservation);
        return $stmt->execute();
    }

    public static function Annuler_Reservation(int $NoReservation): bool
    {
        $db = Database::getConnection();
        $db->set_charset("utf8");
        $stmt = $db->prepare("UPDATE reservations SET actif = 0 WHERE code_reservation = ?");
        $stmt->bind_param("i", $NoReservation);
        return $stmt->execute();
    }




}