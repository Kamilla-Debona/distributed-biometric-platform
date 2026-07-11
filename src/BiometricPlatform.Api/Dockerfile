# syntax=docker/dockerfile:1.7

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY src/BiometricPlatform.Api/BiometricPlatform.Api.csproj                       src/BiometricPlatform.Api/
COPY src/BiometricPlatform.Application/BiometricPlatform.Application.csproj       src/BiometricPlatform.Application/
COPY src/BiometricPlatform.Contracts/BiometricPlatform.Contracts.csproj           src/BiometricPlatform.Contracts/
COPY src/BiometricPlatform.Domain/BiometricPlatform.Domain.csproj                 src/BiometricPlatform.Domain/
COPY src/BiometricPlatform.Infrastructure/BiometricPlatform.Infrastructure.csproj src/BiometricPlatform.Infrastructure/

RUN dotnet restore src/BiometricPlatform.Api/BiometricPlatform.Api.csproj

COPY src/ src/

RUN dotnet publish src/BiometricPlatform.Api/BiometricPlatform.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_RUNNING_IN_CONTAINER=true

RUN mkdir -p /app/uploads \
 && chown -R app:app /app

COPY --from=build --chown=app:app /app/publish .

USER app

EXPOSE 8080

ENTRYPOINT ["dotnet", "BiometricPlatform.Api.dll"]
