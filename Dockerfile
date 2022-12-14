#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["treeHolesApi.csproj", "."]
RUN dotnet restore "./treeHolesApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "treeHolesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "treeHolesApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "treeHolesApi.dll"]
