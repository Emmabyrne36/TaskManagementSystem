# Task Management System

## Overview

To complete this challenge I utilized a .Net 8 Minimal API on the back end and a React UI with TypeScript for the front end.
Visual Studio Code is the recommeneded text editor to use to open the project.

### API

For the API, the code is divided up into different layers.

- The API layer contains the API and includes the relevant endpoints, middleware and mappers
  - There are 5 endpoints in total:
  ```text
    - GET /tasks to retrieve all tasks.
    - POST /tasks to create a new task.
    - PUT /tasks/{id} to update a specific task.
    - DELETE /tasks/{id} to delete a specific task
    - GET /tasks/{id} to retrieve a specific task
  ```
- The Data layer contains the `TaskRepository` which connects to a MongoDB database
  - There is also the `LocalRepository` which mimics a real database and stores data to a file
  - This can be used instead of `TaskRepository` if using the Mongo Docker instance is not doable
- The Domain layer contains the Task model which is used in the API and Data layer

Two test projects were added to the API, one for integration tests and one for unit tests. Each project only contains one test file to illustrate the testing structure. Each file and service would need to be tested using this pattern.

Custom middleware was added so that for each request, a message is logged to the `requests.log` file in the `logs` folder. If this folder or file does not exist, a new one is created automatically.
In the `Create` and `Update` endpoints, there is also a call to a custom logger class which will log an update to the `high_priority_requests.log` file within the `logs` folder. This file is created if it doesn't already exist if a high priority request is triggered.

### UI

For the UI, the project is divided up into clearly named folders for components, css files and pages. There are 3 pages total in this solution:

- Landing page which shows the list of tasks
  - This page also contains a colour-coded pie chart highlighting the different statuses of the created tasks
  - Lazy loading is implemented on this page. The reqeust to `GetAllTasks` in the API supports pagination. On page load, the app loads the first page (which consists of 10 items). Once you scroll to the end of the first 10 tasks, a request is made to the API to fetch the next page and appends the data to the list
- A page to create tasks which is reached by clicking the `Create` button
- A page to view existing tasks
  - Here tasks can be updated or deleted
- On the create task page as well as the task detail page, if the priority is set to "High" then a modal appears to confirm the user wants to set a high priority

For this solution, clean coding and SOLID design principles have been followed.

## Quick Start Guide

This solution can be run in several ways, the services can be run directly or using the `docker-compose.yaml` file provided.
There are demo requests in the `./API/src/TaskManagementSystem.Api/exampleRequests.http` which can be used to test the API directly.

### Docker Setup

The `docker-compose.yaml` file contains all the information needed to quickly spin up the project.
It will build and run the database, API and UI as well as seed the database with some dummy data.

To run the project using Docker, ensure Docker is running on your system, navigate to the base directory of this project and run the command `docker-compose up --build`

Once all the containers have started, you can open a browser on `http://localhost:3000` to view the UI. The API runs on `http://localhost:5000`. To view the swagger Open API spec, navigate to `http://localhost:5000/swagger/index.html` on your browser.

The `docker-compose.yaml` file uses bind mounts for the log files. This means that changes reflected within the container are also reflected in the local system. As a result, any tasks created or updated while the app is running in Docker can be easily viewed by navigating to the `./API/src/TaskManagementSystem.API/logs` folders.
Alternatively, the following commands could be used to enter into the docker container to view the files:

- `docker exec -it taskmanager-api sh`
- `cat ./logs/requests.log`
- `cat ./logs/high_priority_requests.log`

### Local Setup (without Docker)

- To run the API and UI on your local system without using Docker, navigate to `./API/src/TaskManagementSystem.API/Program.cs`
  - Comment out line 25 and uncomment line 27 to register the local database setup
    - This repository saves the data to a file on the local file system and mimics the connection to a real database
- To run the API, in one terminal, navigate to the `./API/src/TaskManagementSystem.API` folder and run `dotnet run` to run the API.
- To run the UI, navigate to `./UI/task-manager` and run `npm run dev`
  - If you run into issues stating "'vite' is not recognized as an internal or external command, operable program or batch file.", then run the command `npm install --save-dev vite` and re-run `npm run dev`
- As with the Docker setup, navigate to `http://localhost:3000` on a browser to view the UI. The API runs on `http://localhost:5000`.

Note: if using this approach, the API won't be seeded with the dummy data - the `seed_api.py` file could be run locally (if Python and other dependencies in the file are installed) to seed the API. The Url on Line 5 would need to be changed to `API_URL = "http://localhost:5000/api/tasks"`.

## To Run the Tests

To run the tests within the API project:

- Navigate to the `./API/tests/TaskManagementSystem.IntegrationTests` folder and run the command `dotnet test` to run the integration tests
- Navigate to the `./API/tests/TaskManagementSystem.UnitTests` folder and run the command `dotnet test` to run the unit tests

## Future Improvements

There are lots of changes that could be made to improve this application going forward.

- Security
  - To secure the API, OAuth with JWTs could be used
  - An identity server/endpoint could be created to issue JWTs
  - Alternatively, Cloud solutions like Cognito in AWS could be used to issue these tokens and the API could validate the token based on the issuer, audience, signing key etc.
  - Each endpoint within the API could then require authorization in order to be accessed
  - On the UI side, the Login form would be the default page, it would send a request to the login endpoint
    - This value would be stored as a cookie in the browser and used for any subsequent request to the API
    - The login state would also be stored using Redux or local storage and a wrapper component would be made to ensure anything within it could not be accessed if not authenticated
- Testing
  - There are two sample tests included with the API to demonstrate how the tests should be written. Going forward these projects would be expanded to make sure each file and execution path is tested
  - Furthermore, API and performance tests could be added using tools like `Cyprus` or `K6`
  - For UI testing, tools like `Jest` and `React Testing Library`
  - End-to-end (E2E) tools like `Cyprus` could be used to test the full solution end to end by mimicking how a user would interact with the UI, also ensuring the correct responses were returned from the API as well as verifying any changes to the UI
- To add real-time capabilities
  - Websockets like `SignalR` could be implemented on the back end to push updated to the client connected hub
  - The front end would connect to the websocket and receives the updates and performs and UI updates based on this
  - Redux could be used for state management on the UI as well

Other improvements could be

- Add an Application layer to abstract logic from the Endpoints
  - Use the CQRS pattern and have a separate command/query handler for each request type
  - Mappers and other application logic could be moved there
- Add validation using tools like `FluentValidation` to the request objects
- Filtering and sorting could be added on the API layer to move this logic out of the front end code
- The API could be versioned to maintain backwards compatibility if the API were to change over time
- A proper logging solution could be used rather than logging to a file. Tools like DataDog or Telemetry in AWS could be used
