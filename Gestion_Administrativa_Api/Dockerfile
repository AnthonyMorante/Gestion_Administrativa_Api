FROM mcr.microsoft.com/dotnet/aspnet:7.0-jammy  AS base
WORKDIR /App

EXPOSE 80
EXPOSE 443


# Update the base image to include tzdata
RUN apt-get update && apt-get install -y tzdata

# For Rotativa SDK
RUN apt-get update && apt-get install -y --no-install-recommends apt-utils fontconfig libfreetype6 libjpeg-turbo8 libpng16-16 libx11-6 libxcb1 libxext6 libxrender1 xfonts-75dpi xfonts-base libgdiplus libc6-dev

FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy AS build-env
WORKDIR /App


# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out



FROM base as final
WORKDIR /App
COPY --from=build-env /App/out .
COPY Rotativa ./Rotativa
ENV TZ="America/Bogota"
ENV DOTNET_System_Drawing_EnableUnixSupport=true
RUN chmod -R 777 ./Rotativa
RUN ls .
ENTRYPOINT ["dotnet", "Gestion_Administrativa_Api.dll"]
