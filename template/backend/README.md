# Sales API

## 📌 Overview
This API manages sales records following **DDD** principles and using the **External Identities** pattern for entity references. It supports complete **CRUD operations** and applies **business rules** for quantity-based discounts.

### Features:
- ✅ Create, read, update, and delete users
- ✅ Create, read, update, and delete branches
- ✅ Create, read, update, and delete customers
- ✅ Create, read, update, and delete products
- ✅ Create, read, update, and delete sales
- ✅ Business rules for discounts based on item quantity
- ✅ Logging for domain events (`SaleCreated`, `SaleModified`, `SaleCancelled`, `ItemCancelled`)
- ✅ Filtering, pagination, and sorting on sales queries

## 🏗️ Project Setup

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (or MongoDB if you chose that)
- [Docker](https://www.docker.com/) (optional, for containerized execution)

### Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/your-username/sales-api.git
   cd sales-api
   ```

2. Configure the database:
   - If using **PostgreSQL**, update `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=SalesDb;Username=your_user;Password=your_password"
     }
     ```
   - If using **Docker**, run:
     ```sh
     docker-compose up -d
     ```

3. Apply migrations:
   ```sh
   dotnet ef database update
   ```

4. Run the API:
   ```sh
   dotnet run
   ```

## 🔍 Testing the API
1. Run unit tests:
   ```sh
   dotnet test
   ```
2. Use **Postman** or **Swagger** (`/swagger` route) to interact with the API.

## 🛠️ Business Rules
- **4+ items** → **10% discount**
- **10-20 items** → **20% discount**
- **Max 20 items per product**
- **Less than 4 items** → **No discount**

## 🔗 Endpoints (Example)
| Method | Endpoint               | Description                     |
|--------|------------------------|---------------------------------|
| POST   | `/api/sales`           | Create a sale                  |
| GET    | `/api/sales`           | Get all sales (filterable)     |
| GET    | `/api/sales?page=1`    | List paginated sales           |
| GET    | `/api/sales/{id}`      | Get a sale by ID               |
| PUT    | `/api/sales/{id}`      | Update a sale                  |
| DELETE | `/api/sales/{id}`      | Delete a sale                  |

More detailed request/response examples can be found in Swagger.
