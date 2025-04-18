# 💰 BankAPI

A robust **Banking REST API** built with **C# and .NET**, providing secure and scalable endpoints for users and admins. Features include user registration, authentication via JWT, money transfers, loan handling, and more — all backed by a large-scale **SQL Server** database.

---

## 🛠 Tech Stack

- **C# / .NET**
- **RESTful API**
- **Entity Framework Core**
- **MS SQL Server**
- **JWT Authentication**
- **FluentValidation**
- **AutoMapper**
- **Dependency Injection (DI)**
- **xUnit / Unit Testing**
- **Swagger / OpenAPI**

---

## 🚀 Features

### 👤 Authentication & Authorization
- JWT-based login system
- Role-based access: **User** vs **Admin**
- Secure endpoints protected via middleware

### 💸 Core Banking Features
- **Send Money** between users
- **Loan Requests** and approvals
- Transaction and loan history
- Balance tracking and constraints

### 🧰 Admin Features
- View all users, transactions, loans
- Approve or reject loan requests
- Manage user roles

---

## 🗃️ Database

- Built on **MS SQL Server**
- Optimized for large-scale transactional data
- Uses **Entity Framework Core** 
- Relational design with users, accounts, loans, transactions

---

## 📦 Installation

1. **Clone the repo**
   ```bash
   git clone https://github.com/yourusername/BankAPI.git
   cd BankAPI
   
2.Set up the database

Update your connection string in appsettings.json

Run migrations:

    dotnet ef database update
    
Run the API

    dotnet run
    
Swagger UI Navigate to https://localhost:5001/swagger to explore the API.




