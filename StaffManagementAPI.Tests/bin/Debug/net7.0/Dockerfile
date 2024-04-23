# Use the .NET 7 runtime as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published application into the container
COPY . .

# Start the application when the container starts
ENTRYPOINT ["dotnet", "StaffManagementAPI.dll"]

