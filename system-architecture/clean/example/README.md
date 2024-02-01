# Clean Architecture

## Here's a step by step guide to help you scaffold your Clean project

### Do it Automatically

You can do it Automatically by run the script inside the `create-script` folder. I attached one for Window `bat` file and one for Linux `sh` file.

### Do it Manually

1. Create a new solution named MyAppServiceSolution using the dotnet CLI:

```
dotnet new sln -n MyAppServiceSolution
```

2. Create the MyApp.Shared project:

```
dotnet new classlib -n MyApp.Shared
```

3. Create the MyApp.API project:

```
dotnet new webapi -n MyApp.API
```

4. Create the MyApp.Dtos project:

```
dotnet new webapi -n MyApp.Dtos
```

5. Create the MyApp.Application project:

```
dotnet new classlib -n MyApp.Application
```

6. Create the MyApp.Domain project:

```
dotnet new classlib -n MyApp.Domain
```

7. Create the MyApp.Infrastructure project:

```
dotnet new classlib -n MyApp.Infrastructure
```

8. Create the MyApp.Tests project:

```
dotnet new xunit -n MyApp.Tests
```

9. Add all the projects to the solution:

```
dotnet sln add MyApp.Shared
dotnet sln add MyApp.API
dotnet sln add MyApp.Application
dotnet sln add MyApp.Domain
dotnet sln add MyApp.Infrastructure
dotnet sln add MyApp.Tests
```

10. Now, you can start creating the directories and files as per your structure in each project. For example, in MyApp.API project, create Controllers and Extensions directories:

```
cd MyApp.API
mkdir Controllers Extensions
```

11. Create a UserController.cs in Controllers directory and AppExtension.cs in Extensions directory:

```
touch Controllers/UserController.cs Extensions/AppExtension.cs
```

Repeat similar steps for other projects as well.
