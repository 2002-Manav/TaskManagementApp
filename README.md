ReadMe (Task Management App)

•	Project Name: TaskManagementSolution 
•	Project Title & Definition: A Task Management Application (Web Api) where users can manage tasks, assign them to team members, and track their statuses. The application will include:
1.	Back-End: .NET Core Web API
2.	Database: SQL Server (using SSMS)

•	Working & Functionalities: 
o	Swagger Integration: Swagger (OpenAPI) is used to test and document this Web API. It provides an interactive UI to execute API requests and view responses, making it easier to understand and use the API.

o	User Authentication & Role Management: Using JWT Token [Pending]

o	Task Management:
-	CRUD operations (Create, Read, Update, Delete) for tasks.
-	Users can assign tasks, update statuses (e.g., Pending, InProgress, Completed), and set due dates.
-	Tasks are linked to users with unique ids for tracking assigned tasks.

o	Database Schema:
-	Tables include Users, Tasks, and Roles with proper relationships using Entity Framework Core.
-	Tasks table includes fields like TaskId, Title, Description, AssignedUserId, DueDate, and Status.

o	Interface Segregation Principle: 
-	Separate interfaces for distinct functionalities, such as IUserRegisterService, IUserLoginService, ICreateTaskService, etc.
-	This ensures modularity and clean architecture principles.

o	Custom Services: 
-	Services for user registration, login, task creation, and management are implemented to decouple logic from the controllers. 

•	Installation & Setup
o	Requirements:
.NET Core SDK, SQL Server, Visual Studio, and Postman (optional).
o	Steps to Run: 
-	Clone the repository and update the connection string in appsettings.json.

o	Apply database migrations (in PM console): 
-	Add-Migration Initial
-	Update-Database
o	Build & run the solution.

•	Key Endpoints:
o	Authentication:
-	Register: POST /api/UserAuth/register
-	Login: POST /api/UserAuth/login
o	Tasks:
-	Get All Tasks: /api/Task
-	Create Task: POST /api/Task/CreateTask
-	View Tasks: GET /api/{taskId}
-	Update Task: PUT /api/Task/{taskId}
-	Delete Task: DELETE /api/Task/{taskId}
-	Search a Task: /api/Task/search

•	Technologies Used:
o	NET Core 9, C#, Entity Framework Core, ASP.NET Identity
o	Database: SQL Server (Visual Studio)
o	Tools: Swagger (API documentation), Postman (Api testing)

•	Future Enhancements:
o	Add advanced filtering and notifications for task updates.
o	Develop a front-end UI using React or Angular.

