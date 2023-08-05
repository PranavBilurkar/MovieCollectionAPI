# Movie Collection API

This is a .NET Framework 4.8 based web API for managing a movie collection. The API allows users to register, login, and perform CRUD operations on movie records. Users can have different roles, such as "Admin" and "User," which provide different levels of access to the movie collection.

## Table of Contents

1. [Installation and Setup](#installation-and-setup)
2. [API Endpoints](#api-endpoints)
3. [Authentication and JWT Tokens](#authentication-and-jwt-tokens)
4. [Role-Based Access](#role-based-access)
5. [Unit Tests](#unit-tests)

## Installation and Setup

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio (Version 2019 or later).
3. Build the solution to restore NuGet packages and ensure all projects build successfully.
4. Run the MovieCollection.API project. The API will be hosted on `https://localhost:44361/`.

## API Endpoints

The API provides the following endpoints:

1. `POST /api/user/register`: Register a new user.
2. `POST /api/user/login`: Login and obtain a JWT token for authentication.
3. `GET /api/user/all`: Get a list of all users (Admin role required).
4. `GET /api/movie/all`: Get a list of all movies.
5. `GET /api/movie/{id}`: Get details of a specific movie by its ID.
6. `POST /api/movie/add`: Add a new movie to the collection (User role required).
7. `PUT /api/movie/update/{id}`: Update the details of an existing movie (Admin role or movie owner required).
8. `DELETE /api/movie/delete/{id}`: Delete a movie from the collection (Admin role or movie owner required).

## Authentication and JWT Tokens

To use the API endpoints that require authentication, you need to obtain a JWT token. Follow these steps:

1. Register a new user using the `/api/user/register` endpoint.
2. Login with the registered user using the `/api/user/login` endpoint. This will return a JWT token in the response.
3. Include the JWT token in the `Authorization` header of subsequent requests with the format `Bearer {token}`.

## Role-Based Access

The API implements role-based access control (RBAC) with two roles: "Admin" and "User."

1. Admin Role: Administrators have full access to all API endpoints, including the ability to view all users and update any movie record.
2. User Role: Regular users can only perform CRUD operations on their own movie records.

## Unit Tests

The MovieCollection.Test project contains unit tests for the API. To run the tests, open the Test Explorer in Visual Studio and click "Run All Tests."

## Other Information

During development, we used Serilog for logging to a log file named "log.txt" in the project's root directory. The log records important events and errors, aiding in debugging and monitoring.

The API follows RESTful conventions, and error responses include detailed messages to help developers understand and resolve issues.

If you encounter any problems or have any questions, feel free to open an issue in the GitHub repository.

Thank you for using the Movie Collection API! Happy coding!
