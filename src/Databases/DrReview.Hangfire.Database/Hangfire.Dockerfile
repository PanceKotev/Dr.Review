FROM mcr.microsoft.com/mssql/server:2019-latest AS final

USER root
EXPOSE 1443

RUN apt-get update && apt-get install unzip -y

RUN wget -progress=bar:force -q -O sqlpackage.zip https://go.microsoft.com/fwlink/?linkid=2185670 \
    && unzip -qq sqlpackage.zip -d /opt/sqlpackage \
    && chmod +x /opt/sqlpackage/sqlpackage

COPY /bin/Debug /tmp/database

ARG DBNAME=DrReview.Hangfire.Database
ARG SAPASSWORD=Password!123

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=$SAPASSWORD

RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/sqlpackage/sqlpackage /a:Publish /tsn:. /tdn:${DBNAME} /tu:sa /tp:$SA_PASSWORD /sf:/tmp/database/DrReview.Hangfire.Database.dacpac