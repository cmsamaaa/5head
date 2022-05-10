-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 10, 2022 at 01:07 PM
-- Server version: 5.1.3
-- PHP Version: 8.1.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `5head`
--

-- --------------------------------------------------------

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
CREATE TABLE IF NOT EXISTS `accounts` (
  `accountID` int NOT NULL AUTO_INCREMENT,
  `username` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `password` text NOT NULL,
  `encryptKey` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `profileID` int NOT NULL,
  `deactivated` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`accountID`),
  UNIQUE KEY `username` (`username`)
) ENGINE=MyISAM AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `accounts`
--

INSERT INTO `accounts` (`accountID`, `username`, `password`, `encryptKey`, `profileID`, `deactivated`) VALUES
(0, 'admin', 'Dm9c0iFRidUmWE2KEb7e4Q==', 't4J8TQqHvILd', 1, 0),
(5, 'staff1', 'G08NAWlMqAKi1AyBsC3imQ==', 'tva9Bjw8EWYA', 4, 0),
(6, 'staff2', 'Da7DhRJ0HUtVx+sE2eBkCA==', '5eIK8-6iPPJp', 4, 0),
(7, 'manager', 'Z8e9BfR+eRdiCJ6gPOM/Zg==', 'wXzlGDsXTzdS', 3, 0);

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
CREATE TABLE IF NOT EXISTS `categories` (
  `categoryID` int NOT NULL AUTO_INCREMENT,
  `categoryName` varchar(200) NOT NULL,
  `deactivated` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`categoryID`),
  UNIQUE KEY `categoryName` (`categoryName`)
) ENGINE=MyISAM AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`categoryID`, `categoryName`, `deactivated`) VALUES
(1, 'Drinks', 0),
(2, 'Pasta', 0),
(3, 'Soup', 0);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `orderID` int(11) NOT NULL,
  `staffID` int(11) NOT NULL,
  `productID` int(11) NOT NULL,
  `categoryID` int(11) NOT NULL,
  `productName` varchar(200) NOT NULL,
  `price` float NOT NULL,
  `status` text DEFAULT 'Not Paid',
  PRIMARY KEY (`orderID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `orders`
--

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `productID` int NOT NULL AUTO_INCREMENT,
  `productName` text NOT NULL,
  `price` double NOT NULL,
  `deactivated` tinyint(1) NOT NULL DEFAULT '0',
  `categoryID` int NOT NULL,
  PRIMARY KEY (`productID`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`productID`, `productName`, `price`, `deactivated`, `categoryID`) VALUES
(1, 'Carbonara', 12.9, 0, 2),
(2, 'Mushroom Soup', 5.6, 0, 3),
(3, 'Coke', 2.5, 0, 1);

-- --------------------------------------------------------

--
-- Table structure for table `profiles`
--

DROP TABLE IF EXISTS `profiles`;
CREATE TABLE IF NOT EXISTS `profiles` (
  `profileID` int NOT NULL AUTO_INCREMENT,
  `profileName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `deactivated` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`profileID`),
  UNIQUE KEY `profileName` (`profileName`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `profiles`
--

INSERT INTO `profiles` (`profileID`, `profileName`, `deactivated`) VALUES
(1, 'Administrator', 0),
(2, 'Restaurant Owner', 0),
(3, 'Restaurant Manager', 0),
(4, 'Restaurant Staff', 0),
(5, 'Customer', 0),
(8, 'Test', 0);

-- --------------------------------------------------------

--
-- Table structure for table `staffs`
--

DROP TABLE IF EXISTS `staffs`;
CREATE TABLE IF NOT EXISTS `staffs` (
  `staffID` int NOT NULL AUTO_INCREMENT,
  `firstName` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `lastName` text NOT NULL,
  `accountID` int NOT NULL,
  PRIMARY KEY (`staffID`),
  UNIQUE KEY `accountID` (`accountID`)
) ENGINE=MyISAM AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `staffs`
--

INSERT INTO `staffs` (`staffID`, `firstName`, `lastName`, `accountID`) VALUES
(1, 'Jackson', 'Lim', 5),
(2, 'Jane', 'Ho', 6),
(11, 'John', 'Chan', 7);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
