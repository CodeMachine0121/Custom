FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ./ ./
RUN dotnet restore "CustomBlockChainLab.csproj"
RUN dotnet publish "CustomBlockChainLab.csproj" -c Release -o published

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

ENV TZ Asia/Taipei
RUN apk add tzdata                                  && \
    ln -snf /usr/share/zoneinfo/$TZ /etc/localtime  && \
    echo $TZ > /etc/timezone   

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_HTTP_PORTS=80

ENV DB_SERVER=localhost
ENV DB_NAME=Blockchain
ENV DB_USER=root
ENV DB_PASS=<YourStrong@Passw0rd> 

WORKDIR /app
COPY --from=build /app/published/ ./

ENTRYPOINT ["dotnet", "CustomBlockChainLab.dll"]
