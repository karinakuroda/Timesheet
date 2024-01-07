FROM mcr.microsoft.com/dotnet/aspnet:8.0  AS base
# Setup NodeJs in base
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    curl -fsSL https://deb.nodesource.com/setup_21.x | bash - &&\
    apt-get install -y nodejs
# End setup

WORKDIR /app
EXPOSE 80
EXPOSE 443

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Setup NodeJs in build
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    curl -fsSL https://deb.nodesource.com/setup_21.x | bash - &&\
    apt-get install -y nodejs
# End setup

WORKDIR /src
COPY ["Timesheet/Timesheet.csproj", "Timesheet/"]
RUN dotnet restore "Timesheet/Timesheet.csproj"
COPY . .
WORKDIR "/src/Timesheet"
RUN dotnet build "Timesheet.csproj" -c Debug -o /app/build

WORKDIR "/src/Timesheet"
FROM build AS publish
RUN dotnet publish "Timesheet.csproj" -c Debug -o /app/publish

RUN npm install @angular/cli && npm link @angular/cli

WORKDIR "/src/Timesheet/ClientApp"

RUN npm install --unsafe-perm node-sass && ng build

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
COPY --from=publish /src/Timesheet/ClientApp ./ClientApp

ENTRYPOINT ["dotnet", "Timesheet.dll"]
