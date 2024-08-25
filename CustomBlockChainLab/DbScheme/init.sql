create database Blockchain;

use Blockchain;
CREATE TABLE Blocks (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Data TEXT NOT NULL,
    Hash VARCHAR(64) NOT NULL,
    PreviousHash VARCHAR(64) NOT NULL,
    TimeStamp DATETIME NOT NULL,
    Nonce INT NOT NULL,
    ChameleonSignature NVARCHAR(256)
);

