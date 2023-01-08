# BloodDonationPortal

## About the Project
BloodDonationPortal is an ASP.NET MVC web application designed to manage blood donation teams and locations, allow users to schedule donation appointments, and display donation points on an interactive map. 

The application integrates comprehensive CRUD operations for blood donation centers, a user-facing appointment booking system, user authentication via ASP.NET Identity, bulk XML data import from external systems, and dynamic map visualization using Leaflet.js into a single, cohesive platform.

## Key Features

*   **Appointment Booking System:** Registered users can select a blood donation center from the map or list and schedule a donation appointment. The system tracks available time slots, associates appointments with specific users and centers, and allows users to view or manage their upcoming donation schedule.
*   **Dynamic Map Integration with Leaflet.js:** Blood donation points are displayed on an interactive map populated directly from the database rather than static HTML. The endpoint `BloodTeamsController.GetTeamsForMap()` exposes the locations in JSON format (TeamName, Neighbourhood, PhoneNumber, Latitude, Longitude). On the frontend (JavaScript), this JSON data is fetched asynchronously and dynamically rendered as Markers and Popups on the Leaflet.js map.
*   **BloodTeam CRUD Operations:** Administrators have access to standard Create, Read/List, Update, and Delete interfaces to manage blood donation centers. These operations are handled by the `BloodTeamsController` and persisted to the database using Entity Framework.
*   **Bulk Data Import via XML:** Using the `BloodTeamService.ImportBloodTeams(string xmlData)` method, XML-based blood donation point data from external systems can be integrated into the application. This service performs robust, namespace-agnostic parsing (using LocalName logic) and saves the records to the database in bulk.
*   **User Authentication:** Secure user registration, login processes, password policies, and account management are implemented using the OWIN and ASP.NET Identity architecture (ApplicationUser, ApplicationUserManager, ApplicationSignInManager).
*   **Informational Content:** The application includes static and dynamic content pages serving its core purpose, such as "Who Can Give Blood?", "Why Give Blood?", and "Where to Donate?".

## Technologies Used
*   **Framework:** ASP.NET MVC (.NET Framework 4.7.2)
*   **Database & ORM:** Entity Framework Code-First, SQL Server
*   **Authentication:** ASP.NET Identity, OWIN
*   **Frontend:** HTML5, CSS3, JavaScript, Bootstrap
*   **Mapping Library:** Leaflet.js

## Important Files and Directory Structure

*   `Models/BloodTeam.cs` - The core Entity model representing a blood donation team/center.
*   `Models/Appointment.cs` - The Entity model linking an `ApplicationUser` to a `BloodTeam` with a specific scheduled date and time.
*   `Models/BloodTeamDbContext.cs` - The EF DbContext class handling database communication for blood donation points and appointments.
*   `Models/IdentityModels.cs` - The class managing user details and the authentication database context (`ApplicationDbContext`).
*   `Models/BloodTeamService.cs` - The service class containing the logic for reading, parsing, and saving XML formatted data.
*   `Controllers/BloodTeamsController.cs` - The main controller responsible for CRUD operations and providing the JSON endpoint for the Leaflet.js map.
*   `Controllers/AppointmentsController.cs` - The controller managing user appointment scheduling, validation, and dashboard views.


