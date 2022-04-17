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

-   Visual Studio (I'm using Enterprise)
-   MySQL (Workbench / XAMPP / wampserver)

## Setup Guide

1. Clone this repository to your computer
2. Import "5head.sql" into MySQL
3. Visual Studio NuGet Package Manager
    - Install all necessary dependencies including [MySQL.Data]

## Documentations

-   #### Scripts/Libraries
    -   Consists of "quality-of-life" methods we use often in the web application
-   #### Entity
    -   Consists of the object classes (Let C# enjoy polymorphism)
-   #### DAL (Data Access Layer)
    -   Consists of the code that communicates with the database
    -   Has very minimal validation and is error-prone
    -   Extensive validation should occur within the BLL
    -   Focus only on data access instead of data storage
-   #### BLL (Business Logic Layer)
    -   Consists of almost all validations and logic code
    -   Main code that controls/runs all application logic
    -   Describes how objects interact with each other, where the front-end and DAL can indirectly communicate with each other

### Login Information
-   Administrator Account
    Username: admin
    Password: 123

### Requirements

To insert project requirements here

### Dependencies

    MySQL.Data
