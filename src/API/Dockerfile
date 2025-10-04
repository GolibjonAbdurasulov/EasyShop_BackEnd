FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
VOLUME ["/app/wwwroot/uploads"]
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS publish
#in container
ARG VARIABLE
WORKDIR /src 
COPY ["src/", "src/"]
RUN dotnet publish "src/API/API.csproj" -c Release -o /publish /p:UseAppHost=false

# Final stage for the runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "API.dll"]
