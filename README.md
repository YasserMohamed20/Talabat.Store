# Talabat.Store API

A professional E-Commerce RESTful API built using ASP.NET Core Web API following Clean Architecture principles.  
The project provides a scalable and maintainable backend solution for online shopping systems using Generic Repository and Unit Of Work patterns.

---

# 🚀 Features

## Authentication & Authorization
- User Registration
- User Login
- JWT Token Authentication
- Protected APIs
- Current User Endpoint

## Products
- Get All Products
- Get Product By Id
- Get Product Brands
- Get Product Types
- Pagination
- Filtering
- Sorting
- Searching

## Basket
- Create Basket
- Update Basket
- Retrieve Basket
- Delete Basket

## Orders
- Create Orders
- Retrieve User Orders
- Get Order By Id
- Delivery Methods

## Payment
- Payment Processing
- Basket Payment Integration
---

# 🏗️ Architecture

The project follows:

- Clean Architecture
- Repository Pattern
- Generic Repository Pattern
- Unit Of Work Pattern
- Separation of Concerns

---

# 🛠️ Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- JWT Authentication
- AutoMapper
- Swagger
- Redis
- Generic Repository
- Unit Of Work
- Dependency Injection

---

# 📂 Project Structure

```bash
Talabat.Store
│
├── Talabat.APIs
├── Talabat.Core
├── Talabat.Repository
├── Talabat.Service

```

---

# 📌 Design Patterns

## Generic Repository Pattern

The Generic Repository pattern is used to reduce duplicated CRUD operations and provide reusable data access logic.

Example:

```csharp
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>>GetAll();
        Task<T?> GetById(int id);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<int> GetCountWithSpec(ISpecification<T> spec);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);

    }
```

---

## Unit Of Work Pattern

The Unit Of Work pattern is implemented to manage repositories and save changes using a single transaction.

Example:

```csharp
public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    Task<int> CompleteAsync();
}
```

---

# 🔐 Authentication

JWT Authentication is implemented to secure APIs.

### Features:
- Secure Login
- Token Generation
- Authorized Endpoints
- Current User Retrieval

---

# 📦 API Endpoints

## Accounts

| Method | Endpoint |
|--------|-----------|
| POST | /api/Accounts/Register |
| POST | /api/Accounts/Login |
| GET | /api/Accounts/GetCurrentUser |
| GET | /api/Accounts/Address |
| PUT | /api/Accounts/Address |
| GET | /api/Accounts/emailExists |
| POST | /api/Accounts/logout |

---

## Products

| Method | Endpoint |
|--------|-----------|
| GET | /api/Product |
| GET | /api/Product/{id} |
| GET | /api/Product/brands |
| GET | /api/Product/types |

---

## Basket

| Method | Endpoint |
|--------|-----------|
| GET | /api/Basket |
| POST | /api/Basket |
| DELETE | /api/Basket |

---

## Orders

| Method | Endpoint |
|--------|-----------|
| POST | /api/Orders |
| GET | /api/Orders |
| GET | /api/Orders/{id} |
| GET | /api/Orders/DeliveryMethod |

---

## Payment

| Method | Endpoint |
|--------|-----------|
| POST | /api/Payment/{basketId} |


---

# ⚙️ Getting Started

## 1️⃣ Clone Repository

```bash
git clone(https://github.com/YasserMohamed20/Talabat.Store)
```

---

## 2️⃣ Configure Database

Update your connection string inside:

```json
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TalabatStore;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

## 3️⃣ Apply Migrations

Using Package Manager Console:

```bash
Update-Database
```

Or using .NET CLI:

```bash
dotnet ef database update
```

# 🧠 Key Concepts Implemented

- Generic Repository
- Unit Of Work
- Dependency Injection
- RESTful APIs
- DTOs
- AutoMapper
- Pagination
- Specification Pattern
- Error Handling Middleware
- Authentication & Authorization

---


# 📸 Swagger Screenshots

## 🔐 Accounts APIs

![Accounts APIs](https://github.com/YasserMohamed20/Talabat.Store/blob/master/Talabat.Repository/1.png)

---

## 🛒 Basket APIs & 📦 Orders APIs

![Basket APIs](https://github.com/YasserMohamed20/Talabat.Store/blob/master/Talabat.Repository/2.png)

---

## 💳 Payment APIs & 🛍️ Product APIs

![Orders APIs](https://github.com/YasserMohamed20/Talabat.Store/blob/master/Talabat.Repository/3.png)

---

# 📈 Future Improvements

- Refresh Tokens
- Role-Based Authorization
- Docker Support
- CI/CD Pipeline
- Email Service
- Angular Frontend
- Redis Caching Optimization

---

# 👨‍💻 Author

## Yasser Mohamed Al-Ghazali

.NET Developer passionate about Backend Development and ASP.NET Core.

---
# 🌐 Connect With Me

- GitHub: https://github.com/YasserMohamed20
- LinkedIn: https://www.linkedin.com/in/yasser-mohamed-826734370/


# ⭐ Support

If you like this project, give it a ⭐ on GitHub.

---
