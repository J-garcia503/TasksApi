FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

RUN mkdir -p /app/data && chown -R $APP_UID /app/data

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/TasksApi.Domain/TasksApi.Domain.csproj", "src/TasksApi.Domain/"]
COPY ["src/TasksApi.Application/TasksApi.Application.csproj", "src/TasksApi.Application/"]
COPY ["src/TasksApi.Infrastructure/TasksApi.Infrastructure.csproj", "src/TasksApi.Infrastructure/"]
COPY ["src/TasksApi.Api/TasksApi.Api.csproj", "src/TasksApi.Api/"]
RUN dotnet restore "src/TasksApi.Api/TasksApi.Api.csproj"

COPY src/ src/
RUN dotnet publish "src/TasksApi.Api/TasksApi.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "TasksApi.Api.dll"]
