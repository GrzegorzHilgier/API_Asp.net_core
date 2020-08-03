CREATE LOGIN owner WITH PASSWORD = 'admin1234!';
CREATE DATABASE Store;
GO
USE Store;
CREATE USER owner;
GO
EXEC sp_addrolemember N'db_owner', N'owner';
GO