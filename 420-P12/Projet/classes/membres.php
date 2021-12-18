<?php

class membres
{
    private $CodeMembre;
    private $Nom;
    private $Prenom;
    private $Telephone;
    private $Courriel;
    private $MotdePasse;
    private $Adresse_Numero;
    private $Adresse_Rue;
    private $Adresse_Ville;
    private $Adresse_Province;
    private $Niveau_Permissions;
    private $Salt;

    public function __construct(array $Membre) //From Get_User
    {
        $this->CodeMembre = $Membre['code_membre'];
        $this->Nom = $Membre['nom'];
        $this->Prenom = $Membre['prenom'];
        $this->Telephone = $Membre['telephone'];
        $this->Courriel = $Membre['courriel'];
        $this->MotdePasse = $Membre['mot_de_passe'];
        $this->Adresse_Numero = $Membre['adresse_numero'];
        $this->Adresse_Rue = $Membre['adresse_rue'];
        $this->Adresse_Ville = $Membre['adresse_ville'];
        $this->Adresse_Province = $Membre['adresse_province'];
        $this->Niveau_Permissions = $Membre['niveau_permissions'];
        $this->Salt = $Membre['salt'];
    }

    function Get_NomComplet(): string
    {
        return $this->Prenom . " " . $this->Nom;
    }

    function Get_Code_membre(): string
    {
        return $this->CodeMembre;
    }

    function Get_Courriel(): string
    {
        return $this->Courriel;
    }

    function Get_Niveau_Permissions(){
        return $this->Niveau_Permissions;
    }

    public static function Get_User(string $Courriel): ?membres
    {
        $Courriel = filter_var($Courriel, FILTER_SANITIZE_EMAIL);

        $db = Database::getConnection();
        $stmt = $db->prepare("SELECT * FROM membres WHERE courriel = ?");
        $stmt->bind_param("s", $Courriel);
        $stmt->execute();
        $result = $stmt->get_result();

        if($result->num_rows < 1){
            return null;
        } else {
            return new membres($result->fetch_array());
        }
    }

    public static function Get_User_From_Code(int $Code_membre): ?membres
    {
        $db = Database::getConnection();
        $stmt = $db->prepare("SELECT * FROM membres WHERE code_membre = ?");
        $stmt->bind_param("s", $Code_membre);
        $stmt->execute();
        $result = $stmt->get_result();

        if($result->num_rows < 1){
            return null;
        } else {
            return new membres($result->fetch_array());
        }
    }

    function Get_AllUser(): array
    {
        if($this->Niveau_Permissions >= USER_EMPLOYEE){

            $array = [];
            $db = Database::getConnection();
            $db->set_charset("utf8");
            $stmt = $db->prepare("SELECT * FROM membres");
            $stmt->execute();
            $result = $stmt->get_result();

            while($membre = $result->fetch_array()){
                $membre = new membres($membre);
                array_push($array, $membre);
            }

            return $array;
        } else {
            return [];
        }
    }

    public static function Connexion(string $Courriel, string $Password): bool
    {
        $Courriel = filter_var($Courriel, FILTER_SANITIZE_EMAIL);
        $Membre = self::Get_User($Courriel);

        if(is_null($Membre)){
            return false;
        } else {
            $Password = pbkdf2("SHA256", $Password, $Membre->Salt, "40000", "128", false);
            if($Password !== $Membre->MotdePasse){
                return false;
            }
            $_SESSION['Membre'] = serialize($Membre);

            return true;
        }
    }

    public static function Ajout_Compte(array $post): bool
    {
        $nom = filter_var($post['nom'], FILTER_SANITIZE_STRING);
        $prenom = filter_var($post['prenom'], FILTER_SANITIZE_STRING);
        $telephone = filter_var($post['telephone'], FILTER_SANITIZE_STRING);
        $courriel = filter_var($post['courriel'], FILTER_SANITIZE_EMAIL);
        $salt = generateRandomString(128);
        $password = pbkdf2("SHA256", $post['password'], $salt, "40000", "128", false);
        $adresse_numero = $post['adresse_numero'];
        $adresse_rue = filter_var($post['adresse_rue'], FILTER_SANITIZE_STRING);
        $adresse_ville = filter_var($post['adresse_ville'], FILTER_SANITIZE_STRING);
        $adresse_province = filter_var($post['adresse_province'], FILTER_SANITIZE_STRING);
        $niveau_permissions = is_null($post['FormSelectUserLevel']) ? USER_DEFAULT : $post['FormSelectUserLevel'];

        $db = Database::getConnection();
        $stmt = $db->prepare("INSERT INTO membres (nom, prenom, telephone, courriel, mot_de_passe, adresse_numero, adresse_rue, adresse_ville, adresse_province, niveau_permissions, salt)
                                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");
        $stmt->bind_param("sssssisssis", $nom, $prenom, $telephone, $courriel, $password, $adresse_numero, $adresse_rue, $adresse_ville, $adresse_province, $niveau_permissions, $salt);
        $stmt->execute();

        return self::Connexion($courriel, $post['password']);
    }

}