# use MSSQL 2017 image on Ubuntu 16.04
FROM mcr.microsoft.com/mssql/server:2022-latest

USER 0

# create directory within SQL container for database files
RUN mkdir -p /opt/mssql-scripts

# copy the database files from host to container
COPY db/db_init.sql /opt/mssql-scripts

# set environment variables
ENV MSSQL_SA_PASSWORD=MatriX84
ENV ACCEPT_EULA=Y

# run initial scripts
RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'MatriX84' -i /opt/mssql-scripts/db_init.sql \
    && pkill sqlservr 