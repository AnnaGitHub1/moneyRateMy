#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Api.Rate/Api.Rate.csproj", "Api.Rate/"]
RUN dotnet restore "Api.Rate/Api.Rate.csproj"
COPY . .
WORKDIR "/src/Api.Rate"
RUN dotnet build "Api.Rate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Rate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Rate.dll"]