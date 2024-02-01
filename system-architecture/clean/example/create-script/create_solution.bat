@echo off

:: Back to example folder
cd ..

:: Create a MyApp folder
mkdir MyApp
cd MyApp

:: Create a new solution named MyAppServiceSolution
dotnet new sln -n MyAppServiceSolution
::  Create a src directory
mkdir src
cd src
::  Create the MyApp.Shared project
dotnet new classlib -n MyApp.Shared
cd ..
dotnet sln add src/MyApp.Shared
cd src
::  Create the MyApp.API project
dotnet new webapi -n MyApp.API
cd ..
dotnet sln add src/MyApp.API
cd src
::  Create the MyApp.Dtos project
dotnet new classlib -n MyApp.Dtos
cd ..
dotnet sln add src/MyApp.Dtos
cd src
::  Create the MyApp.Application project
dotnet new classlib -n MyApp.Application
cd ..
dotnet sln add src/MyApp.Application
cd src
::  Create the MyApp.Domain project
dotnet new classlib -n MyApp.Domain
cd ..
dotnet sln add src/MyApp.Domain
cd src
::  Create the MyApp.Infrastructure project
dotnet new classlib -n MyApp.Infrastructure
cd ..
dotnet sln add src/MyApp.Infrastructure
cd src

:: Create the MyApp.Tests project
dotnet new xunit -n MyApp.Tests
cd ..
dotnet sln add src/MyApp.Tests

echo All projects and solution created successfully.
