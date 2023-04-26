# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0

# Set the working directory to /app
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY Server.csproj ./
RUN dotnet restore

# Copy the rest of the app files
COPY . ./

EXPOSE 31337
# Set the entry point to run the console app
ENTRYPOINT ["dotnet", "bin/Release/net6.0/Server.dll"]