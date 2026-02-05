# 🚀 Task Manager MVC - SaaS Edition

An advanced, modern, and responsive Task Management Application built with **.NET 10 (ASP.NET Core MVC)**. This application is designed to help teams facilitate project tracking, task assignment, and performance analytics with a premium user experience.

![Dashboard Preview](https://via.placeholder.com/800x400?text=Task+Manager+Dashboard+Preview)

## 🌟 Key Features

### 🔹 Core Management
-   **Task Management**: Create, edit, and delete tasks with rich details (deadlines, priority, estimated hours).
-   **Project Organization**: Group tasks into projects for better workflow management.
-   **User Assignment**: Assign tasks to team members and track their workload.
-   **Comments System**: Collaborate directly on tasks with threaded comments.

### 🔹 Analytics & Reporting
-   **Interactive Dashboard**: Real-time overview of system status.
-   **Visual Reports**: 
    -   **Tasks**: Doughnut chart showing status distribution (Pending vs In Progress vs Completed).
    -   **Projects**: Horizontal bar charts visualizing task load per project.
    -   **Users**: Vertical bar charts showing task assignments per user.
-   **CSV Export**: Export detailed data for tasks, projects, and users to CSV format for external analysis.

### 🔹 Advanced Functionality
-   **Secure Authentication**: Role-based access with **BCrypt** password hashing and session management.
-   **Audit History**: Detailed logs of all changes made to tasks (status changes, updates).
-   **Notifications**: System-wide notifications for important updates.
-   **Global Search**: Quickly find tasks, projects, or comments.

### 🔹 Modern UI/UX
-   **Responsive Design**: Built with **Bootstrap 5**, fully optimized for mobile and desktop.
-   **Dynamic Interactions**: **SweetAlert2** for beautiful alerts and confirmations.
-   **Visual Excellence**: Custom gradients, shadows, and **Google Fonts** (Inter & Poppins) for a premium feel.
-   **Data Visualization**: Powered by **Chart.js** 4.4 with smooth animations.

---

## 🛠️ Technology Stack

-   **Framework**: [.NET 10.0](https://dotnet.microsoft.com/) (ASP.NET Core MVC)
-   **Database**: PostgreSQL (Entity Framework Core 10)
-   **Frontend**: Razor Views (`.cshtml`), HTML5, CSS3
-   **Styling**: Bootstrap 5.3 + Custom CSS
-   **JavaScript Libraries**:
    -   [Chart.js](https://www.chartjs.org/) (Data Visualization)
    -   [SweetAlert2](https://sweetalert2.github.io/) (Modals & Toasts)
-   **Authentication**: BCrypt.Net-Next (Security)

---

## 📂 Project Structure

```bash
TaskManagerMVC/
├── Controllers/       # Logic for handling requests (Tasks, Projects, Reports, etc.)
├── Models/            # Database entities and ViewModels
├── Services/          # Business logic layer (TaskService, ReportService, AuthService, etc.)
├── Views/             # UI using Razor syntax
│   ├── Reports/       # Analytics dashboards with Chart.js
│   ├── Tasks/         # Task management interfaces
│   └── Shared/        # Layouts and reusable components
├── wwwroot/           # Static assets (CSS, JS, Images)
└── Program.cs         # App configuration and dependency injection
```

---

## 🚀 Getting Started

### Prerequisites
-   [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
-   [PostgreSQL Database](https://www.postgresql.org/)

### Installation

1.  **Clone the repository**
    ```bash
    git clone https://github.com/yourusername/TaskManagerMVC.git
    cd TaskManagerMVC
    ```

2.  **Configure Database**
    Update the `DefaultConnection` string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=TaskManagerDB;Username=your_user;Password=your_password"
    }
    ```

3.  **Run Migrations**
    Apply the database schema:
    ```bash
    dotnet ef database update
    ```

4.  **Run the Application**
    ```bash
    dotnet run
    ```
    The application will start at `https://localhost:7152` (or similar).

---

## 📸 Screenshots

### 📊 Interactive Reports
Dynamic charts allow you to visualize project progress and team workload at a glance.
*(Add your screenshot here)*

### ✅ Task Management
Clean interface for managing task lifecycles, priorities, and assignments.
*(Add your screenshot here)*

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Made with ❤️ by the Task Manager Team using .NET 10.
