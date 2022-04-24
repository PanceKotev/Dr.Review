echo "Step 1: Restore projects"
dotnet restore "DrReview.Core/DrReview.Api/DrReview.Api.csproj"
echo "Step 2: Build projects"
dotnet build "DrReview.Core/DrReview.Api/DrReview.Api.csproj"
echo "Step 3: Publish projects"
dotnet publish "DrReview.Core/DrReview.Api/DrReview.Api.csproj" -o "/app/publish/"
echo "Step 4: Copy appSettings to execution folder"
cp /app/publish/appsettings.json .
cp /app/publish/appsettings.Development.json .
echo "Step 5: Start the app"
dotnet /app/publish/DrReview.Api.dll
