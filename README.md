# Tournament-Management-App

Aplicación Web, utilizando .Net Core y SQLServer, a través de Visual Studio Code. Reto del ciclo 3 del grupo 26 de MisionTic2022.

This repository contains the code for the "Torneo.App" application, which manages tournaments and related entities. The application is divided into several projects, each serving a specific purpose. Below is a brief description of each project:

## Projects

1. ### Torneo.App.Consola

   This project contains the console application for the Torneo.App. It allows users to interact with the application using the command line. The main entry point is `Program.cs`, and the project file is named `Torneo.App.Consola.csproj`.

2. ### Torneo.App.Dominio

   The `Torneo.App.Dominio` project includes the domain logic and entity classes for the application. These classes represent the core entities of the tournament system, such as `DirectorTecnico`, `Equipo`, `Jugador`, `Municipio`, `Partido`, and `Posicion`. The project file is named `Torneo.App.Dominio.csproj`.

3. ### Torneo.App.Frontend

   This project contains the frontend part of the Torneo.App. It appears to be an ASP.NET Core web application as it contains the "Pages" folder, which typically contains Razor Pages. The frontend uses ASP.NET Core conventions, and it includes configuration files, Razor Pages, and other web-related files. The main entry point is `Program.cs`, and the project file is named `Torneo.App.Frontend.csproj`. The `wwwroot` folder contains web-related resources such as CSS, JavaScript, and other static files.

4. ### Torneo.App.Persistencia

   The `Torneo.App.Persistencia` project handles data persistence and database interactions for the Torneo.App application. It includes a `DataContext.cs` class representing the database context and several interfaces and repository classes for each entity, like `RepositorioDT.cs`, `RepositorioEquipo.cs`, `RepositorioJugador.cs`, `RepositorioMunicipio.cs`, `RepositorioPartido.cs`, and `RepositorioPosicion.cs`. The project file is named `Torneo.App.Persistencia.csproj`, and it contains a `Migrations` folder for managing database migrations.

## How to Use

To use the "Torneo.App" application, follow these steps:

1. **Clone the Repository:**
   ```bash
   git clone <repository_url.git>
   ```

2. **Navigate to the Console Application:**
   ```bash
   cd Torneo.App/Torneo.App.Consola
   ```

3. **Build and Run the Console Application:**
   ```bash
   dotnet run
   ```

   The console application will start, and you can interact with the application through the command line.

4. **Explore the Frontend (Web Application):**

   The frontend web application can be explored by running it using the following steps:

   ```
   cd Torneo.App/Torneo.App.Frontend
   dotnet run
   ```

   The web application will start, and you can access it using a web browser at `http://localhost:5000`.

## Dependencies

The projects in this repository are built using .NET Core. To run the applications, make sure you have the appropriate .NET Core SDK installed on your machine.

## Contributions

Contributions to the "Torneo.App" project are welcome! If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

Please note that this README only provides a high-level overview of the repository's structure and how to get started with the application. For more in-depth documentation and code details, refer to the individual project files and documentation within each project folder.