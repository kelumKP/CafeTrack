# Café Employee Management System

## Overview

This application is designed to manage cafés and employees, allowing users to perform CRUD operations on both entities. It is built using a backend developed in .NET 8.x with an RDMS database and a frontend using React JS, along with Material-UI and Aggrid for the user interface.

## Backend

The backend handles the following functionalities:

1. **Employee Management**:
   - Store employee data such as ID, name, email, phone number, and gender.
   - Handle employee relationships with cafés, including their start dates.

2. **Café Management**:
   - Manage café information like name, description, logo, and location.
   - Allow CRUD operations on cafés.

3. **Endpoints**:
   - `GET /cafes?location=<location>`: Retrieve a list of cafés, optionally filtered by location, sorted by the number of employees.
   - `GET /employees?cafe=<café>`: Retrieve employees working at a specific café, sorted by the number of days worked.
   - `POST /cafe`: Create a new café.
   - `POST /employee`: Create a new employee and associate them with a café.
   - `PUT /cafe`: Update an existing café's details.
   - `PUT /employee`: Update an existing employee's details and their café association.
   - `DELETE /cafe`: Delete a café and its associated employees.
   - `DELETE /employee`: Delete an employee.

4. **Database**:
   - The database contains tables for employees and cafés, with a relationship between them.
   - Each employee can only be associated with one café.

### Setup Instructions

#### Prerequisites

- .NET 8.x or higher
- An RDMS (MSSQL, PostgreSQL, MySQL, etc.)

#### Database Setup

1. Create the database based on the schema provided in the backend project.
2. Seed the database with initial data for testing purposes.

#### Running the Backend

1. Clone the repository.
2. Navigate to the backend folder.
3. Restore the NuGet packages: `dotnet restore`
4. Run the application: `dotnet run`
5. The API will be available at `http://localhost:5000`.

---

## Frontend

The frontend is a React-based application for managing cafés and employees. It uses Material-UI or Antd for styling and Aggrid for displaying tables.

### Setup Instructions

#### Prerequisites

- Node.js (v16.x or higher)
- npm or yarn

#### Running the Frontend

1. Clone the repository.
2. Navigate to the frontend folder.
3. Install dependencies: `npm install` or `yarn install`
4. Run the application: `npm start` or `yarn start`
5. The frontend will be available at `http://localhost:3000`.

### Features

- **Café Page**:
  - Display a list of cafés with columns for logo, name, description, number of employees, and location.
  - Option to filter cafés by location.
  - View employees associated with a café by clicking on the café.
  - Options to edit or delete cafés.

- **Employee Page**:
  - Display a list of employees with details such as employee ID, name, email, phone number, days worked, and café name.
  - Options to edit or delete employees.

- **Add/Edit Café Page**:
  - A form to create or edit a café, including validations for the name, description, logo, and location.

- **Add/Edit Employee Page**:
  - A form to create or edit an employee, with validation for name, email, phone number, gender, and café assignment.

### Libraries Used

- **React**: JavaScript library for building user interfaces.
- **Aggrid**: For displaying data tables.
- **Material-UI/Antd**: CSS framework for styling.
- **Redux Form**: To handle forms and validations.

---

## Contribution

1. Fork the repository.
2. Create a new branch for your changes.
3. Make your changes and commit them.
4. Push the changes to your forked repository.
5. Create a pull request.

## UI 
Landing Page
![Landing Page](.//Frontend/cafe-employee-manager/photos/landing_page.png)

Add New Cafe Page
![Add New Cafe Page](.//Frontend/cafe-employee-manager/photos/add_new_cafe.png)

Update Cafe Page
![Update Cafe Page](.//Frontend/cafe-employee-manager/photos/update_cafe_page.png)

Employee Page
![Employee Page](.//Frontend/cafe-employee-manager/photos/employee_page.png)

Add New Employee Page
![Add New Employee Page](.//Frontend/cafe-employee-manager/photos/add_new_employee.png)

Update Employee Page
![Update Employee Page](.//Frontend/cafe-employee-manager/photos/update_employee.png)
---

## License

This project is licensed under the MIT License.

