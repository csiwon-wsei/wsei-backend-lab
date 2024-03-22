FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StatisticRabbitMQSubscriber/StatisticRabbitMQSubscriber.csproj", "StatisticRabbitMQSubscriber/"]
RUN dotnet restore "StatisticRabbitMQSubscriber/StatisticRabbitMQSubscriber.csproj"
COPY . .
WORKDIR "/src/StatisticRabbitMQSubscriber"
RUN dotnet build "StatisticRabbitMQSubscriber.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StatisticRabbitMQSubscriber.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StatisticRabbitMQSubscriber.dll"]
