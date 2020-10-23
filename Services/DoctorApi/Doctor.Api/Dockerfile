#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Doctor.Api/DoctorApi.csproj", "Doctor.Api/"]
COPY ["DoctorApi.Service/DoctorApi.Service.csproj", "DoctorApi.Service/"]
COPY ["DoctorApi.Domain/DoctorApi.Domain.csproj", "DoctorApi.Domain/"]
COPY ["DoctorApi.Messaging.Send/DoctorApi.Messaging.Send.csproj", "DoctorApi.Messaging.Send/"]
COPY ["DoctorApi.Data/DoctorApi.Data.csproj", "DoctorApi.Data/"]
RUN dotnet restore "Doctor.Api/DoctorApi.csproj"
COPY . .
WORKDIR "/src/Doctor.Api"
RUN dotnet build "DoctorApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DoctorApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DoctorApi.dll"]