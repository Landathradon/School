CREATE DATABASE  IF NOT EXISTS `school` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `school`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: school
-- ------------------------------------------------------
-- Server version	5.7.21-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cours`
--

DROP TABLE IF EXISTS `cours`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cours` (
  `id` varchar(5) NOT NULL,
  `desc` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_mysql500_ci DEFAULT NULL,
  `duree` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cours`
--

LOCK TABLES `cours` WRITE;
/*!40000 ALTER TABLE `cours` DISABLE KEYS */;
INSERT INTO `cours` VALUES ('ARP','APPROCHE STRUCTURÉE À LA RÉSOLUTION DE PROBLÈMES',60),('AWB','ANIMATION WEB',60),('BD1','BASE DE DONNÉES 1',60),('BD2','BASE DE DONNÉES 2',60),('DCS','DÉVELOPPEMENT WEB CÔTÉ SERVEUR',60),('DGP','DÉVELOPPEMENT ET GESTION DE PROJETS',60),('DM1','DÉVELOPPEMENT APPLICATIONS MOBILES 1',60),('DM2','DÉVELOPPEMENT APPLICATIONS MOBILES 2',60),('DW1','DÉVELOPPEMENT WEB 1',60),('DW2','DÉVELOPPEMENT WEB 2',60),('INF','INFONUAGIQUE',60),('NTE','NOUVELLES TECHNOLOGIES',60),('P11','PROJET D’INTÉGRATION 1 – PROGRAMMATION ORIENTÉ OBJET',60),('P12','PROJET D’INTÉGRATION 2 – PROGRAMMATION WEB',60),('PFE','PROJET DE FIN D’ÉTUDES – INTÉGRATION',270),('PO1','PROGRAMMATION ORIENTÉE OBJET 1',60),('PO2','PROGRAMMATION ORIENTÉE OBJET 2',60),('PPA','PROFESSION DE PROGRAMMEUR-ANALYSTE',60),('PWB','PROGRAMMATION WEB',60),('TDD','TRAITEMENT DE DONNÉES',60);
/*!40000 ALTER TABLE `cours` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `evaluations`
--

DROP TABLE IF EXISTS `evaluations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `evaluations` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `codeCours` varchar(5) DEFAULT NULL,
  `ponderation` int(10) unsigned DEFAULT NULL,
  `evaluation` varchar(45) DEFAULT NULL,
  `note` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evaluations`
--

LOCK TABLES `evaluations` WRITE;
/*!40000 ALTER TABLE `evaluations` DISABLE KEYS */;
INSERT INTO `evaluations` VALUES (1,'PPA',30,'Examen intra',100),(2,'PPA',30,'Examen final',100),(3,'PPA',40,'Projet 1',100),(4,'DW1',30,'Examen intra',100),(5,'DW1',30,'Examen final',100),(6,'DW1',40,'Projet 1',100),(7,'ARP',15,'Examen intra',100),(8,'ARP',15,'Examen final',92),(9,'ARP',30,'TP et Professionnalisme',100),(10,'ARP',15,'Projet 1',100),(11,'ARP',15,'Projet 2',100),(12,'ARP',10,'Projet 3',100),(13,'DW2',30,'Examen intra',100),(14,'DW2',30,'Examen final',92),(15,'DW2',40,'Projet 1',100),(16,'AWB',30,'Examen intra',0),(17,'AWB',30,'Examen final',0),(18,'AWB',40,'Projet 1',0),(19,'PO1',30,'Examen intra',100),(20,'PO1',30,'Examen final',92),(21,'PO1',40,'Projet 1',100),(22,'PO2',30,'Examen intra',100),(23,'PO2',30,'Examen final',92),(24,'PO2',40,'Projet 1',100),(25,'BD1',30,'Examen intra',92),(26,'BD1',30,'Examen final',92),(27,'BD1',40,'Projet 1',100),(28,'BD2',30,'Examen intra',92),(29,'BD2',30,'Examen final',92),(30,'BD2',40,'Projet 1',100),(31,'TDD',30,'Examen intra',100),(32,'TDD',30,'Examen final',92),(33,'TDD',40,'Projet 1',100),(34,'DCS',30,'Examen intra',100),(35,'DCS',30,'Examen final',96),(36,'DCS',40,'Projet 1',100),(37,'PWB',30,'Examen intra',0),(38,'PWB',30,'Examen final',0),(39,'PWB',40,'Projet 1',0),(40,'DM1',30,'Examen intra',0),(41,'DM1',30,'Examen final',0),(42,'DM1',40,'Projet 1',0),(43,'DM2',30,'Examen intra',0),(44,'DM2',30,'Examen final',0),(45,'DM2',40,'Projet 1',0),(46,'INF',30,'Examen intra',0),(47,'INF',30,'Examen final',0),(48,'INF',40,'Projet 1',0),(49,'NTE',30,'Examen intra',0),(50,'NTE',30,'Examen final',0),(51,'NTE',40,'Projet 1',0),(52,'P11',100,'Projet 1',0),(53,'P12',100,'Projet 1',0),(54,'PFE',100,'Projet 1',0);
/*!40000 ALTER TABLE `evaluations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resultats`
--

DROP TABLE IF EXISTS `resultats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `resultats` (
  `id` varchar(5) NOT NULL,
  `note` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resultats`
--

LOCK TABLES `resultats` WRITE;
/*!40000 ALTER TABLE `resultats` DISABLE KEYS */;
INSERT INTO `resultats` VALUES ('',0),('ARP',98.8),('AWB',0),('BD1',95.2),('BD2',95.2),('DCS',98.8),('DGP',0),('DM1',0),('DM2',0),('DW1',100),('DW2',97.6),('INF',0),('NTE',0),('P11',0),('P12',0),('PFE',0),('PO1',97.6),('PO2',97.6),('PPA',100),('PWB',0),('TDD',97.6);
/*!40000 ALTER TABLE `resultats` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-07-27 18:33:31
