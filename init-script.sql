
-- Path: init-script.sql

/* 
 Init script for the database
*/

EXEC sp_configure 'remote access', 0;
GO  
RECONFIGURE;
GO

EXEC sp_configure 'remote query timeout', 600;
GO
RECONFIGURE;
GO

EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO

EXEC sp_configure 'xp_cmdshell', 1;
GO
RECONFIGURE;
GO

EXEC sp_configure 'show advanced options', 0;
GO
RECONFIGURE;
GO

EXEC sp_configure 'clr enabled', 1;
GO
RECONFIGURE;
GO

EXEC sp_configure 'clr strict security', 0;
GO
RECONFIGURE;
GO

CREATE DATABASE Torneo;
GO

USE Torneo;
GO