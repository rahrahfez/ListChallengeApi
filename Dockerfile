FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
EXPOSE 5000

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ListChallengeApi.dll"]