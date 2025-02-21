# .NET Core 5 Backend API

## 📌 Overview
This is a **.NET Core 5** backend designed to provide API services. It follows a **RESTful API** architecture and uses **Entity Framework Core** for database management.

---

## 📦 Technologies Used
- **.NET Core 5**
- **Entity Framework Core**
- **JWT Authentication**
- **Swagger for API Documentation**
- **Dependency Injection**

---

## 🚀 Getting Started

### 1️⃣ Prerequisites
- Install [.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- Install **SQL Server** or any supported database
- Install **Visual Studio** or **VS Code**

---

### 2️⃣ Setup and Configuration
#### Clone the Repository
```sh
git clone https://github.com/your-repo-url.git
cd your-project-name

```
### 3️⃣Run Database Migrations
#### Run the following command to apply migrations
```sh
dotnet ef database update
```
#### If you haven’t created migrations yet
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update

```
### 4️⃣ Running the Application
#### Start the API using
```sh
dotnet run
```
#### The API will be available at
```sh
http://localhost:5000
https://localhost:5001 (for HTTPS)
