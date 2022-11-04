echo "Step 1: Build projects"
echo "---------------------------------------------------------"
dotnet build /app/DrReview.Core/DrReview.Api/DrReview.Api.csproj
echo "---------------------------------------------------------"
echo "Step 2: Publish projects"
echo "---------------------------------------------------------"
dotnet publish /app/DrReview.Core/DrReview.Api/DrReview.Api.csproj -o /app/publish/
echo "---------------------------------------------------------"
echo "Step 3: Copy appSettings to execution folder"
echo "---------------------------------------------------------"
cp "./publish/appsettings.json" "./appsettings.json"
cp "./publish/appsettings.Development.json" "appsettings.Development.json"
echo "---------------------------------------------------------"
echo "Step 4: Start the app"
echo "---------------------------------------------------------"
dotnet /app/publish/DrReview.Api.dll
