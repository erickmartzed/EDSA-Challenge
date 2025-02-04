USE master;

GO

CREATE DATABASE DbMechanicalAssistanceShop;

GO

USE DbMechanicalAssistanceShop


CREATE TABLE Color (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) UNIQUE NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL
)
/* 
CREATE TABLE Continent (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) UNIQUE NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL
)

CREATE TABLE Country (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ContinentId INT NOT NULL,
    Name VARCHAR(100) UNIQUE NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL,
    CONSTRAINT FK_Country_Continent FOREIGN KEY (ContinentId)
        REFERENCES Continent(Id)
) */

CREATE TABLE Brand (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    --CountryId INT NOT NULL,
    Name VARCHAR(100) UNIQUE NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL,
    /* CONSTRAINT FK_Brand_Country FOREIGN KEY (CountryId) 
        REFERENCES Country(Id) */
)

CREATE TABLE Model (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BrandId INT NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL,
    CONSTRAINT FK_Model_Brand FOREIGN KEY (BrandId)
        REFERENCES Brand(Id),
    CONSTRAINT UQ_Model UNIQUE (
        BrandId,
        Name
    )
)

CREATE TABLE Vehicle (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ModelId INT NOT NULL,
    LicensePlate VARCHAR(6) UNIQUE NOT NULL, --Allow only XXX000 XX00XX
    ChassisNumber VARCHAR(30) UNIQUE NOT NULL,
    EngineNumber VARCHAR(20) NOT NULL,
    ColorId INT NOT NULL,
    ManufacturingYear SMALLINT NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL,
    CONSTRAINT FK_Model_Vehicle FOREIGN KEY (ModelId)
        REFERENCES Model(Id),
    CONSTRAINT FK_Color_Vehicle FOREIGN KEY (ColorId)
        REFERENCES Color(Id),
    CONSTRAINT CK_LicensePlate
    CHECK (
        LicensePlate LIKE '[A-Z][A-Z][0-9][0-9][A-Z][A-Z]'
        OR
        LicensePlate LIKE '[A-Z][A-Z][A-Z][0-9][0-9][0-9]'
    )
)

CREATE TABLE Service (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(100) UNIQUE NOT NULL,
    Description VARCHAR(400) NOT NULL,
    Price DECIMAL NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL
)

CREATE TABLE RenderedService (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VehicleId INT NOT NULL,
    Total INT NOT NULL,
    Date DATE,
    Annotation VARCHAR(800) NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL,
    CONSTRAINT FK_Vehicle_RendServ FOREIGN KEY (VehicleId)
        REFERENCES Vehicle(Id)
)

CREATE TABLE ServiceByRendServ (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ServiceId INT NOT NULL,
    RenderedServiceId INT NOT NULL,
    Price INT NOT NULL,
    Valid BIT NOT NULL DEFAULT 1,
    DeletedAt DATETIME DEFAULT NULL,
    CONSTRAINT FK_Service_ServiceByRendServ FOREIGN KEY (ServiceId)
        REFERENCES Service(Id),
    CONSTRAINT FK_RenderedService_ServiceByRendServ FOREIGN KEY (RenderedServiceId)
        REFERENCES RenderedService(Id)
)
