##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
##
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 8080
#
#ENV ASPNETCORE_URLS=http://+:8080
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /app
#COPY . .
#RUN dotnet restore "CSMS.csproj"
#COPY . .
#WORKDIR /app
#RUN dotnet build "CSMS.csproj" -c Release -o /app/build
#ENTRYPOINT ["dotnet", "CSMS.dll"]
#
#FROM build AS publish
#RUN dotnet publish "CSMS.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "CSMS.dll"]

# ASP.NET Core Runtime イメージの取得
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app/
# ポート 80, 443 でリッスン
EXPOSE 80
EXPOSE 443

# ソースコードのビルド用に .NET SDK イメージの取得
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/
COPY "CSMS.csproj" "."
# プロジェクトの依存関係の復元
RUN dotnet restore "CSMS.csproj"
COPY . .
# ソースコードをビルド 構成:Release, 出力先: /app/build
RUN dotnet build "CSMS.csproj" -c Release -o /app/build
# ビルドしたモジュールを発行 構成:Release, 出力先: /app/publish, プロパティ: UseAppHost=false
RUN dotnet publish "CSMS.csproj" -c Release -o /app/publish -p:UseAppHost=false

FROM base
WORKDIR /app/
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CSMS.dll"]



