FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Presentation/BlazorApp/BlazorApp.csproj", "Presentation/BlazorApp/"]
COPY ["Presentation/Dto/Dto.csproj", "Presentation/Dto/"]
RUN dotnet restore "Presentation/BlazorApp/BlazorApp.csproj"
COPY . .
WORKDIR "/src/Presentation/BlazorApp"
RUN dotnet build "BlazorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /app/publish/wwwroot /usr/local/webapp/nginx/html
COPY --from=publish /app/publish/wwwroot/nginx.conf /etc/nginx/nginx.conf
