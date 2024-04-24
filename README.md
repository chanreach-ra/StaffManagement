# Staff Management Web API

This project is a simple staff management system implemented using ASP.NET Web API. It provides endpoints to perform CRUD operations on staff members.

## Features

- Add a new staff member
- Retrieve all of staffs member
- Retrieve details of a staff member
- Update details of a staff member
- Delete a staff member

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository to your local machine:

```bash
git clone https://github.com/chanreach-ra/StaffManagement.git
```
2. Navigate to the project directory:
```bash
cd staff-management-api
```
3. Run the project using the dotnet CLI:
```bash
dotnet run
```
## The API will be hosted at http://localhost:5011.

## Endpoints
POST /api/staff: Add a new staff member.
GET /api/staff/{id}: Retrieve details of a staff member by ID.
PUT /api/staff/{id}: Update details of a staff member by ID.
DELETE /api/staff/{id}: Delete a staff member by ID.
## Usage
You can interact with the API using tools like Postman or curl. Here are some example requests:

Add a new staff member:
```bash
curl -X POST -H "Content-Type: application/json" -d '{"staffID":"S0000001","fullName":"John Doe","birthday":"1990-01-01","gender":1}' http://localhost:5011/api/staff
```
Retrieve all of staffs member:
```bash
curl -X POST -H "Content-Type: application/json" -d '{"staffID":"S0000001","gender":1,"fromDate":"1990-01-01","toDate":"1990-01-01"}' http://localhost:5000/api/staff/by-criteria
```
Retrieve details of a staff member:
```bash
curl http://localhost:5000/api/staff/1
```
Update details of a staff member:
```bash
curl -X PUT -H "Content-Type: application/json" -d '{"staffID":"S0000001","fullName":"John Smith","birthday":"1990-01-01","gender":1}' http://localhost:5011/api/staff/1
```
Delete a staff member:
```bash
curl -X DELETE http://localhost:5011/api/staff/1
```
