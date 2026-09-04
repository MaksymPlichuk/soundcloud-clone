FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Soundcloud_Clone.API/Soundcloud_Clone.API.csproj Soundcloud_Clone.API/
COPY Soundcloud_Clone.BLL/Soundcloud_Clone.BLL.csproj Soundcloud_Clone.BLL/
COPY Soundcloud_Clone.DAL/Soundcloud_Clone.DAL.csproj Soundcloud_Clone.DAL/
RUN dotnet restore Soundcloud_Clone.API/Soundcloud_Clone.API.csproj

COPY . .
RUN dotnet publish Soundcloud_Clone.API/Soundcloud_Clone.API.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Soundcloud_Clone.API.dll"]