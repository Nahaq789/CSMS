# .NET SDKイメージをベースにする
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# プロジェクトファイルをコピーして依存関係を復元
WORKDIR /app
COPY MyWebApp.csproj .
RUN dotnet restore

# アプリケーションコードをコピーしてビルド
COPY . .
RUN dotnet publish -c Release -o out

# 実行用の軽量なランタイムイメージを使用
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# アプリケーションを起動
ENTRYPOINT ["./MyWebApp"]
