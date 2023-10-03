FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MISA.WebFresher042023.Api/MISA.WebFresher042023.Api.csproj", "MISA.WebFresher042023.Api/"]
COPY ["MISA.WebFresher042023.Core/MISA.WebFresher042023.Core.csproj", "MISA.WebFresher042023.Core/"]
COPY ["MISA.WebFresher042023.Infrastructure/MISA.WebFresher042023.Infrastructure.csproj", "MISA.WebFresher042023.Infrastructure/"]
RUN dotnet restore "MISA.WebFresher042023.Api/MISA.WebFresher042023.Api.csproj"
COPY . .
WORKDIR "/src/MISA.WebFresher042023.Api"
RUN dotnet build "MISA.WebFresher042023.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISA.WebFresher042023.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISA.WebFresher042023.Api.dll"]