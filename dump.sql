-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema estoque
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema estoque
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `estoque` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
-- -----------------------------------------------------
-- Schema Fornecedores
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema Fornecedores
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Fornecedores` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
-- -----------------------------------------------------
-- Schema projetos
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema projetos
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `projetos` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
-- -----------------------------------------------------
-- Schema usuarios
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema usuarios
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `usuarios` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `estoque` ;

-- -----------------------------------------------------
-- Table `estoque`.`Produtos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `estoque`.`Produtos` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Cor` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Quantidade` INT NOT NULL,
  `ValorUnitario` INT NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `estoque`.`__EFMigrationsHistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `estoque`.`__EFMigrationsHistory` (
  `MigrationId` VARCHAR(150) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProductVersion` VARCHAR(32) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

USE `Fornecedores` ;

-- -----------------------------------------------------
-- Table `Fornecedores`.`Fornecedores`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Fornecedores`.`Fornecedores` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Cnpj` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Endereco` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Cep` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Telefone` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `Fornecedores`.`__EFMigrationsHistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Fornecedores`.`__EFMigrationsHistory` (
  `MigrationId` VARCHAR(150) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProductVersion` VARCHAR(32) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

USE `projetos` ;

-- -----------------------------------------------------
-- Table `projetos`.`Arquivos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projetos`.`Arquivos` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Extension` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projetos`.`Projetos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projetos`.`Projetos` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Status` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `DataInicio` DATETIME(6) NOT NULL,
  `DataEntrega` DATETIME(6) NOT NULL,
  `Descricao` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `QuantidadeDeChapa` INT NOT NULL,
  `Valor` DECIMAL(65,30) NOT NULL,
  `Chapa` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `projetos`.`__EFMigrationsHistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `projetos`.`__EFMigrationsHistory` (
  `MigrationId` VARCHAR(150) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProductVersion` VARCHAR(32) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

USE `usuarios` ;

-- -----------------------------------------------------
-- Table `usuarios`.`AspNetRoles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`AspNetRoles` (
  `Id` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Name` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `NormalizedName` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

CREATE UNIQUE INDEX `RoleNameIndex` ON `usuarios`.`AspNetRoles` (`NormalizedName` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `usuarios`.`AspNetRoleClaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`AspNetRoleClaims` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `RoleId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ClaimType` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `usuarios`.`AspNetRoles` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `usuarios`.`AspNetRoleClaims` (`RoleId` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `usuarios`.`AspNetUsers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`AspNetUsers` (
  `Id` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `UserName` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `NormalizedUserName` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `Email` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `NormalizedEmail` VARCHAR(256) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `PhoneNumber` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEnd` DATETIME(6) NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

CREATE UNIQUE INDEX `UserNameIndex` ON `usuarios`.`AspNetUsers` (`NormalizedUserName` ASC) VISIBLE;

CREATE INDEX `EmailIndex` ON `usuarios`.`AspNetUsers` (`NormalizedEmail` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `usuarios`.`AspNetUserClaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`AspNetUserClaims` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ClaimType` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `usuarios`.`AspNetUsers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `usuarios`.`AspNetUserClaims` (`UserId` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `usuarios`.`AspNetUserLogins`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`AspNetUserLogins` (
  `LoginProvider` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProviderKey` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProviderDisplayName` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `usuarios`.`AspNetUsers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `usuarios`.`AspNetUserLogins` (`UserId` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `usuarios`.`AspNetUserRoles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`AspNetUserRoles` (
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `RoleId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `usuarios`.`AspNetRoles` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `usuarios`.`AspNetUsers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `usuarios`.`AspNetUserRoles` (`RoleId` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `usuarios`.`AspNetUserTokens`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`AspNetUserTokens` (
  `UserId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `LoginProvider` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Name` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `Value` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `usuarios`.`AspNetUsers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `usuarios`.`RefreshTokens`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`RefreshTokens` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `UserEmail` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `TokenRefresh` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `CriadoEm` DATETIME(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ExpiraEm` DATETIME(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `usuarios`.`__EFMigrationsHistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `usuarios`.`__EFMigrationsHistory` (
  `MigrationId` VARCHAR(150) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  `ProductVersion` VARCHAR(32) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
