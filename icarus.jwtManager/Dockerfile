FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5057

ENV ASPNETCORE_URLS=http://+:5057

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["icarus.jwtManager/icarus.jwtManager.csproj", "icarus.jwtManager/"]
RUN dotnet restore "icarus.jwtManager/icarus.jwtManager.csproj"
COPY . .
WORKDIR "/src/icarus.jwtManager"
RUN dotnet build "icarus.jwtManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "icarus.jwtManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "icarus.jwtManager.dll"]
