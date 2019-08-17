#Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY ./ ./
RUN dotnet build 
RUN dotnet publish -c Release -o dist

#Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS publish
WORKDIR /app
COPY --from=build /src/dist/ ./
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings:EventSqlConnection="Data Source=mssql;Initial Catalog=HexaDB;Persist Security Info=True;User ID=sa;Password=Password@123"
ENTRYPOINT [ "dotnet", "EventManagementApplication.dll" ]

