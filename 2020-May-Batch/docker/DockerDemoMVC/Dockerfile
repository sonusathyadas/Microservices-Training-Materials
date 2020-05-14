FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY ./*.csproj ./
RUN dotnet restore 

# copy everything else and build app
COPY ./ ./
RUN dotnet build
RUN dotnet publish -c release -o /dist --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
ENV Company="Hexaware"
ENV Author="Akhil"
COPY --from=build /dist ./
ENTRYPOINT ["dotnet", "DockerDemoMVC.dll"]