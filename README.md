# Cinema Ticket Booking

## Configuration

Before running the project, please follow these configuration steps:

1. **Connection String**: Update the database connection string in the `appsettings.json` or `appsettings.Development.json` file to point to your database. Be sure to include the server name, database name, username, and password for your database server. Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_name;Database=your_database_name;User=your_username;Password=your_password;"
  }
}

2. Database Setup:

Database Creation: Ensure that your database exists. You can manually create it or use Entity Framework Core migrations to create the database for you.

Migrations: If you are using Entity Framework Core, run migrations to create or update the database schema based on your model classes. Use the following commands in the terminal:

Update-Database



