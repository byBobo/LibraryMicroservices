#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BorrowingsService/BorrowingsService.csproj", "BorrowingsService/"]
RUN dotnet restore "BorrowingsService/BorrowingsService.csproj"
COPY . .
WORKDIR "/src/BorrowingsService"
RUN dotnet build "BorrowingsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BorrowingsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BorrowingsService.dll"]