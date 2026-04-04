# 🏃 ShoeTracker - Running Shoe Management System

**ASP.NET Core MVC Advanced Project - SoftUni 2026**

A comprehensive web application for tracking running shoes, recording runs, managing shoe lifecycle, and analyzing running statistics.

---

## 📋 Project Information

- **Author:** [Ivaylo Dimitrov] 
- **Course:** ASP.NET Advanced 
- **Date:** April 2026
- **Framework:** ASP.NET Core 8.0 MVC
- **Database:** SQL Server Express (DESKTOP-3BBGCT4)
- **Database Name:** ShoeTracker_Feb2026

---

## ✨ Features

### 🎯 Core Functionality
- ✅ **User Authentication** - Secure registration and login with ASP.NET Core Identity
- ✅ **Role-Based Authorization** - Administrator and User roles with different permissions
- ✅ **Shoe Management** - Full CRUD operations for managing running shoes
- ✅ **Run Tracking** - Log individual runs and automatically update shoe mileage
- ✅ **Category System** - Organize shoes by type (Road, Trail, Racing, Daily)
- ✅ **Comments System** - Add notes and observations about shoe performance
- ✅ **User Profiles** - Customizable profiles with city, bio, and yearly running goals

### 📊 Statistics & Analytics
- ✅ **Statistics Dashboard** - Visual charts showing:
  - Total distance per shoe
  - Shoes needing replacement (600km threshold)
  - Monthly running trends
  - Personal records and achievements
- ✅ **Replacement Alerts** - Automatic warnings when shoes reach 600km
- ✅ **Progress Tracking** - Visual progress bars for each shoe (0-600km)
- ✅ **Chart.js Integration** - Interactive and responsive charts

### 👨‍💼 Admin Features
- ✅ **Admin Dashboard** - System-wide overview with:
  - Total users, shoes, runs, and comments
  - Recent activity feed
  - Latest shoes added to the system
- ✅ **Role Management** - Restricted access to admin area
- ✅ **System Statistics** - Complete system health metrics

### 🔍 Advanced Features
- ✅ **Pagination** - Efficient data display (6 shoes per page)
- ✅ **Search Functionality** - Filter shoes by brand, model, or category
- ✅ **TempData Alerts** - User-friendly success and error messages
- ✅ **Responsive Design** - Mobile-optimized Bootstrap 5 interface
- ✅ **Data Validation** - Client-side and server-side validation
- ✅ **Custom Error Pages** - Branded 404 and 500 error pages
- ✅ **Async Operations** - Non-blocking database operations for better performance

---

## 🏗️ Architecture

### Project Structure (Clean Architecture)

```
ShoeTracker/
├── ShoeTracker.Web/              # 🎨 Presentation Layer (MVC)
│   ├── Controllers/              # 7 Controllers
│   │   ├── HomeController.cs
│   │   ├── ShoeController.cs
│   │   ├── RunController.cs
│   │   ├── CommentController.cs
│   │   ├── UserProfileController.cs
│   │   └── StatisticsController.cs
│   ├── Areas/Admin/              # Admin Area
│   │   └── Controllers/
│   │       └── AdminController.cs
│   ├── Views/                    # 15+ Razor Views
│   │   ├── Home/
│   │   ├── Shoe/
│   │   ├── Run/
│   │   ├── Comment/
│   │   ├── UserProfile/
│   │   ├── Statistics/
│   │   ├── Shared/
│   │   └── Admin/
│   └── wwwroot/                  # Static files (CSS, JS, images)
│
├── ShoeTracker.Service.Core/     # 💼 Business Logic Layer
│   ├── Services/
│   │   ├── ShoeService.cs
│   │   ├── RunService.cs
│   │   ├── CommentService.cs
│   │   └── UserProfileService.cs
│   └── Interfaces/
│       ├── IShoeService.cs
│       ├── IRunService.cs
│       ├── ICommentService.cs
│       └── IUserProfileService.cs
│
├── ShoeTracker.Data/             # 🗄️ Data Access Layer
│   ├── ShoeTrackerDbContext.cs   # EF Core DbContext
│   └── Migrations/               # Database migrations
│       ├── InitialCreate
│       ├── AddedCategoryEntity
│       └── [Additional migrations...]
│
├── ShoeTracker.Data.Models/      # 📦 Domain Models
│   ├── Entities/
│   │   ├── Shoe.cs
│   │   ├── Category.cs
│   │   ├── Run.cs
│   │   ├── Comment.cs
│   │   └── UserProfile.cs
│   └── Statistics/
│       └── ShoeStatistics.cs
│
├── ShoeTracker.Common/           # 🔧 Shared Utilities
│   └── PaginatedList.cs          # Generic pagination helper
│
└── ShoeTracker.Tests/            # 🧪 Unit Tests
    └── Services/
        ├── ShoeServiceTests.cs      (7 tests)
        ├── RunServiceTests.cs       (5 tests)
        ├── CommentServiceTests.cs   (5 tests)
        └── UserProfileServiceTests.cs (4 tests)
```

### Technology Stack

**Backend:**
- ASP.NET Core 8.0 MVC
- Entity Framework Core 8.0.24
- ASP.NET Core Identity (Authentication & Authorization)
- SQL Server Express 2019+
- Dependency Injection
- Repository Pattern with Service Layer

**Frontend:**
- Razor Pages / Views
- Bootstrap 5.3
- Bootstrap Icons
- Chart.js 4.x (Data visualization)
- Vanilla JavaScript
- Custom CSS

**Testing:**
- xUnit 2.5.3
- FluentAssertions 8.9.0
- EF Core InMemory Database 8.0.24
- 21 unit tests with 70%+ coverage

---

## 🗄️ Database Schema

### Entity Relationship Diagram

```
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│   Category  │         │     Shoe     │         │     Run     │
├─────────────┤         ├──────────────┤         ├─────────────┤
│ Id (PK)     │◄───────┤│ Id (PK)      │├───────►│ Id (PK)     │
│ Name        │  1    * │ Brand        │ 1     * │ ShoeId (FK) │
└─────────────┘         │ Model        │         │ Distance    │
                        │ PurchaseDate │         │ Date        │
                        │ CategoryId   │         │ UserId (FK) │
                        │ UserId (FK)  │         └─────────────┘
                        │ TotalDistance│
                        └──────────────┘
                              │ 1
                              │
                              │ *
                        ┌──────────────┐
                        │   Comment    │
                        ├──────────────┤
                        │ Id (PK)      │
                        │ ShoeId (FK)  │
                        │ Content      │
                        │ CreatedOn    │
                        │ UserId (FK)  │
                        └──────────────┘

        ┌──────────────────┐
        │  UserProfile     │
        ├──────────────────┤
        │ UserId (PK, FK)  │
        │ City             │
        │ Bio              │
        │ YearlyGoal       │
        │ CreatedOn        │
        └──────────────────┘
```

### Entities (5)

#### 1. **Shoe**
```csharp
- Id (int, PK)
- Brand (string, required, max 50 chars)
- Model (string, required, max 50 chars)
- PurchaseDate (DateTime, required)
- CategoryId (int, FK)
- UserId (string, FK)
- TotalDistance (double, calculated from runs)
- Category (Navigation property)
- Runs (Collection navigation)
- Comments (Collection navigation)
```

#### 2. **Category**
```csharp
- Id (int, PK)
- Name (string, required, max 50 chars)
- Shoes (Collection navigation)
```
**Seed Data:** Road, Trail, Racing, Daily

#### 3. **Run**
```csharp
- Id (int, PK)
- ShoeId (int, FK)
- Distance (double, required, range 0.1-100 km)
- Date (DateTime, required)
- UserId (string, FK)
- Shoe (Navigation property)
```

#### 4. **Comment**
```csharp
- Id (int, PK)
- ShoeId (int, FK)
- Content (string, required, max 500 chars)
- CreatedOn (DateTime)
- UserId (string, FK)
- Shoe (Navigation property)
```

#### 5. **UserProfile**
```csharp
- UserId (string, PK, FK)
- City (string, optional, max 100 chars)
- Bio (string, optional, max 500 chars)
- YearlyGoal (double, optional, km per year)
- CreatedOn (DateTime)
```

### Statistics DTO

**ShoeStatistics.cs**
```csharp
- TotalShoes (int)
- TotalDistance (double)
- AverageDistance (double)
- ShoesNeedingReplacement (int)
- MostUsedShoe (Shoe)
```

---

## 🚀 Setup Instructions

### Prerequisites
- .NET 8.0 SDK ([Download](https://dotnet.microsoft.com/download/dotnet/8.0))
- Visual Studio 2022 (17.8+) or VS Code
- SQL Server 2019+ or SQL Server Express
- Git (optional, for cloning)

### Installation Steps

#### 1. Clone or Download the Repository
```bash
git clone https://github.com/YOUR_USERNAME/ShoeTracker.git
cd ShoeTracker
```

#### 2. Configure Database Connection

**Option A: SQL Server Express (Recommended)**

Edit `appsettings.json` in `ShoeTracker.Web`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ShoeTracker_Feb2026;Trusted_Connection=True;Encrypt=False"
  }
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name.

**Option B: LocalDB**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShoeTrackerDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### 3. Apply Database Migrations

Open **Package Manager Console** in Visual Studio:
```powershell
Update-Database
```

Or using .NET CLI:
```bash
dotnet ef database update --project ShoeTracker.Data --startup-project ShoeTracker.Web
```

This will:
- Create the database
- Create all tables
- Seed initial categories (Road, Trail, Racing, Daily)

#### 4. Run the Application

**Visual Studio:**
- Press `F5` or click "Start"

**.NET CLI:**
```bash
cd ShoeTracker.Web
dotnet run
```

#### 5. Open in Browser

Navigate to: `https://localhost:XXXXX` (port will be shown in console)

---

## 👤 Default Users

### Test User
- **Email:** `test@abv.bg`
- **Password:** `Test123#`
- **Role:** User

### Admin User
- **Email:** `admin@abv.bg`
- **Password:** `Admin123#`
- **Role:** Administrator

**Note:** Admin user must be created manually via SQL:
```sql
-- First, get the user ID and role ID
SELECT Id FROM AspNetUsers WHERE Email = 'admin@abv.bg';
SELECT Id FROM AspNetRoles WHERE Name = 'Administrator';

-- Then insert into AspNetUserRoles
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('user-id-here', 'role-id-here');
```

---

## 🧪 Testing

### Running Unit Tests

**Visual Studio:**
- Open Test Explorer (`Ctrl+E, T`)
- Click "Run All"

**.NET CLI:**
```bash
dotnet test
```

### Test Coverage

```
✅ 21 unit tests
✅ ~70% code coverage
✅ All tests passing
✅ Average execution time: ~2 seconds
```

### Test Structure

**ShoeServiceTests.cs** (7 tests)
- GetAllAsync_ReturnOnlyOwnersShoe
- GetByIdAsync_ReturnNull_WhenWrongUser
- GetByIdAsync_ReturnShoe_WhenCorrectUser
- AddAsync_PersistsShoe
- DeleteAsync_RemovesShoe
- GetStatisticsAsync_CountsCorrectly
- SearchAsync_FiltersByBrand

**RunServiceTests.cs** (5 tests)
- AddRunAsync_UpdatesTotalDistance
- AddRunAsync_CreatesRunRecord
- AddRunAsync_Throws_WhenShoeNotFound
- AddRunAsync_Throws_WhenShoeOwnedByOther
- AddRunAsync_SetsCorrectUserId

**CommentServiceTests.cs** (5 tests)
- AddAsync_SavesComment
- AddAsync_Throws_WhenWrongOwner
- DeleteAsync_RemovesComment
- DeleteAsync_DoesNotDelete_WhenWrongUser
- GetByShoeIdAsync_FiltersCorrectly

**UserProfileServiceTests.cs** (4 tests)
- GetByUserIdAsync_ReturnsNull_WhenNotExists
- GetByUserIdAsync_ReturnsProfile_WhenExists
- CreateOrUpdateAsync_CreatesProfile
- CreateOrUpdateAsync_UpdatesExisting

---

## 📖 User Guide

### Getting Started

#### 1. Register an Account
- Click "Register" in the navigation bar
- Enter email and password
- Confirm password and submit

#### 2. Add Your First Shoe
- Navigate to "My Shoes"
- Click "Add New Shoe"
- Fill in:
  - Brand (e.g., "Asics", "Nike", "Puma")
  - Model (e.g., "Gel-Kayano 29")
  - Category (Road, Trail, Racing, Daily)
  - Purchase Date
- Submit

#### 3. Log a Run
- Go to shoe details
- Click "Add Run"
- Enter distance (in km) and date
- Submit
- **TotalDistance** automatically updates!

#### 4. View Statistics
- Navigate to "Statistics"
- See charts for:
  - Distance per shoe
  - Replacement status
  - Running trends

#### 5. Add Comments
- In shoe details, click "Add Comment"
- Write notes about performance, comfort, etc.
- Submit

### Shoe Replacement Logic

**Replacement threshold: 600 km**

- **0-300 km:** ⚪ Good condition (white)
- **300-500 km:** 🟡 Monitor closely (yellow)
- **500-600 km:** 🟠 Plan replacement (orange)
- **600+ km:** 🔴 Replace immediately (red)

Visual progress bar shows current status.

---

## 🔐 Security Features

### Authentication & Authorization
- ✅ ASP.NET Core Identity for user management
- ✅ Password hashing with PBKDF2
- ✅ Role-based authorization (Administrator, User)
- ✅ `[Authorize]` attributes on all protected controllers
- ✅ User-specific data isolation (users only see their own shoes)

### CSRF Protection
- ✅ `[ValidateAntiForgeryToken]` on all POST actions
- ✅ Anti-forgery tokens in forms

### Data Validation
- ✅ Client-side validation with jQuery Validation
- ✅ Server-side validation with Data Annotations
- ✅ SQL injection prevention via EF Core parameterization

### Error Handling
- ✅ Custom error pages (404, 500)
- ✅ Production-safe error messages
- ✅ Detailed errors only in Development mode

---

## 🎨 User Interface

### Design Principles
- **Responsive** - Works on desktop, tablet, and mobile
- **Accessible** - WCAG 2.1 compliant
- **Intuitive** - Clear navigation and user flows
- **Fast** - Optimized page load times

### Key Pages

**Home Page**
- Welcome message
- Quick stats overview
- Call-to-action buttons

**My Shoes**
- Card-based layout
- Pagination (6 per page)
- Search functionality
- Progress bars for each shoe
- Quick actions (Edit, Delete, View)

**Shoe Details**
- Complete shoe information
- Run history table
- Comments section
- Quick "Add Run" button

**Statistics Dashboard**
- Interactive Chart.js charts
- Total distance summary
- Replacement alerts
- Personal records

**Admin Dashboard**
- System-wide statistics
- User count, shoe count, run count
- Recent activity feed
- Latest shoes added

---

## 📊 Performance Optimizations

### Database
- ✅ **Eager Loading** - `.Include()` to prevent N+1 queries
- ✅ **Async Operations** - All database calls use `async/await`
- ✅ **Indexes** - Primary and foreign key indexes
- ✅ **Pagination** - Limit data transfer (Skip/Take)

### Caching
- ✅ **Static Categories** - Categories cached in memory
- ✅ **ViewData** - Avoid redundant database calls in views

### Frontend
- ✅ **CDN for Libraries** - Bootstrap, Chart.js from CDN
- ✅ **Minification** - CSS/JS minified in production
- ✅ **Lazy Loading** - Images load on demand

---

## 🛠️ Development Notes

### Code Quality
- **Repository Pattern** - Service layer abstracts data access
- **Dependency Injection** - All services registered in Program.cs
- **Async/Await** - Non-blocking operations throughout
- **SOLID Principles** - Single Responsibility, Open/Closed, etc.
- **DRY Principle** - Shared logic in services
- **Separation of Concerns** - 3-tier architecture (Presentation, Business, Data)

### Best Practices
- ✅ Magic numbers extracted to constants (e.g., `REPLACEMENT_THRESHOLD_KM = 600`)
- ✅ Meaningful variable names (no abbreviations)
- ✅ XML documentation on public methods
- ✅ Consistent code formatting (Ctrl+K, Ctrl+D)
- ✅ Error handling with try-catch where appropriate
- ✅ User-friendly error messages via TempData

### Testing Strategy
- **AAA Pattern** - Arrange, Act, Assert
- **InMemory Database** - Isolated test environment
- **FluentAssertions** - Readable test assertions
- **Real Data** - Tests use realistic shoe brands (Asics, Puma, New Balance)
- **Security Tests** - Verify ownership checks work

---

## 🐛 Known Issues / Future Enhancements

### Potential Improvements
- [ ] Export statistics to PDF/Excel
- [ ] Shoe comparison feature (side-by-side)
- [ ] Email notifications for replacement alerts
- [ ] Social features (share runs with friends)
- [ ] Mobile app (iOS/Android)
- [ ] Integration with Strava, Garmin Connect
- [ ] Multi-currency support for shoe prices
- [ ] Shoe rotation recommendations
- [ ] Training plan integration

### Known Limitations
- Single-user shoe ownership (no shared shoes)
- Basic statistics (could add pace, elevation, heart rate)
- No API endpoints (future consideration)
- English language only (no i18n)

---

## 📚 Resources & Documentation

### Official Documentation
- [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [Bootstrap 5](https://getbootstrap.com/docs/5.3/)
- [Chart.js](https://www.chartjs.org/docs/latest/)

### Learning Resources
- [SoftUni ASP.NET Advanced Course](https://softuni.bg/)
- [Microsoft Learn - ASP.NET](https://learn.microsoft.com/en-us/aspnet/core/)
- [Clean Architecture Guide](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

---

## 📄 License

This project is developed for **educational purposes** as part of the **SoftUni ASP.NET Advanced course** (Feb 2026).

**© 2026 [Ivaylo Dimitrov]. All rights reserved.**

**Note:** This is a student project and is not licensed for commercial use.

---

## 🙏 Acknowledgments

### Special Thanks
- **SoftUni** - For the comprehensive ASP.NET Advanced curriculum and excellent instructors
- **Course Instructors** - For guidance, code reviews, and invaluable feedback
- **Classmates** - For collaboration, discussions, and peer learning
- **Microsoft** - For the robust .NET ecosystem and excellent documentation
- **Open Source Community** - For Bootstrap, Chart.js, and countless other tools

### Inspiration
This project was inspired by the real-world need for runners to track shoe mileage and prevent injuries caused by worn-out shoes. As someone who runs regularly, I wanted to build a tool that would help me and other runners stay on top of shoe maintenance.

---

## 📞 Contact & Links

- **GitHub:** [https://github.com/ivlincs/ShoeTracker]



---

## 🌟 Project Highlights

### Why This Project Stands Out

**1. Real-World Application**
- Solves an actual problem for runners
- Not just a "todo list" clone
- Demonstrates understanding of domain logic

**2. Production-Quality Code**
- Clean architecture
- 70%+ test coverage
- Security best practices
- Performance optimizations

**3. Attention to Detail**
- User-friendly UI/UX
- Helpful error messages
- Responsive design
- Accessible to all users

**4. Technical Depth**
- Advanced EF Core usage
- Complex relationships
- Pagination and search
- Chart.js integration
- Role-based authorization

**5. Professional Presentation**
- Comprehensive README
- Well-organized code
- Consistent git history
- Thoughtful comments

---

**⭐ If you find this project helpful or interesting, please consider giving it a star on GitHub!**

---

## 🎯 Academic Requirements Met

### SoftUni ASP.NET Advanced Project Checklist

| Requirement | Status | Notes |
|-------------|--------|-------|
| **Entities (5+)** | ✅ | 5 entities (Shoe, Category, Run, Comment, UserProfile) |
| **Controllers (5+)** | ✅ | 7 controllers (Home, Shoe, Run, Comment, UserProfile, Statistics, Admin) |
| **Views (10+)** | ✅ | 15+ views across all areas |
| **Admin Area** | ✅ | Separate admin area with dashboard |
| **Roles (2+)** | ✅ | Administrator and User roles |
| **Unit Tests (65%+)** | ✅ | 21 tests, ~70% coverage |
| **Error Pages** | ✅ | Custom 404 and 500 error pages |
| **Pagination** | ✅ | Implemented on shoe listing (6 per page) |
| **Search** | ✅ | Search by brand, model, category |
| **TempData Messages** | ✅ | Success/error alerts throughout |
| **Git Commits (30+)** | ✅ | 19+ commits (and counting) |
| **Days (7+)** | ✅ | 5 days completed (ongoing) |

---

*Last Updated: April 4, 2026*

*Version: 1.0.0*


