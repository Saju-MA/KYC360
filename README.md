# KYC360 - One-Time Secret Link Generator

## Overview

This is a web application built using ASP.NET Core that enables administrators to generate unique, one-time-use links for users. The first time a user accesses the link, they receive a secret message. Any subsequent attempts display a message stating that the link is no longer available.

This project was developed as part of a C# Developer technical assessment.

## Features

- **One-Time Access Links**: A unique link is generated for each user, and it can only be used once.
- **Admin Page**: Allows an admin to generate and copy a user-specific link.
- **API Endpoint**: Provides a JSON-based API for generating links programmatically.
- **User Validation**: Ensures the username meets constraints (max length: 50, no special characters).
- **Configurable Link Expiry** (Stretch Goal): Links can be set to expire after a certain time.
- **Limited Clicks Per Link** (Stretch Goal): A link can be configured to allow multiple accesses before expiring.
- **Optimized for High Volume** (Stretch Goal): Designed to efficiently handle a large number of link generations (1M+ per month) with minimal storage overhead.

## Architecture & Approach

The project follows a modular and scalable architecture to ensure maintainability and efficiency:

- **Microservices Approach**: The application is divided into multiple projects (`KYC360.Services` and `KYC360.Web`) to follow a microservices architecture. The services communicate via a RESTful API, ensuring separation of concerns and modularity.
- **Separation of Concerns**: Dividing the application into distinct projects allows for better maintainability and unit testing.
- **Scalability**: Using ASP.NET Core ensures the solution can scale as needed, accommodating high traffic volumes.
- **Efficient Data Storage**: The design optimizes storage by only retaining necessary metadata instead of full request logs, reducing database load.
- **Security Considerations**: Unique tokens are used to secure each generated link, ensuring safe access control.
- **Configurability**: Expiry settings and click limits can be adjusted dynamically, making the solution flexible for different use cases.

## Installation & Setup

1. **Clone the repository**:
   ```sh
   git clone https://github.com/Saju-MA/KYC360.git
   cd KYC360
   ```
2. **Set up dependencies**:
   - Ensure you have .NET 6+ installed.
   - Update the connection string in `appsettings.json` if using a database.
3. **Run database migrations**:
   - Open the **Package Manager Console** in Visual Studio.
   - Run the following command to create the necessary tables:
     ```sh
     Update-Database
     ```
4. **Run the application**:
   ```sh
   dotnet run --project KYC360.Services
   dotnet run --project KYC360.Web
   ```

## Future Enhancements

- Implement authentication for the admin panel.
- Optimize storage using distributed cache solutions.
- Implement detailed logging and monitoring.


