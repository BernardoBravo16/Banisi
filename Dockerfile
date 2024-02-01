# Usar un SDK de .NET para construir la solución
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copiar los archivos csproj y restaurar las dependencias de NuGet por separado para mejorar la cacheabilidad
COPY YappyBanisi.sln .
COPY Banisi.Domain/*.csproj ./Banisi.Domain/
COPY Banisi.Common/*.csproj ./Banisi.Common/
COPY Banisi.Persistence/*.csproj ./Banisi.Persistence/
COPY Banisi.Infrastructure/*.csproj ./Banisi.Infrastructure/
COPY Banisi.Application/*.csproj ./Banisi.Application/
COPY Banisi.Web.API/*.csproj ./Banisi.Web.API/
#RUN dotnet restore --verbosity detailed

# Copiar el resto de los archivos y construir la aplicación
COPY Banisi.Domain/ ./Banisi.Domain/
COPY Banisi.Common/ ./Banisi.Common/
COPY Banisi.Persistence/ ./Banisi.Persistence/
COPY Banisi.Infrastructure/ ./Banisi.Infrastructure/
COPY Banisi.Application/ ./Banisi.Application/
COPY Banisi.Web.API/ ./Banisi.Web.API/

# Construir y publicar el proyecto API
RUN dotnet publish Banisi.Web.API/Banisi.Web.API.csproj -c Release -o out

# Construir la imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Banisi.Web.API.dll"]