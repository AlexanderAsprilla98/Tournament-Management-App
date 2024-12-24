
-- Path: init-script.sql

/********************************
*   Database creation script    *
********************************/

/*----------------------------------------------------------------
*   This script creates the database and the necessary objects   *
*   to run the application.                                      *
*----------------------------------------------------------------*/

--- Enable remote connections
EXEC sp_configure 'remote access', 0; 
GO  
RECONFIGURE;
GO

--- Set remote connections timeout
EXEC sp_configure 'remote query timeout', 600;
GO
RECONFIGURE;
GO

--- Disable advanced options
EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO

--- Disable xp_cmdshell
EXEC sp_configure 'xp_cmdshell', 1;
GO
RECONFIGURE;
GO

--- Show advanced options
EXEC sp_configure 'show advanced options', 0;
GO
RECONFIGURE;
GO

--- Disable CLR
EXEC sp_configure 'clr enabled', 1;
GO
RECONFIGURE;
GO

--- Enable CLR strict security
EXEC sp_configure 'clr strict security', 0;
GO
RECONFIGURE;
GO

--- Create database
CREATE DATABASE Torneo;
GO

--- Use database
USE Torneo;
GO