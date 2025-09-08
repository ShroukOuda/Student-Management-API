# 🔐 Storing Connection Strings Securely in ASP.NET Core (User Secrets)

> **Important**: Never commit connection strings, API keys, or other sensitive data to your repository. Use User Secrets for development and proper secret management for production.

## 🎯 What are User Secrets?

User Secrets is a development-time feature in ASP.NET Core that stores sensitive data outside your project tree. It's perfect for:
- Database connection strings
- API keys
- Third-party service credentials
- Any sensitive configuration data

## 🚀 Quick Setup Guide

### ✅ Step 1: Initialize User Secrets

Navigate to your project directory and initialize User Secrets:

```bash
dotnet user-secrets init
```

This command:
- Adds a `UserSecretsId` to your `.csproj` file
- Creates a unique identifier for your project's secrets

### ✅ Step 2: Add MS SQL Server Connection String to Secrets

**Default SQL Server Authentication (Works on all platforms):**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=StudentDB;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=true;MultipleActiveResultSets=true;"
```

**Platform-specific connection strings:**

**🪟 Windows - SQL Server with Windows Authentication:**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=StudentDB;Integrated Security=true;TrustServerCertificate=true;MultipleActiveResultSets=true;"
```

**🐧 Linux - SQL Server in Docker:**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=StudentDB;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=true;Encrypt=true;"
```

**🍎 macOS - SQL Server in Docker:**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=StudentDB;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=true;Encrypt=true;"
```

**☁️ Azure SQL Database:**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=tcp:yourserver.database.windows.net,1433;Database=StudentDB;User ID=yourusername;Password=YourPassword123!;Encrypt=true;Connection Timeout=30;"
```

**🐳 Docker SQL Server Setup Commands:**

For Linux/macOS users, run SQL Server in Docker first:
```bash
# Pull and run SQL Server Docker container
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrongPassword123!" \
   -p 1433:1433 --name sqlserver --hostname sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest

# Verify container is running
docker ps
```

### ✅ Step 3: View and Manage Secrets

**List all secrets:**
```bash
dotnet user-secrets list
```

**Open secrets file directly:**
```bash
# Windows
code %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json

# macOS/Linux
code ~/.microsoft/usersecrets/<user_secrets_id>/secrets.json
```

**Remove a specific secret:**
```bash
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
```

**Clear all secrets:**
```bash
dotnet user-secrets clear
```

## 💻 Using Secrets in Your Application

### In `Program.cs` or `Startup.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();
```

### Accessing secrets in controllers or services:

```csharp
public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        // Use the connection string...
        return View();
    }
}
```

## 🔍 Verification

Check that your secrets are properly configured:

```csharp
// In a controller or service
public void TestConfiguration()
{
    var connectionString = _configuration.GetConnectionString("DefaultConnection");
    Console.WriteLine($"Connection string exists: {!string.IsNullOrEmpty(connectionString)}");
}
```

## 🏗️ Project Structure

After initialization, your project should look like this:

```
YourProject/
├── YourProject.csproj (contains UserSecretsId)
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
```

Your `.csproj` file will contain:
```xml
<PropertyGroup>
  <UserSecretsId>your-unique-id-here</UserSecretsId>
</PropertyGroup>
```

## ⚠️ Important Notes

- **Development Only**: User Secrets only work in the Development environment
- **Machine Specific**: Secrets are stored per machine and user account
- **Not Encrypted**: Secrets are stored in plain text on your local machine
- **Team Sharing**: Each team member needs to set up their own secrets

## 🚀 Production Deployment

For production environments, use:
- **Azure**: Azure Key Vault
- **AWS**: AWS Secrets Manager
- **Environment Variables**: Set directly on the hosting environment
- **Docker**: Use Docker secrets or environment variables

### Example with Environment Variables:

```bash
# Set environment variable
export ConnectionStrings__DefaultConnection="YourProductionConnectionString"

# Or in Docker
docker run -e ConnectionStrings__DefaultConnection="YourConnectionString" yourapp
```

## 🛠️ Troubleshooting

**Secret not found:**
```bash
# Verify secrets exist
dotnet user-secrets list

# Check UserSecretsId in .csproj
cat YourProject.csproj | grep UserSecretsId
```

**Permission issues:**
```bash
# Check secrets file permissions (macOS/Linux)
ls -la ~/.microsoft/usersecrets/<user_secrets_id>/
```

## 📚 Additional Resources

- [ASP.NET Core Configuration Documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/)
- [Safe Storage of App Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)
- [Configuration in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/)

---

**🔒 Security Tip**: Always use strong passwords and consider using managed identity or certificate-based authentication for production databases.