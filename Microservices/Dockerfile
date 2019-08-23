#Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet build
RUN dotnet publish -c Release -o out

#Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
WORKDIR /app
COPY --from=build /src/out .
EXPOSE 80

ENV ConnectionStrings:EventSqlConnection="Constr"
ENV Jwt:Secret="thisislengthysecretkey"
ENV Jwt:Issuer="http://localhost:5000"
ENV Jwt:Audience="http://localhost:5001"

ENTRYPOINT [ "dotnet", "EventAPI.dll" ]
