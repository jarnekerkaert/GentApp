# GentApp
Project Windows, UWP app for city Ghent.

### Install and run
The frontend and backend are both in the same project. To run the project you will have to download and install [PostgreSQL](https://www.postgresql.org/download/). After this, clone the project to your directory of choice and open the solution in visual studio. 

Because this project contains two projects, you will have to change the run configuration to start both projects at once. To do this, go to the solution properties -> Common properties -> Startup Project, select Multiple Startup Projects. 

It is also required that you create a database in postgres called "gentdb" with username: postgres and password: root. Finally, you have to generate the database model. To do this in visual studio, go to Tools -> NuGet Package Manager -> Package Manager Console and execute the following commands:
 1) Add-Migration Initial
 2) Update-Database

After successfully completing these steps you should be able to run the project.

### Authors
 - Jarne Kerkaert
 - Nathan Westerlinck
 
 ### Group
 - ENG1
