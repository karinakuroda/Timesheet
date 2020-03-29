#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Timesheet/Timesheet.csproj", "Timesheet/"]
RUN dotnet restore "Timesheet/Timesheet.csproj"
COPY . .
WORKDIR "/src/Timesheet"
RUN dotnet build "Timesheet.csproj" -c Release -o /app/build

RUN echo $(ls -1 /src/Timesheet)

WORKDIR "/src/Timesheet"
FROM build AS publish
RUN dotnet publish "Timesheet.csproj" -c Release -o /app/publish

FROM node
COPY "Timesheet/ClientApp/*" "ClientApp/"
WORKDIR "/src/Timesheet/ClientApp"
RUN echo $(ls -1 /src/Timesheet/ClientApp)
RUN npm install --unsafe-perm node-sass \
    npm start

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Timesheet.dll"]