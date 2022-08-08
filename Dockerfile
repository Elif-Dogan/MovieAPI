# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
# # copy csproj and restore as distinct layers
# WORKDIR /app

# COPY DataTransferObject/*.csproj ./DataTransferObject/
# COPY DomainService/*.csproj ./DomainService/
# COPY MoviesAPI/*.csproj ./MoviesAPI/
# RUN dotnet restore

# COPY . ./
# WORKDIR /app/DataTransferObject
# RUN dotnet build -c Release -o /app


# WORKDIR /app/DomainService
# RUN dotnet build -c Release -o /app

# WORKDIR /app/MoviesAPI
# RUN dotnet build -c Release -o /app

# FROM build AS publish
# RUN dotnet publish -c Release -o /app

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app 

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS build
# # # copy everything else and build app
# WORKDIR /app
# EXPOSE 5005
# COPY --from=build /app/out .
# # RUN dotnet publish -c release -o /app --no-restore

# ENTRYPOINT ["dotnet", "MoviesAPI.dll"]



# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
# WORKDIR /app
# EXPOSE 5005

# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app

# COPY DomainService/*.csproj ./DomainService/
# COPY DataTransferObject/*.csproj ./DataTransferObject/
# COPY MoviesAPI/*.csproj ./MoviesAPI/

# RUN dotnet restore
# COPY . .
# WORKDIR /app/DomainService
# RUN dotnet build -c Release -o /app

# WORKDIR /app/DataTransferObject
# RUN dotnet build -c Release -o /app

# WORKDIR /app/MoviesAPI
# RUN dotnet build -c Release -o /app

# FROM build AS publish
# RUN dotnet publish -c Release -o /app

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "MoviesAPI.dll"]
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS build
WORKDIR /src

COPY DataTransferObject/*.csproj ./DataTransferObject/
COPY DomainService/*.csproj ./DomainService/
COPY API/*.csproj ./MoviesAPI/

RUN dotnet restore DataTransferObject.csproj
RUN dotnet restore DomainService.csproj
RUN dotnet restore MoviesAPI.csproj
COPY . .
WORKDIR /src/DomainService
RUN dotnet build -c Release -o /app

WORKDIR /src/DataTransferObject
RUN dotnet build -c Release -o /app

WORKDIR /src/MoviesAPI
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MoviesAPI.dll"]