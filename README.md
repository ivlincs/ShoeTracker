# ShoeTracker

Running shoe mileage tracker - ASP.NET Core MVC application

---

## Description

ShoeTracker helps runners track the mileage on their running shoes and alerts them when it's time for a replacement (at 600 km).

---

## Features

- Track multiple running shoes with categories (Road, Trail, Race, Daily)
- Log runs and automatically update shoe mileage
- Visual progress bars showing wear (0-600 km)
- Replacement alerts at 600 km 
- Statistics dashboard
- User authentication with ASP.NET Core Identity

---

##  Models
### Entities
- **Shoe** - Running shoe with brand, model, mileage, category
- **Run** - Individual run record with distance and date
- **Category** - Shoe categories (Road, Trail, Race, Daily)

### DTO
- **ShoeStatistics** - statistics for dashboard

---

## Usage

1. Register an account
2. Add running shoes with brand, model, category, and purchase date
3. Log runs for each shoe
4. Track mileage with visual progress bars
5. View statistics dashboard

---

## Database

**Connection String:** Located in `appsettings.Development.json`

**Migrations:**
- InitialCreate
- AddedCategoryEntity
