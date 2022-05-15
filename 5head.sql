-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 14, 2022 at 07:48 AM
-- Server version: 8.0.21
-- PHP Version: 7.3.21

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
) ENGINE=MyISAM AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `accounts`
--

INSERT INTO `accounts` (`accountID`, `username`, `password`, `encryptKey`, `profileID`, `deactivated`) VALUES
(0, 'admin', 'Dm9c0iFRidUmWE2KEb7e4Q==', 't4J8TQqHvILd', 1, 0),
(3, 'staff', 'CVC736It92Rdvs7PTipJcw==', 'h1-UsyxOikRS', 4, 0),
(2, 'manager', 'UVflKxqFWw1+zdNeg8gz0w==', 'vF9pLFyyY4mX', 3, 0),
(1, 'owner', 'T78WaYX9Ri9MPwx9cYp9xg==', 'E-QlGzSEP4kW', 2, 0);

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
-- Table structure for table `coupons`
--

DROP TABLE IF EXISTS `coupons`;
CREATE TABLE IF NOT EXISTS `coupons` (
  `couponID` int NOT NULL AUTO_INCREMENT,
  `code` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `discount` int NOT NULL,
  `deactivated` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`couponID`),
  UNIQUE KEY `code` (`code`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `coupons`
--

INSERT INTO `coupons` (`couponID`, `code`, `discount`, `deactivated`) VALUES
(1, 'GREEDISGOOD', 15, 0),
(2, 'THEREISNOSPOON', 5, 1),
(3, 'CNY2022', 8, 0),
(4, 'HAPPYHOLS2022', 5, 0);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `orderID` INTEGER PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `tableNumber` int NOT NULL,
  `productID` int NOT NULL,
  `categoryID` int NOT NULL,
  `productName` varchar(200) NOT NULL,
  `productQty` int NOT NULL,
  `price` double NOT NULL,
  `start_datetime` DATETIME NOT NULL,
  `end_datetime` DATETIME NOT NULL,
  `status` varchar(200) DEFAULT 'Not Paid',
  `finalPrice` double NOT NULL,
  `contacts` VARCHAR(200) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `staffs`
--

INSERT INTO `staffs` (`staffID`, `firstName`, `lastName`, `accountID`) VALUES
(1, 'Jackson', 'Lim', 3),
(11, 'Jack', 'Horse', 2),
(12, 'Elonge', 'Moss', 1);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
