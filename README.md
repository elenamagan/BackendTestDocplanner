# BackendTestDocplanner

This repository contains a .NET project with a C# backend API and a Windows Forms UI. Follow the instructions below to set up and run the project.

## Getting Started

### Prerequisites

- Windows OS
- .NET SDK 8.0
- Visual Studio (optional, but recommended)

### Cloning the Repository

First, clone the repository to your local machine. Navigate to the desired directory and run:

```
git clone https://github.com/yourusername/BackendTestDocplanner.git
```

After cloning, you will have two directories:

· `BackendTestDocplanner`
· `BackendTestDocplanner.Tests`

### Configuration

You need to configure the `appsettings.json` file in both `BackendTestDocplanner` and `BackendTestDocplanner.Tests` directories.

Update the following section in each `appsettings.json` file with a valid username and password for the service https://draliatest.azurewebsites.net/api/:

```
"SlotService": {
  "Username": "username",
  "Password": "password"
}
```

### Running the Application

#### Visual Studio

1. Open the solution file `BackendTestDocplanner.sln` in Visual Studio.
2. Set the `BackendTestDocplanner` project as the startup project.
3. Click the "Start" button (or press `F5`) to run the application.

#### Command Line

Alternatively, you can use the .NET CLI to run the application.

1. Navigate to the `BackendTestDocplanner` directory:

```
cd BackendTestDocplanner
```

2. Restore the dependencies

```
dotnet restore
```

3. Build the project

```
dotnet build
```

4. Run the project

```
dotnet run
```

### Accessing the API and UI

The API is deployed by default at `https://localhost:5001`. You can access the Swagger UI to interact with the API at: [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)

Additionally, the application provides a simple Windows Forms UI that will be launched when the application runs.

### Running Tests

To run the tests, you need to configure the `appsettings.json` file in the `BackendTestDocplanner.Tests` directory as described in the configuration section.

#### Visual Studio

1. Open the Test Explorer in Visual Studio.
2. Click "Run All" to execute the tests.

#### Command Line

Alternatively, you can use the .NET CLI to run the application.

1. Navigate to the `BackendTestDocplanner.Tests` directory:

```
cd BackendTestDocplanner.Tests
```

2. Run the tests:

```
dotnet test
```

## Notes

* The application must be run on a Windows OS due to the Windows Forms UI component.
* The API base URL can be modified in the appsettings.json file of the BackendTestDocplanner project if necessary.

Feel free to reach out at elenamagan.14@gmail.com if you encounter any issues or have any questions.