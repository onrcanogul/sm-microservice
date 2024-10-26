FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Notification/Notification.API/Notification.API.csproj", "src/Services/Notification/Notification.API/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "src/Services/Notification/Notification.API/Notification.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Notification/Notification.API"
RUN dotnet build "Notification.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notification.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.API.dll"]
