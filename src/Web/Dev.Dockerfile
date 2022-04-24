#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0
EXPOSE 5000
RUN mkdir /app
COPY ./start.sh ./app
RUN apt-get update && apt-get install -y vim
WORKDIR /app