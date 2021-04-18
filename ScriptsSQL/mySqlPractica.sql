drop database if exists Practica;
create database Practica;
use Practica;





CREATE TABLE `Usuario` (
  `Id` int(6) NOT NULL AUTO_INCREMENT,  
  `Nombre` varchar(200) NOT NULL,
  `Edad` int NOT NULL,
  `Email` varchar(200) NOT NULL,
  `Password` varchar(64) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
