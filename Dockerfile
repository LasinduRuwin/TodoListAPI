FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TodoListAPI/TodoListAPI.csproj", "TodoListAPI/"]
RUN dotnet restore "TodoListAPI/TodoListAPI.csproj"
COPY . .
WORKDIR "/src/TodoListAPI"
RUN dotnet build "TodoListAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoListAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoListAPI.dll"]