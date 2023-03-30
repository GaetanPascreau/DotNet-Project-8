#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /AppointmentService
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AppointmentService.csproj", "./"]
RUN dotnet restore "AppointmentService.csproj"
COPY . .
RUN dotnet build "AppointmentService.csproj" -c Release -o /AppointmentService

FROM build AS publish
RUN dotnet publish "AppointmentService.csproj" -c Release -o /AppointmentService

FROM base AS final
WORKDIR /AppointmentService
COPY --from=publish /AppointmentService .
ENTRYPOINT ["dotnet", "AppointmentService.dll"]