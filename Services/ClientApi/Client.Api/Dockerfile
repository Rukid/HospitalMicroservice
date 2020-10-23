#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Client.Api/ClientApi.csproj", "Client.Api/"]
COPY ["ClientApi.Service/ClientApi.Service.csproj", "ClientApi.Service/"]
COPY ["ClientApi.Domain/ClientApi.Domain.csproj", "ClientApi.Domain/"]
COPY ["ClientApi.Data/ClientApi.Data.csproj", "ClientApi.Data/"]
COPY ["ClientApi.Messaging.Send/ClientApi.Messaging.Send.csproj", "ClientApi.Messaging.Send/"]
RUN dotnet restore "Client.Api/ClientApi.csproj"
COPY . .
WORKDIR "/src/Client.Api"
RUN dotnet build "ClientApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientApi.dll"]