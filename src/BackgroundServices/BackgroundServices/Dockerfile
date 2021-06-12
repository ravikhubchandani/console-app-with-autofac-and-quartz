#docker build -t background-services:1.0 . 
#docker container run -it --name background-jobs-server background-services:1.0

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY BackgroundServices.csproj .
RUN dotnet restore "BackgroundServices.csproj"
COPY . .
RUN dotnet build "BackgroundServices.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "BackgroundServices.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackgroundServices.dll"]