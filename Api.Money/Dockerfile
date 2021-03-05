#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Api.Money/Api.Money.csproj", "Api.Money/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore "Api.Money/Api.Money.csproj"
COPY . .
WORKDIR "/src/Api.Money"
RUN dotnet build "Api.Money.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Money.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENTRYPOINT ["dotnet", "Api.Money.dll"]