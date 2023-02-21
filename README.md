## Assignment_Create_a_database_and_access_it
 
### Part 1 - Superheroes db,
SQL scripts for DDL and DML of the superheroes db (See queries folder). This database contains different superheroes, their assistants and powers.  
### Part 2 - Chinook database, 
Part 2 was to setup the Chinook database using the given SQL file and later access it by creating a C# console application.

## Technologies 
Technologies used for the assignment is [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (.Net 6.0), [SQL server management studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16), console project.

# Installation
## part 1
To run part 1 of the assignment follow the steps of "install database" but instead of chooseing the file "Chinook_SqlServer_AutoIncrementPKs.sql", choose any of the files in the "Queries" folder within this project. However, make sure to execute these files in the right order.

## part 2
Firstly, to run this project you need to setup the Chinook database on your system. 
### install database
An example of how this is done is using Sql Server Management System. 
- download and install SSMS, [SQL server management studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16). 
- you also need the Chinook db file provided by Noroff for this assignment
- start the program, it will ask you for a "server name" what was used in this project was "(LocalDb)\MSSQLLocalDB"
- In SSMS navigate to "File" -> "Open" -> "File..."
- Visual Studio will then open a file explorer, navigate to the file "Chinook_SqlServer_AutoIncrementPKs.sql"
- When the file opens in SSMS, use "execute" to run the file and create the database. 
### install Visual studio and open project
We also needs to be able to run the project itself. 
An example on how to run this project is through visual studio. 
- download and install a prefered version of [Visual Studio 2022](https://visualstudio.microsoft.com/vs/).
- Clone this repository to your device.
- In Visual Studio navigate to "File" -> "Open" -> "Project/Solution" 
- Visual Studio will then open a file explorer, navigate to the project and select the file "Assignment_Create_a_database_and_access_it.sln"

Once the project is imported we can run the Program.cs file to get some example use of the project. 

### authors
Made by Jonas Duong, Adam Nyman and Robin Stempa.




