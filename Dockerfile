FROM mcr.microsoft.com/dotnet/sdk:5.0 as build

WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "iTechArt.iTechQuiz.WebApp.dll"]




