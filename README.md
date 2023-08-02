# Tournament-Management-App

Aplicación Web, utilizando .Net Core y SQLServer, a través de Visual Studio Code. Reto del ciclo 3 del grupo 26 de MisionTic2022.

Este repositorio contiene el código para la aplicación "Torneo.App", la cual gestiona torneos y entidades relacionadas. La aplicación está dividida en varios proyectos, cada uno con un propósito específico. A continuación, se presenta una breve descripción de cada proyecto:

## Proyectos

1. ### Torneo.App.Consola

   Este proyecto contiene la aplicación de consola para Torneo.App. Permite a los usuarios interactuar con la aplicación a través de la línea de comandos. El punto de entrada principal es `Program.cs`, y el archivo de proyecto se llama `Torneo.App.Consola.csproj`.

2. ### Torneo.App.Dominio

   El proyecto `Torneo.App.Dominio` incluye la lógica de dominio y las clases de entidad para la aplicación. Estas clases representan las entidades principales del sistema de torneos, como `DirectorTecnico`, `Equipo`, `Jugador`, `Municipio`, `Partido` y `Posicion`. El archivo de proyecto se llama `Torneo.App.Dominio.csproj`.

3. ### Torneo.App.Frontend

   Este proyecto contiene la parte frontal de la aplicación Torneo.App. Parece ser una aplicación web ASP.NET Core, ya que contiene la carpeta "Pages", la cual normalmente contiene Razor Pages. El frontend utiliza las convenciones de ASP.NET Core e incluye archivos de configuración, Razor Pages y otros archivos relacionados con la web. El punto de entrada principal es `Program.cs`, y el archivo de proyecto se llama `Torneo.App.Frontend.csproj`. La carpeta `wwwroot` contiene recursos relacionados con la web, como CSS, JavaScript y otros archivos estáticos.

4. ### Torneo.App.Persistencia

   El proyecto `Torneo.App.Persistencia` maneja la persistencia de datos y las interacciones con la base de datos para la aplicación Torneo.App. Incluye una clase `DataContext.cs` que representa el contexto de la base de datos, y varias interfaces y clases de repositorio para cada entidad, como `RepositorioDT.cs`, `RepositorioEquipo.cs`, `RepositorioJugador.cs`, `RepositorioMunicipio.cs`, `RepositorioPartido.cs` y `RepositorioPosicion.cs`. El archivo de proyecto se llama `Torneo.App.Persistencia.csproj`, y contiene una carpeta `Migrations` para gestionar las migraciones de la base de datos.

## Uso

Para utilizar la aplicación "Torneo.App", sigue estos pasos:

1. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/AlexanderAsprilla98/TorneoFutbol
   ```

2. **Navegar a la Aplicación de Consola:**
   ```bash
   cd Torneo.App/Torneo.App.Consola
   ```

3. **Compilar y Ejecutar la Aplicación de Consola:**
   ```bash
   dotnet run
   ```

   La aplicación de consola se iniciará y podrás interactuar con la aplicación a través de la línea de comandos.

4. **Explorar el Frontend (Aplicación Web):**

   La aplicación web frontend se puede explorar ejecutándola con los siguientes pasos:

   ```
   cd Torneo.App/Torneo.App.Frontend
   dotnet run
   ```

   La aplicación web se iniciará y podrás acceder a ella a través de un navegador web en `http://localhost:5000`.

5. **Utilizar Docker:**

   Si deseas ejecutar la aplicación utilizando Docker, sigue estos pasos:

   - Asegúrate de tener Docker instalado en tu máquina.
   - Abre una terminal y navega hasta el directorio raíz del repositorio.
   - Ejecuta el siguiente comando para construir y ejecutar la aplicación en un contenedor:
   
     ```bash
     docker-compose up -d
     ```
   
   - La aplicación estará disponible en `http://localhost:5000` en tu navegador.

## Dependencias

Los proyectos en este repositorio se construyen utilizando .NET Core. Para ejecutar las aplicaciones, asegúrate de tener instalado el SDK de .NET Core correspondiente en tu máquina.

## Contribuciones

¡Las contribuciones al proyecto "Torneo.App" son bienvenidas! Si encuentras algún problema o tienes sugerencias para mejoras, no dudes en abrir un issue o enviar un pull request.

Ten en cuenta que este README solo proporciona una descripción general de la estructura del repositorio y cómo empezar con la aplicación. Para obtener documentación más detallada y detalles de código, consulta los archivos de proyecto y la documentación en cada carpeta de proyecto.