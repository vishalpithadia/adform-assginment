FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

# Copy project file and restore as distinct layers
COPY --link . .
RUN dotnet restore ./Assignment.API/Assignment.API.csproj 

RUN dotnet publish ./Assignment.API/Assignment.API.csproj --no-restore -o /app


# Enable globalization and time zones:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
EXPOSE 8080
WORKDIR /app

RUN apk add icu-libs

COPY --link --from=build /app .
ENTRYPOINT ["./Assignment.API"]