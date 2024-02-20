# Kanban WebAPI - Project Documentation

Welcome to the API interface for Kanban applications! This API allows you to efficiently manage tasks in a Kanban-style project board. You can create, update, delete, and organize tasks, boards, and users, making project management intuitive and easy.

## Technologies

- **ASP.NET Core 8**
- **Entity Framework Core**

## Features

### 1. **Boards Management**

- Create, update, and delete boards.
- Each board can have different names and dates for creation and last update.
- Boards can be assigned to users with different roles:
  - **Creator**: Full control over the board, including deletion of boards and resources.
  - **Admin**: Full control over the board but without deletion rights for boards.
  - **User**: Limited control (can add comments and perform task assignments).

### 2. **Lists Management**

- Cards within boards are organized into lists for better task management.
- Each list contains tasks that can be ordered using the "Position" attribute.
- Lists can have a deadline date assigned.

### 3. **Cards Management**

- Create, update, and delete cards.
- Cards contain descriptions, important dates (e.g., deadlines), and may have attachments and comments.
- Each card is assigned to one list and can have multiple assigned users.
- Users can add attachments and comments to cards for enhanced collaboration.

### 4. **User Management**

- Users have their profiles with login credentials, passwords, and avatars.
- Users can be assigned tasks and cards within the system.
- **User roles** determine access to different functionalities within the application.

### 5. **Board Users (Role Assignments)**

- Users can be assigned to boards with different roles:
  - **Creator**: Full control over the board, including deletion of boards and content.
  - **Admin**: Full control over the board but without deletion rights for boards.
  - **User**: Limited control (can add comments and perform task assignments).

### 6. **Card Users (Task Assignment)**

- Users can be assigned to specific cards within a board, allowing them to take ownership of tasks.
- Cards contain elements, which are stages of a task, and users can be assigned to these stages.

### 7. **Elements Management**

- Elements are components of cards that represent stages of a task.
- Each element has an assigned user responsible for completing the task at that stage.
- Elements can be marked as complete or incomplete based on task progress.

### 8. **Comments**

- Users can add comments to cards, facilitating communication within the team.
- Comments help in tracking decisions, providing updates, and clarifying tasks.

### 9. **Attachments**

- Users can attach files to cards for additional context or resources.
- Attachments are associated with both the card and the users, ensuring proper management.

### 10. **Task Management**

- Tasks are created and managed as part of cards.
- Tasks can be assigned to specific elements and users, ensuring each step is completed in the process.

### 11. **User Roles**

- **Member Role (User)**:
  - Can add comments and make changes to cards.
  - Can assign users to cards and elements.
- **Admin Role**:
  - Has full control over the board and its content.
  - Can manage users, create, edit, and delete boards, lists, and cards.
- **Creator Role**:
  - Inherits all the permissions of the **Admin** role.
  - Additionally, can **delete boards** and other resources (cards, lists, tasks).
  - This role provides complete control over the board and its content, making it the highest level of permission.

### 12. **Security & Data Integrity**

- The application uses **role-based access control** to ensure that users only have permissions that are appropriate for their roles.
- **Data integrity** is maintained with the use of user and role management, ensuring that only authorized users can perform sensitive operations like deleting boards, lists, or cards.

---

For more information, you can visit the front-end section supporting the API at [https://gitify.net](https://gitify.net). The API documentation is available at [https://flobird.azurewebsites.net/swagger](https://flobird.azurewebsites.net/swagger).
