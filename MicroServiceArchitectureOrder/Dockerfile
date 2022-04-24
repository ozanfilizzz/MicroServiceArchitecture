#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MicroServiceArchitectureOrder/MicroServiceArchitectureOrder.csproj", "MicroServiceArchitectureOrder/"]
COPY ["OrderingApplication/OrderingApplication.csproj", "OrderingApplication/"]
COPY ["OrderingDomain/OrderingDomain.csproj", "OrderingDomain/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["OrderingInfrastructure/OrderingInfrastructure.csproj", "OrderingInfrastructure/"]
RUN dotnet restore "MicroServiceArchitectureOrder/MicroServiceArchitectureOrder.csproj"
COPY . .
WORKDIR "/src/MicroServiceArchitectureOrder"
RUN dotnet build "MicroServiceArchitectureOrder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroServiceArchitectureOrder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServiceArchitectureOrder.dll"]
