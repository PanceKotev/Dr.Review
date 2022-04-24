dotnet restore "DrReview.Core/DrReview.Api/DrReview.Api.csproj"
dotnet build "DrReview.Core/DrReview.Api/DrReview.Api.csproj"
dotnet publish "DrReview.Core/DrReview.Api/DrReview.Api.csproj" -o "/app/publish/"
cp /app/publish/appsettings.json .
cp /app/publish/appsettings.Development.json .
dotnet /app/publish/DrReview.Api.dll
