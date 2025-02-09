FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./BobrVerse.Api/BobrVerse.Api.csproj", "./BobrVerse.Api/"]
COPY ["./BobrVerse.Auth/BobrVerse.Auth.csproj", "./BobrVerse.Auth/"]
COPY ["./BobrVerse.Bll/BobrVerse.Bll.csproj", "./BobrVerse.Bll/"]
COPY ["./BobrVerse.Dal/BobrVerse.Dal.csproj", "./BobrVerse.Dal/"]
COPY ["./BobrVerse.Common/BobrVerse.Common.csproj", "./BobrVerse.Common/"]

RUN dotnet restore "./BobrVerse.Api/BobrVerse.Api.csproj"
COPY . .
WORKDIR "/src/BobrVerse.Api"
RUN dotnet build "BobrVerse.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./BobrVerse.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_HTTP_PORTS 80
ENTRYPOINT ["dotnet", "BobrVerse.Api.dll"]