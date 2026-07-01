# Modular Shop Management System

A modular shop management system built with **ASP.NET Core 8**, **Razor Pages**, **Entity Framework Core**, and **Onion Architecture** principles.

This project is designed as a learning-focused implementation of modern software architecture concepts, including **modular monolith design**, **clean separation of layers**, **dependency injection**, **SOLID principles**, and **clean code practices**.

The system follows a **Modular Monolith** architecture where each business module is independently structured into **Application**, **Domain**, and **Infrastructure** layers while sharing a single ASP.NET Core host application.

> **Note:** Shopping cart, order processing, and payment gateway integration are not implemented in the current version.

---
# Screenshots

## Public Website
### Slider
![Slider](README-Images/Screenshot%202026-06-29%20014424.png)
### Login Page
![Login](README-Images/Screenshot%202026-06-29%20023112.png)

### Category Products
![CategoryPage](README-Images/Screenshot%202026-06-29%20022801.png)

## Admin Panel

### Inventory Management
![inventoryManagement](README-Images/Screenshot%202026-06-29%20020231.png)

### Inventory History
![inventoryHistory](README-Images/Screenshot%202026-06-29%20021920.png)

### Slider Management
![SliderManagement](README-Images/Screenshot%202026-06-29%20015503.png)

### Create Slider Form
![AddSliderForm](README-Images/Screenshot%202026-06-29%20022202.png)

### Product Management
![ProductManagement](README-Images/Screenshot%202026-06-29%20021331.png)

### Discount Management
![DiscountManagement](README-Images/Screenshot%202026-06-29%20021036.png)

---

# Features

### Administration

- Product Management
- Category Management
- Inventory Management
- Discount Management
- Slider Management

### Customer

- User Registration & Authentication
- Browse Products
- Product Categories

### Technical

- Onion Architecture
- Modular Design
- Dependency Injection
- SEO-friendly URLs
- Role-based Authorization
- Authentication & Authorization

---
# Technologies

- ASP.NET Core 8
- Razor Pages
- Entity Framework Core
- SQL Server
- jQuery

---

# Architecture

The project follows **Onion Architecture** and is organized into independent modules to improve maintainability and separation of concerns.

```
Presentation (ServiceHost) ──> Application ──> Domain
                                                ▲
Infrastructure ─────────────────────────────────┘
```

The architecture emphasizes:

- Separation of Concerns
- Dependency Inversion
- SOLID Principles
- Clean Code
- Modular Design

---
# Project Structure

```
Modular-Shop-Management-System
│
├── ServiceHost                # ASP.NET Core host application
├── 0_Framework                # Shared infrastructure and common utilities
├── 01_ShopQuery               # Query models and read services
│
├── AccountManagement
│   ├── Application
│   ├── Domain
│   └── Infrastructure
│
├── ShopManagement
│   ├── Application
│   ├── Domain
│   └── Infrastructure
│
├── InventoryManagement
│   ├── Application
│   ├── Domain
│   └── Infrastructure
│
└── DiscountManagement
    ├── Application
    ├── Domain
    └── Infrastructure
```
---
# Getting Started

## Prerequisites

Before running the project, make sure you have the following installed:

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 (recommended)

## Installation

1. Clone the repository

```bash
git clone https://github.com/AmirCodesRepo/Modular-Shop-Management-System.git
```

2. Navigate to the project directory

```bash
cd Modular-Shop-Management-System
```

3. Open `Shop_Project.sln` in Visual Studio.

4. Update the SQL Server connection string in:

```
ServiceHost/appsettings.json
```

5. Apply the Entity Framework Core migrations for each business module's `DbContext` (such as the Package Manager Console or .NET CLI).

6. Set **ServiceHost** as the Startup Project.

7. Run the application.

---
# Design Decisions

This project was developed as a learning-focused implementation of a **modular software architecture** using **ASP.NET Core**.

The main objective is to explore and practice modern software architecture principles such as **Onion Architecture**, **modular monolith design**, **clean separation of concerns**, **dependency inversion**, and **maintainable code structure**, rather than implementing every possible e-commerce feature.

The system is designed as a **Modular Monolith**, where each business module is independently organized into **Application**, **Domain**, and **Infrastructure** layers. This structure helps enforce clear boundaries between modules, reduce coupling, and improve scalability while keeping the application as a single deployable unit.

---
# Planned Features

- Shopping Cart
- Checkout Process
- Order Management
- Payment Gateway Integration
- Product Reviews

---