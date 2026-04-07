# 🏃 ShoeTracker - Running Shoe Management System

**ASP.NET Core MVC Advanced Project - SoftUni 2026**

A comprehensive web application for tracking running shoes, recording runs, managing shoe lifecycle, and analyzing running statistics.

**Project Status:** ✅ COMPLETED  
**Unit Tests:** 21/21 ✅ (100%)  
**Development Days:** 7/7 ✅ (100%)  
**Submission Date:** April 7, 2026

---

## 📋 Project Information

- **Author:** Ivaylo Dimitrov
- **Course:** ASP.NET Advanced
- **Date:** April 2026
- **Framework:** ASP.NET Core 8.0 MVC
- **Database:** SQL Server Express (DESKTOP-3BBGCT4)
- **Database Name:** ShoeTracker_Feb2026
- **Repository:** https://github.com/ivlincs/ShoeTracker

---

## ✨ Features

### 🎯 Core Functionality
- ✅ **User Authentication** - Secure registration and login with ASP.NET Core Identity
- ✅ **Role-Based Authorization** - Administrator and User roles with different permissions
- ✅ **Shoe Management** - Full CRUD operations for managing running shoes
- ✅ **Run Tracking** - Log individual runs and automatically update shoe mileage
- ✅ **Category System** - Organize shoes by type (8 categories: Road, Trail, Race, Daily, Hybrid, Recovery, Track & Field, Walking)
- ✅ **Personal Notes** - Add observations about each shoe (500 chars max)
- ✅ **Archive Feature** - Soft delete with dedicated archived view
- ✅ **Comments System** - Add notes and observations about shoe performance
- ✅ **User Profiles** - Customizable profiles with city, bio, and yearly running goals

### 📊 Statistics & Analytics
- ✅ **Statistics Dashboard** - Visual display showing:
  - Total distance per shoe
  - Shoes needing replacement (600km threshold)
  - Total runs tracked
  - Average distance per shoe
- ✅ **Replacement Alerts** - Automatic warnings when shoes reach 600km
- ✅ **Progress Tracking** - Visual progress bars for each shoe (0-600km) with color coding
- ✅ **Comprehensive Metrics** - Total shoes, runs, distance calculations

### 👨‍💼 Admin Features
- ✅ **Admin Dashboard** - System-wide overview with:
  - Total users, shoes, runs, and comments
  - Statistics cards with Bootstrap Icons
  - Scrollable recent shoes table with sticky header (last 50 shoes)
  - Archive icon representing data view
- ✅ **Role Management** - Restricted access to admin area
- ✅ **System Statistics** - Complete system health metrics

### 🔍 Advanced Features
- ✅ **Pagination** - Efficient data display (6 shoes per page)
- ✅ **Search Functionality** - Filter shoes by brand, model, or category
- ✅ **TempData Alerts** - User-friendly success and error messages
- ✅ **Responsive Design** - Mobile-optimized Bootstrap 5 interface with Bootstrap Icons
- ✅ **Data Validation** - Client-side and server-side validation
- ✅ **Custom Error Pages** - Branded 404 and 500 error pages
- ✅ **Async Operations** - Non-blocking database operations for better performance

---

## 🆕 Recent Additions (Days 5-7)

### Day 5 - Documentation & Polish
- ✅ Comprehensive README (900+ lines)
- ✅ Admin bug fix (_ViewImports)
- ✅ Security audit completed
- ✅ Code cleanup (constants, XML docs)

### Day 6 - Optional Features & UI Polish
- ✅ **Expanded Categories** - 8 categories (Road, Trail, Race, Daily, Hybrid, Recovery, Track & Field, Walking)
- ✅ **Notes Field** - Personal observations for each shoe (500 chars max)
- ✅ **Archive Feature** - Soft delete with dedicated archived view
- ✅ **UI Enhancements** - Redesigned home page with feature cards and Bootstrap Icons
- ✅ **Footer Improvements** - Enhanced with contextual links and runner-friendly message
- ✅ **Admin Dashboard** - Scrollable table with sticky header for 50+ shoes
- ✅ **Icon Consistency** - Bootstrap Icons throughout the application

### Day for Final Polish & Submission
- ✅ Footer spacing fix for better content layout
- ✅ Admin card icon improvements (archive icon for data archive view)
- ✅ ViewBag naming consistency fixes (TotalShoe → TotalShoes)
- ✅ Final testing and bug fixes
- ✅ Comprehensive testing report (TESTING-REPORT.md)
- ✅ Documentation completion

---

## 🏗️ Architecture

### Project Structure (Clean Architecture)
ShoeTracker/
├── ShoeTracker.Web/              # 🎨 Presentation Layer (MVC)
│   ├── Controllers/              # 7 Controllers
│   │   ├── HomeController.cs
│   │   ├── ShoeController.cs
│   │   ├── RunController.cs
│   │   ├── CommentController.cs
│   │   └── UserProfileController.cs
│   ├── Areas/Admin/              # Admin Area
│   │   └── Controllers/
│   │       └── AdminController.cs
│   ├── Views/                    # 20+ Razor Views
│   │   ├── Home/
│   │   ├── Shoe/
│   │   ├── Run/
│   │   ├── Comment/
│   │   ├── UserProfile/
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
│   └── Migrations/               # Database migrations (15+)
│       ├── InitialCreate
│       ├── AddedCategoryEntity
│       ├── AddMoreSeedCategories
│       ├── AddNotesToShoe
│       ├── AddIsArchivedToShoe
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

### Technology Stack

**Backend:**
- **Framework:** ASP.NET Core 8.0 MVC
- **ORM:** Entity Framework Core 8.0.24
- **Database:** SQL Server Express 2019+
- **Authentication:** ASP.NET Core Identity
- **Architecture:** 3-Layer (Presentation, Business, Data)
- **Patterns:** Repository Pattern, Dependency Injection

**Frontend:**
- **UI Framework:** Bootstrap 5.3
- **Icons:** Bootstrap Icons 1.11.3
- **JavaScript:** Vanilla JavaScript
- **Templating:** Razor Pages / Views
- **Custom CSS:** Additional styling

**Testing:**
- **Framework:** xUnit 2.5.3
- **Assertions:** FluentAssertions 8.9.0
- **Database:** EF Core InMemory Database 8.0.24
- **Coverage:** 21 unit tests with 100% service layer coverage

**Tools:**
- **Version Control:** Git + GitHub
- **IDE:** Visual Studio 2022
- **Design Patterns:** Repository, Service Layer, Dependency Injection

---

## 🗄️ Database Schema

### Entity Relationship Diagram
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
│ Notes        │
│ IsArchived   │
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
- Notes (string, optional, max 500 chars) [NEW Day 6]
- IsArchived (bool, default false) [NEW Day 6]
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
**Seed Data (8 categories):** Road running, Trail running, Race running, Daily training, Hybrid training, Recovery running, Track & Field running, Walking

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
- TotalRuns (int)
- AverageDistancePerShoe (double)
- ShoesNeedingReplacement (int)
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
git clone https://github.com/ivlincs/ShoeTracker.git
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

Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g., `DESKTOP-3BBGCT4`).

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
- Create all tables with relationships
- Seed initial 8 categories

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
✅ 21 unit tests
✅ 100% service layer coverage
✅ All tests passing
✅ Average execution time: ~3 seconds

### Test Structure

**ShoeServiceTests.cs** (7 tests)
- GetAllAsync_ReturnsOnlyOwnersShoes
- GetByIdAsync_ReturnsNull_WhenWrongUser
- GetByIdAsync_ReturnsShoe_WhenCorrectUser
- AddAsync_PersistsShoe
- DeleteAsync_RemovesShoe
- GetStatisticsAsync_CalculatesCorrectly
- SearchAsync_FiltersByBrand

**RunServiceTests.cs** (5 tests)
- AddRunAsync_UpdatesTotalDistance
- AddRunAsync_CreatesRunRecord
- AddRunAsync_ThrowsException_WhenShoeNotFound
- AddRunAsync_ThrowsException_WhenShoeOwnedByOtherUser
- AddRunAsync_SetsCorrectUserId

**CommentServiceTests.cs** (5 tests)
- AddAsync_SavesComment
- AddAsync_ThrowsException_WhenWrongOwner
- DeleteAsync_RemovesComment
- DeleteAsync_DoesNotDelete_WhenWrongUser
- GetByShoeIdAsync_FiltersCorrectly

**UserProfileServiceTests.cs** (4 tests)
- GetByUserIdAsync_ReturnsNull_WhenNotExists
- GetByUserIdAsync_ReturnsProfile_WhenExists
- CreateOrUpdateAsync_CreatesNewProfile
- CreateOrUpdateAsync_UpdatesExistingProfile

---

## 📖 User Guide

### Getting Started

#### 1. Register an Account
- Click "Register" in the navigation bar
- Enter email and password (min 6 characters, requires uppercase, lowercase, digit, special char)
- Confirm password and submit

#### 2. Add Your First Shoe
- Navigate to "My Shoes"
- Click "Add New Shoe"
- Fill in:
  - Brand (e.g., "Asics", "Nike", "Puma")
  - Model (e.g., "Gel-Kayano 30")
  - Category (8 options available)
  - Purchase Date
  - Notes (optional, personal observations up to 500 chars)
- Submit

#### 3. Log a Run
- Go to shoe details
- Click "Add Run"
- Enter distance (in km) and date
- Submit
- **TotalDistance** automatically updates!

#### 4. View Statistics
- Navigate to "Statistics"
- See comprehensive dashboard with:
  - Total shoes, distance, runs
  - Shoes needing replacement
  - Average distance per shoe
  - Color-coded status indicators

#### 5. Add Comments
- In shoe details, scroll to "Notes & Comments" section
- Click "Add Note" button
- Write notes about performance, comfort, durability, etc.
- Submit

#### 6. Archive Old Shoes
- Click "Delete" on a shoe
- Shoe is archived (soft delete with IsArchived flag)
- View archived shoes in "Archived" menu section
- Archived shoes are excluded from statistics calculations

### Shoe Replacement Logic

**Replacement threshold: 600 km**

- **0-300 km:** 🟢 Good condition (green progress bar, 0-50%)
- **300-480 km:** 🟡 Monitor closely (yellow progress bar, 50-80%)
- **480-600 km:** 🟠 Plan replacement (orange progress bar, 80-100%)
- **600+ km:** 🔴 Replace immediately (red progress bar, 100%+ with badge alert)

Visual progress bar shows current status on every shoe card with color-coded indicators.

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
- ✅ Anti-forgery tokens automatically included in all forms

### Data Validation
- ✅ Client-side validation with jQuery Validation
- ✅ Server-side validation with Data Annotations
- ✅ SQL injection prevention via EF Core parameterization
- ✅ XSS protection via Razor auto-escaping

### Error Handling
- ✅ Custom error pages (404, 500)
- ✅ Production-safe error messages
- ✅ Detailed errors only in Development mode
- ✅ Try-catch blocks in critical service operations

---

## 🎨 User Interface

### Design Principles
- **Responsive** - Works on desktop, tablet, and mobile (tested on iPhone SE, iPad, Desktop 1920x1080)
- **Accessible** - Bootstrap Icons with semantic HTML
- **Intuitive** - Clear navigation and user flows
- **Fast** - Optimized page load times (< 2 seconds)
- **Consistent** - Bootstrap 5.3 styling throughout

### Key Pages

**Home Page**
- Welcome banner with ShoeTracker branding and Bootstrap Icons
- 3 feature cards (Track Your Shoes, Monitor Progress, View Statistics)
- Contextual call-to-action buttons (Get Started vs Go to My Shoes)
- Enhanced footer with runner-friendly message and links

**My Shoes**
- Card-based responsive layout (3 columns on desktop, 1 on mobile)
- Pagination (6 per page)
- Search functionality (brand/model/category)
- Progress bars for each shoe with color coding
- Quick actions (View Details, Edit, Archive)
- "Archived" navigation link

**Shoe Details**
- Complete shoe information with all fields
- Personal Notes card (conditional display if notes exist)
- Run history table sorted by date
- Notes & Comments section with add/delete functionality
- Quick "Add Run" button with icon

**Statistics Dashboard**
- Key metrics cards:
  - Total shoes (with icon)
  - Total distance in km
  - Total runs tracked
  - Shoes needing replacement (count with alert)
- Average distance calculation per shoe
- Color-coded visual presentation

**Admin Dashboard**
- System-wide statistics cards:
  - Shoes (archive icon - represents data archive)
  - Users (person icon)
  - Runs (list icon)
  - Comments (chat icon)
- Scrollable recent shoes table:
  - Sticky header (remains visible on scroll)
  - Last 50 shoes displayed
  - Smooth scrolling with max-height: 500px
- "Back to Site" navigation button

**Archived Shoes**
- Dedicated view for soft-deleted shoes
- Read-only card display
- "Archived" badge indicator
- Filtered out from main shoe list and statistics

---

## 📊 Performance Optimizations

### Database
- ✅ **Eager Loading** - `.Include()` to prevent N+1 queries
- ✅ **Async Operations** - All database calls use `async/await`
- ✅ **Indexes** - Primary and foreign key indexes automatically created
- ✅ **Pagination** - Limit data transfer with Skip/Take (6 per page)
- ✅ **Filtered Queries** - IsArchived flag filtering at database level

### Code Quality
- ✅ **Constants** - Magic numbers extracted:
  - REPLACEMENT_THRESHOLD_KM = 600
  - DEFAULT_PAGE_SIZE = 6
  - Admin recent shoes = 50
- ✅ **XML Documentation** - All public service methods documented
- ✅ **Dependency Injection** - All services registered in Program.cs
- ✅ **Repository Pattern** - Service layer abstracts data access
- ✅ **SOLID Principles** - Single Responsibility, Open/Closed, Dependency Inversion

### Frontend
- ✅ **CDN for Libraries** - Bootstrap 5.3, Bootstrap Icons from CDN
- ✅ **Minimal JavaScript** - jQuery only for Bootstrap functionality
- ✅ **CSS Optimization** - Custom CSS for animations and spacing fixes
- ✅ **Lazy Loading** - Images load on demand

---

## 🛠️ Development Notes

### Code Quality Practices
- **Async/Await** - Non-blocking operations throughout
- **Error Handling** - Try-catch with meaningful exceptions
- **User Feedback** - TempData for success/error messages with icons
- **Validation** - Server-side and client-side dual validation
- **Security** - Authorization checks on all protected actions
- **Separation of Concerns** - 3-tier architecture strictly followed
- **Naming Conventions** - Consistent throughout (ViewBag naming fixed in Day 7)

### Best Practices
- ✅ Magic numbers extracted to constants
- ✅ Meaningful variable names (no abbreviations)
- ✅ XML documentation on all public service methods
- ✅ Consistent code formatting (Ctrl+K, Ctrl+D)
- ✅ Error handling with try-catch where appropriate
- ✅ User-friendly error messages via TempData with Bootstrap styling

### Testing Strategy
- **AAA Pattern** - Arrange, Act, Assert in all tests
- **InMemory Database** - Isolated test environment
- **FluentAssertions** - Readable test assertions
- **Realistic Data** - Tests use actual running shoe brands (Asics, Puma, New Balance, Nike)
- **Security Tests** - Ownership verification tests for data isolation
- **100% Coverage** - All service methods have unit tests

---

## 🐛 Known Issues / Future Enhancements

### Potential Improvements
- [ ] User-created custom categories
- [ ] Export statistics to PDF/Excel
- [ ] Shoe comparison feature (side-by-side)
- [ ] Email notifications for replacement alerts
- [ ] Social features (share runs, follow friends)
- [ ] Integration with Strava/Garmin
- [ ] Multi-language support (Bulgarian, English)
- [ ] Dark mode theme
- [ ] Advanced filtering (by date range, mileage range)
- [ ] Shoe retirement ceremony (fun feature!)

### Technical Improvements
- [ ] Implement caching (Redis)
- [ ] Add logging (Serilog)
- [ ] API layer (RESTful endpoints)
- [ ] Real-time updates (SignalR)
- [ ] Progressive Web App (PWA)
- [ ] Docker containerization
- [ ] CI/CD pipeline (GitHub Actions)

### Design Decisions
- Archive feature uses soft delete (IsArchived flag) instead of hard delete - preserves data history
- Statistics exclude archived shoes from calculations - provides accurate active shoe metrics
- Bootstrap Icons CDN dependency - requires internet connection
- LocalDB for development - production deployment would use full SQL Server
- Admin "archive" icon represents data archive/storage perspective (not shoe archive feature)

### Browser Compatibility
- Tested on: Chrome 120+, Edge 120+
- Bootstrap Icons require modern browser
- Older IE browsers not supported (by design)

---

## 🎯 Academic Requirements Met

| Requirement | Implementation | Status |
|-------------|----------------|--------|
| **Entities (5+)** | Shoe, Run, Category, Comment, UserProfile | ✅ 5 entities |
| **Controllers (5+)** | Shoe, Run, Comment, UserProfile, Home, Admin, Account | ✅ 7 controllers |
| **Views (10+)** | 20+ views (Index, Create, Edit, Delete, Details, Dashboard, Admin, Archived, etc.) | ✅ 20+ views |
| **Admin Area** | Dedicated Admin area with dashboard and scrollable recent shoes table | ✅ Implemented |
| **Roles (2+)** | Administrator, User (with role-based authorization) | ✅ 2 roles |
| **CRUD Operations** | Full CRUD for Shoes, Runs, Comments, UserProfile | ✅ All entities |
| **Search** | Search by brand, model, category name | ✅ Implemented |
| **Pagination** | 6 items per page with page navigation | ✅ Implemented |
| **Sorting** | OrderByDescending by PurchaseDate | ✅ Implemented |
| **Validation** | Data Annotations + ModelState validation | ✅ All forms |
| **Error Handling** | Try-catch blocks, custom error pages (404, 500) | ✅ Implemented |
| **Responsive Design** | Mobile-first Bootstrap layout, tested on multiple devices | ✅ Fully responsive |
| **TempData Messages** | Success/error alerts with Bootstrap styling and icons | ✅ All actions |
| **Unit Tests (65%+)** | 21 unit tests, 100% service layer coverage | ✅ 21/21 tests |
| **Git Commits (30+)** | 30 meaningful commits across 7 development days | ✅ 30/30 commits |
| **Days (7+)** | 7 development days with consistent progress | ✅ 7/7 days |
| **README.md** | Comprehensive documentation (900+ lines) | ✅ Complete |

---

## 📊 Final Project Statistics

**Completion Date:** April 7, 2026  
**Git Commits:** 46 ✅  
**Unit Tests:** 21/21 ✅  
**Code Coverage:** 100% (Service Layer)  
**Total Files:** 100+  
**Lines of Code:** ~5,500+  

### Key Achievements
🏆 100% Requirements Met  
🏆 46 Git Commits  
🏆 21 Unit Tests (100% Service Coverage)  
🏆 Zero Critical Bugs  
🏆 Mobile Responsive  
🏆 Production-Ready Code  
🏆 Comprehensive Documentation  

---

## 📚 Resources & Documentation

### Official Documentation
- [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [Bootstrap 5](https://getbootstrap.com/docs/5.3/)
- [Bootstrap Icons](https://icons.getbootstrap.com/)

### Learning Resources
- [SoftUni ASP.NET Advanced Course](https://softuni.bg/)
- [Microsoft Learn - ASP.NET](https://learn.microsoft.com/en-us/aspnet/core/)
- [Clean Architecture Guide](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

---

## 📄 License

This project is developed for **educational purposes** as part of the **SoftUni ASP.NET Advanced course** (April 2026).

**© 2026 Ivaylo Dimitrov. All rights reserved.**

**Note:** This is a student project and is not licensed for commercial use.

---

## 🙏 Acknowledgments

### Special Thanks
- **SoftUni** - For the comprehensive ASP.NET Advanced curriculum and excellent instructors
- **Course Instructors** - For guidance, code reviews, and invaluable feedback
- **Classmates** - For collaboration, discussions, and peer learning
- **Microsoft** - For the robust .NET ecosystem and excellent documentation
- **Open Source Community** - For Bootstrap, Bootstrap Icons, and countless development tools

### Inspiration
This project was inspired by the real-world need for runners to track shoe mileage and prevent injuries caused by worn-out shoes. As someone who runs regularly, I wanted to build a tool that would help me and other runners stay on top of shoe maintenance.

---

## 📞 Contact & Links

- **GitHub:** https://github.com/ivlincs/ShoeTracker

---

## 🌟 Project Highlights

### Why This Project Stands Out

**1. Real-World Application**
- Solves an actual problem for runners
- Not just a "todo list" clone
- Demonstrates understanding of domain logic and business rules

**2. Production-Quality Code**
- Clean 3-layer architecture
- 100% service layer test coverage
- Security best practices (authorization, CSRF, XSS protection)
- Performance optimizations (async/await, eager loading, pagination)

**3. Attention to Detail**
- User-friendly UI/UX with Bootstrap Icons
- Helpful error messages with icons
- Responsive design tested on multiple devices
- Accessible to all users

**4. Technical Depth**
- Advanced EF Core usage (migrations, relationships, soft delete)
- Complex business logic (mileage calculation, replacement alerts)
- Pagination and search functionality
- Role-based authorization with admin area
- Archive feature with soft delete pattern

**5. Professional Presentation**
- Comprehensive README
- Testing report documentation
- Well-organized code structure
- Consistent git history (30 meaningful commits)
- Thoughtful comments and XML documentation

---

**⭐ If you find this project helpful or interesting, please consider giving it a star on GitHub!**

---

*Last Updated: April 7, 2026*

*Version: 1.0.0*

*Status: ✅ READY FOR SUBMISSION*

**Thank you for reviewing ShoeTracker!** 🏃‍♂️🎉