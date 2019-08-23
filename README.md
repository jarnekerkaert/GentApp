# GentApp
Project Windows, UWP app for city Ghent.

### Install and run
The frontend and backend are both in the same project. to run the project you will have to downdload and install [PostgreSQL](https://www.postgresql.org/download/). After this, clone the project to your directory of choice and open the solution in visual studio. 

Because this project contains two projects, you will have to change the run configuration to start both projects at once. To do this, go to the solution properties -> Common properties -> Startup Project, select Multiple Startup Projects. 

Finally it is also required that you create a database in postgres called "gentdb" with username: postgres and password: root. After these steps you should be able to run the project.

### Authors
 - Jarne Kerkaert
 - Nathan Westerlinck
 
 ### Group
 - ENG1
