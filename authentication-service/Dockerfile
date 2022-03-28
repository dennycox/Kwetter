#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6060

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Kwetter
COPY ["authentication-service.csproj", "."]
RUN dotnet restore "./authentication-service.csproj"
COPY . .
WORKDIR "/Kwetter/."
RUN dotnet build "authentication-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "authentication-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "authentication-service.dll"]