# Movie Collection API

This is a .NET Framework 4.8 based REST API for managing a movie collection. The API allows users to register, login, and perform CRUD operations on movie records. Users can have different roles, such as "Admin" and "User," which provide different levels of access to the movie collection.

## Table of Contents

1. [Installation and Setup](#installation-and-setup)
2. [Features](#features)
3. [API Endpoints](#api-endpoints)
4. [Authentication and JWT Tokens](#authentication-and-jwt-tokens)
5. [Role-Based Access](#role-based-access)
6. [Unit Tests](#unit-tests)
7. [Postman Collection](#postman-collection)
8. [User Interface](#user-interface)
9. [Motivation Behind Choices](#motivation-behind-choices)

## Installation and Setup

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio (Version 2019 or later).
3. Build the solution to restore NuGet packages and ensure all projects build successfully. I suggest you to build API project first to create packages.
4. Run the MovieCollection.API project by making it as a startup project from Solution - properties. The API will be hosted on `https://localhost:44361/`.
Note - You might get warning regarding the conflicts of "Unity.Abstractions", to fix that click on warning and click Yes.


 ## Features
1. User registration and login functionality with JWT authentication.
2. Role-based access control (RBAC) to restrict certain endpoints based on user roles.
3. CRUD operations for managing movie records.
4. In-memory list used for storing movie data.

## API Endpoints

The API provides the following endpoints:

1. `POST /api/user/register`: Register a new user.
2. `POST /api/user/login`: Login and obtain a JWT token for authentication.
3. `GET /api/user/all`: Get a list of all users (For Testing purpose).
4. `GET /api/movie/all`: Get a list of all movies.
5. `GET /api/movie/{id}`: Get details of a specific movie by its ID.
6. `POST /api/movie/add`: Add a new movie to the collection.
7. `PUT /api/movie/update/{id}`: Update the details of an existing movie (Admin role or movie owner required).
8. `DELETE /api/movie/delete/{id}`: Delete a movie from the collection (Admin role or movie owner required).
9. `GET /api/movies/search?keyword={keyword}`: Search for movies by keyword.

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

The `MovieCollection.Test` project contains unit tests for the API. To run the tests, open the Test Explorer in Visual Studio and click "Run Tests."

## Postman Collection
I have provided a Postman collection (MovieCollectionAPI.postman_collection.json) in this repository. You can import this collection into Postman to easily send requests to the MovieCollection.API endpoints and explore its functionalities. To use the Postman collection, follow these steps:

Open Postman:-
1. Click on the "Import" button and choose the MovieCollectionAPI.postman_collection.json file from the repository.
2. Once imported, you will see a collection named "MovieCollectionAPI" in Postman.
3. You can now explore the different endpoints and send requests to the API directly from Postman

## User Interface

The `MovieCollection.UI` project provides a user-friendly web interface for managing movie collections. The UI allows users to:

View the list of all movies in the collection.
View details of a specific movie.
Add a new movie to the collection (authentication required).
Update an existing movie (authentication required).
Delete a movie from the collection (authentication required).
Search for movies by keyword.

## Motivation Behind Choices

1. **Serilog**: Chose Serilog for logging due to its simplicity, flexibility, structured events and performance. It offers easy log message handling with various sinks like files and databases, aiding scalability and monitoring.

2. **Unity Container DI & Separation of Concerns**: Implemented IoC with Unity for DI to promote loosely coupled and testable code. Unity's configuration process allows easy dependency integration, enhancing code quality. Additionally, I followed the principle of separation of concerns for code modularity and maintenance ease.

3. **JWT Authentication**: Chose JWT for secure communication between client and server. Its stateless nature avoids server-side storage, making it suitable for modern web apps.

4. **Custom Exception Handling**: Implemented custom exception handling for meaningful error responses, ensuring a better user experience and efficient debugging.

5. **Role-Based Access Control**: Implemented RBAC to restrict endpoint access based on user roles, maintaining data security.

6. **Unit Testing with MSTest**: Used MSTest to ensure code quality and identify bugs early in development.

7. **Dockerization**: Added Dockerfile for containerization, providing a consistent and isolated environment for deployment.

8. **Separation of Concerns**: Followed this principle for code modularity and maintenance ease.

9. **Code Documentation**: Emphasized code documentation for better understanding and collaboration among developers.

10. **Monolithic Architecture**: As a single, cohesive unit; developing, testing, and deploying is generally more straightforward. Easier to debug and can be efficient for small to medium-sized applications with low complexity. 

These choices contribute to a robust, secure, and maintainable MovieCollectionAPI, providing an efficient user experience and smooth movie data handling.

## Other Information

During development, I used Serilog for logging to a log file named `log.txt` at the absolute path `@"C:\Logs\log.txt"`. The log records important events and errors, aiding in debugging and monitoring.

To interact with the MovieCollectionAPI and test its functionalities, you can use Postman, a powerful API testing tool. Postman provides an intuitive interface for sending various HTTP requests, including GET, POST, PUT, and DELETE, to the API endpoints.

The API follows RESTful conventions, and error responses include detailed messages to help developers understand and resolve issues.

If you encounter any problems or have any questions, feel free to open an issue in the GitHub repository.

Thank you for using the Movie Collection API! Happy coding!
