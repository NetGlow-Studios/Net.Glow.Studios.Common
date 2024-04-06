FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Examples/Ngs.Common.AspNetCore.Storage.Example/Ngs.Common.AspNetCore.Storage.Example.csproj", "Examples/Ngs.Common.AspNetCore.Storage.Example/"]
RUN dotnet restore "Examples/Ngs.Common.AspNetCore.Storage.Example/Ngs.Common.AspNetCore.Storage.Example.csproj"
COPY . .
WORKDIR "/src/Examples/Ngs.Common.AspNetCore.Storage.Example"
RUN dotnet build "Ngs.Common.AspNetCore.Storage.Example.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ngs.Common.AspNetCore.Storage.Example.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ngs.Common.AspNetCore.Storage.Example.dll"]
