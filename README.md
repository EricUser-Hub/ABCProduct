# ABCProduct
A simple API for product management

# Steps to execute
- Go to the root folder where the solution is to execute the following command line.
  - dotnet run --project .\Product.API\Product.API.csproj /p:EnvironmentName=Development
- Access Swagger : https://localhost:5001/swagger/index.html or http://localhost:5000/swagger/index.html

Can also :
- Compile
    - Go to the root folder where the solution is.
    - Execute command : dotnet build
- To have Swagger to check the API, be sure to set your environment to "Development"
    --set ASPNETCORE_ENVIRONMENT=Development
- Start Rest APi by executing the "Product.API.exe" in the bin folder (Exemple : APath..\ABCProduct\Product.API\bin\Debug\net8.0\Product.API.exe)
- Access Swagger : https://localhost:5001/swagger/index.html or http://localhost:5000/swagger/index.html
