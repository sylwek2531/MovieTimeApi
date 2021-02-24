#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./MovieTime.Api/MovieTime.Api.csproj", "./MovieTime.Api/"]
COPY ["./MovieTime.Infrastructure/MovieTime.Infrastructure.csproj", "./MovieTime.Infrastructure/"]
COPY ["./MovieTime.Core/MovieTime.Core.csproj", "./MovieTime.Core/"]
RUN dotnet restore "MovieTime.Api/MovieTime.Api.csproj"
#COPY . .
COPY ["./MovieTime.Infrastructure/", "MovieTime.Infrastructure/"]
COPY ["./MovieTime.Core/", "MovieTime.Core/"]
COPY ["./MovieTime.Api/", "MovieTime.Api/"]
WORKDIR "/src/MovieTime.Api"
RUN dotnet build "MovieTime.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieTime.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieTime.Api.dll"]