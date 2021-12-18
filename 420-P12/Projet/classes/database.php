<?php

class database
{
    private static $db;
    private mysqli $connection;

    private function __construct() {
        $this->connection = new mysqli(DB_HOST,DB_USER, DB_PASSWORD, DB_DATABASE);
    }

    function __destruct() {
        $this->connection->close();
    }

    public static function getConnection(): mysqli
    {
        if (self::$db == null) {
            self::$db = new Database();
        }
        return self::$db->connection;
    }
}