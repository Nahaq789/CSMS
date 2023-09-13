##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
##
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "CSMS.csproj"
COPY . .
WORKDIR /app
RUN dotnet build "CSMS.csproj" -c Release -o /app/build
ENTRYPOINT ["dotnet", "CSMS.dll"]

FROM build AS publish
RUN dotnet publish "CSMS.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSMS.dll"]
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
#WORKDIR /app
#COPY CSMS.csproj .
#RUN dotnet restore
#COPY . .
#RUN dotnet publish -c Release -o /app/publish
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#ENV ASPNETCORE_URLS=http://+:8080
#WORKDIR /app
#COPY --from=base /app/publish .
#ENTRYPOINT [ "dotnet", "CSMS.dll" ]

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /source
#COPY . .
#RUN dotnet restore "CSMS.csproj" 
#RUN dotnet publish "CSMS.csproj" -c release -o /app --no-restore
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#COPY --from=build /app ./
#
#EXPOSE 5000
#
#ENTRYPOINT ["dotnet", "CSMS.dll"]

