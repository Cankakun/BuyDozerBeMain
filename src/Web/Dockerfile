FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8080

EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Web/Web.csproj" ,"Web/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["src/Domain/Domain.csproj","Domain/"]
COPY ["src/Application/Application.csproj","Application/"]

RUN dotnet restore "/src/Web/Web.csproj"

COPY . .

WORKDIR "/src/src/Web"
RUN dotnet build "Web.csproj" -c Development -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Web.csproj" -c Development -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "BuyDozerBeMain.Web.dll" ]
