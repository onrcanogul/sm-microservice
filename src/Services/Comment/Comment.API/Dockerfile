FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Comment/Comment.API/Comment.API.csproj", "src/Services/Comment/Comment.API/"]
COPY ["src/Shared/Shared/Shared.csproj", "src/Shared/Shared/"]
RUN dotnet restore "src/Services/Comment/Comment.API/Comment.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Comment/Comment.API"
RUN dotnet build "Comment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Comment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comment.API.dll"]
