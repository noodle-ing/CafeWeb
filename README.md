# CafeWeb

## 📝 Features

* **Cafe Management**: Register new cafes with details such as name and description.
* **Photo Uploads**: Add multiple images for each cafe to showcase ambiance and menu items.
* **User Feedback**: Leave ratings and comments to help other users discover high-quality cafes.
* **Search & Browse**: Filter and search cafes by name or rating.
* **Data Validation**: Ensure strong passwords by requiring at least one special character.

## 🚀 Architecture

* **ASP.NET Core MVC**: Implements the Model-View-Controller pattern for clean separation of concerns.
* **Entity Framework Core**: Handles database interactions with PostgreSQL.
* **Controllers**: Process HTTP requests and route to appropriate services.
* **Models**: Define application data structures (Cafe, Photo, Review).
* **Views**: Razor pages for dynamic HTML rendering and user interaction.

## ⚙️ Prerequisites

* [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
* [PostgreSQL](https://www.postgresql.org/download/)
* Visual Studio 2022 or VS Code

## 🚀 Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/noodle-ing/CafeWeb.git
   cd CafeWeb
   ```
2. **Configure the Database**

   * Create a PostgreSQL database, e.g. `CafeWebDb`.
   * Update the connection string in `appsettings.json`:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=CafeWebDb;Username=your_username;Password=YourP@ssw0rd!"
     }
     ```
3. **Apply Migrations and Seed Data**

   ```bash
   dotnet ef database update
   ```
4. **Run the Application**

   ```bash
   dotnet run
   ```
5. **Access the App**

   * Open a browser and navigate to `https://localhost:5001`.

## 📷 Screenshots

*Add screenshots of the main pages (e.g., home, cafe details, feedback form).*

## 🤝 Contributing

Contributions are welcome! Please open an issue or submit a pull request for enhancements or bug fixes.

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
