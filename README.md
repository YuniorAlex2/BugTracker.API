# 🚀 Bug Tracker API

A production-style backend API built with ASP.NET Core for managing projects and issues.  
This project was developed to demonstrate backend architecture, API design, and real-world features such as authentication, filtering, and pagination.

---

## 📌 Features

- 🔐 JWT Authentication (Register & Login)
- 📦 Full CRUD for Projects and Issues
- 🔗 One-to-Many relationship (Project → Issues)
- 🔍 Filtering (status, priority, project)
- 🔎 Search (title, description)
- 📄 Pagination with metadata
- ✅ Input validation with Data Annotations
- 🧱 Clean architecture (Controllers + Services + DTOs)
- 📊 Swagger UI for API testing

---

## 🛠️ Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger (Swashbuckle)

---

## 📂 Project Structure
Controllers/ → API endpoints
Services/ → Business logic
Models/ → Database entities
DTOs/ → Request/response shaping
Data/ → DbContext and configuration


---

## 🔐 Authentication

The API uses JWT (JSON Web Tokens).

### Register

POST /api/auth/register

### Login

POST /api/auth/login

Example Endpoints
Get Issues (with filtering & pagination)
GET /api/issues?status=Pending&search=login&pageNumber=1&pageSize=5
Create Issue (Protected)
POST /api/issues
Get Project Issues
GET /api/projects/{id}/issues
📊 Example Response
{
  "data": [
    {
      "id": 1,
      "title": "Login bug",
      "status": "Pending",
      "priority": "High",
      "projectId": 1,
      "projectName": "Bug Tracker API"
    }
  ],
  "pageNumber": 1,
  "pageSize": 5,
  "totalCount": 10,
  "totalPages": 2
}

⚙️ Setup Instructions
Clone the repository
git clone https://github.com/YuniorAlex2/BugTracker.API

Navigate to the project
cd BugTracker.API

Configure connection string in appsettings.json

Run migrations
dotnet ef database update

Run the API
dotnet run

Open Swagger
https://localhost:xxxx/swagger
