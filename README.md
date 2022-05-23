# 5Head - Restaurant Ordering System

##### CSIT314 : Software Development Methodologies Project

## Table of contents

-   [Pre-requisites](#pre-requisites)
-   [Setup Guide](#setup-guide)
-   [Documentations](#documentations)
-   [Login Information](#login-information)
-   [Requirements](#requirements)
-   [Dependencies](#dependencies)

## Pre-requisites

-   Visual Studio 2022 (I'm using Enterprise)
-   MySQL 7.0 and above (Community + Workbench / XAMPP / wampserver)

## Setup Guide

1. Clone this repository to your computer
2. Create database named "5head" (if you haven't)
3. Select 5head, then import "5head.sql" in
4. Visual Studio NuGet Package Manager
    - Install all necessary dependencies including [MySQL.Data]

## Documentations

-   #### Scripts/Libraries
    -   Consists of "quality-of-life" methods we use often in the web application
-   #### Entity (combined with Data Access Layer)
    -   Consists of the object classes (Let C# enjoy polymorphism)
-   #### Data Access Layer
    -   Consists of the code that communicates with the database
    -   Has very minimal validation and is error-prone
    -   Extensive validation should occur within the Controller
    -   Focus only on data access instead of data storage
-   #### Controller (Business Logic Layer)
    -   Consists of almost all validations and logic code
    -   Main code that controls/runs all application logic
    -   Describes how objects interact with each other, where the Boundary and Entity can indirectly communicate with each other

### Login Information

    Administrator Account
    Username: admin
    Password: 123
	
	Owner Account (required to see and use owner features)
    Username: owner
    Password: 111

    Manager Account (required to see and use manager features)
    Username: manager
    Password: 222

    Staff Account (required to see and use staff features)
    Username: staff
    Password: 333

### Requirements

To insert project requirements here

### Dependencies

    MySQL.Data
