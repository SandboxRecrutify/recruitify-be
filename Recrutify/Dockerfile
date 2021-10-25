FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Recrutify.Host/Recrutify.Host.csproj", "Recrutify.Host/"]
COPY ["Recrutify.Services/Recrutify.Services.csproj", "Recrutify.Services/"]
COPY ["Recrutify.DataAccess/Recrutify.DataAccess.csproj", "Recrutify.DataAccess/"]
RUN dotnet restore "Recrutify.Host/Recrutify.Host.csproj"
COPY . .
WORKDIR "/src/Recrutify.Host"
RUN dotnet build "Recrutify.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recrutify.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Recrutify.Host.dll

