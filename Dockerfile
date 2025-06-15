# Use the official .NET 6 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the solution file and restore dependencies
COPY *.sln ./
COPY MSESG.CargoCare.Web/*.csproj ./MSESG.CargoCare.Web/
COPY MSESG.CargoCare.Web/Reportes ./MSESG.CargoCare.Web/
COPY MSESG.CargoCare.Core/*.csproj ./MSESG.CargoCare.Core/
COPY MSESG.CargoCare.Core.EFServices/*.csproj ./MSESG.CargoCare.Core.EFServices/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish MSESG.CargoCare.Web/MSESG.CargoCare.Web.csproj -c Release -o out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

COPY --from=build-env /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "MSESG.CargoCare.Web.dll"] 