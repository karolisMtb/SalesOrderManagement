Documentation

Download SQL Server Management Studio and install SQL Server.
Download SalesOrderManagement folder from github, unzip it and open it in visual studio.
Delete ALL the files from folder SalesOrderManagement.DataAccess\Migrations
Open Package Manager Console, change default project to SalesOrderManagement.DataAccess, run the following commands: add-migration initialMigration update-database
Check the newly created SalesOrderManagement database in SQL Server management studio. Tables will be empty.
Run application.
