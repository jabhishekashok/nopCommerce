FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /nop
COPY ./ /nop

RUN dotnet build src/NopCommerce.sln

WORKDIR /nop/src/Presentation/Nop.Web

RUN dotnet publish Nop.Web.csproj -o /app/published

WORKDIR /app/published

RUN mkdir logs && mkdir bin

RUN chmod 775 App_Data/
RUN chmod 775 App_Data/DataProtectionKeys
RUN chmod 775 bin
RUN chmod 775 logs
RUN chmod 775 Plugins
RUN chmod 775 wwwroot/bundles
RUN chmod 775 wwwroot/db_backups
RUN chmod 775 wwwroot/files/exportimport
RUN chmod 775 wwwroot/icons
RUN chmod 775 wwwroot/images
RUN chmod 775 wwwroot/images/thumbs
RUN chmod 775 wwwroot/images/uploaded

#CMD ["sleep", "1d"]

#------------------------------

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime 

WORKDIR /nop
COPY --from=build /app/published /nop
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
CMD ["dotnet","Nop.Web.dll"]
