# Freelance.ProjectManagement

To compile and run source code locally:

### Backend prerequisites:
.NET 7.0\
SQL Server Express 2019 LocalDB (or higher)\

### Frontend prerequisites:
Node.js 18+

## Start backend
1. cd to the root folder of the solution
2. run 'dotnet restore' to restore all the external NuGet-packages 
3. cd to './Freelance.ProjectManagement.API'
4. run 'dotnet run --urls https://localhost:7013/' in PowerShell or CMD
5. Browse to https://localhost:7013/swagger to make sure that API is running

Alternatively, Backend can be started from Visual Studio.

## Start frontend
1. cd to './Freelance.ProjectManagement.UI'
2. run 'npm i' to install all external packages
3. run 'npm start'
4. Browse to http://localhost:3000

## You can
1. change DB connection string in ./Freelance.ProjectManagement.API/appsettings.json
2. in case you api is runnig on some port different from 7013, then you need to make corresponding corrections in ./Freelance.ProjectManagement.UI/src/constants.js
