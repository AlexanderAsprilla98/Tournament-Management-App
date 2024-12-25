FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

# Create SQL Server directories
RUN mkdir -p /var/opt/mssql && \   
    mkdir -p /var/opt/mssql/data && \
    mkdir -p /var/opt/mssql/log && \
    chown -R mssql:root /var/opt/mssql && \
    chmod -R 775 /var/opt/mssql

# Grant permissions to sqlservr
RUN chown -R mssql:root /opt/mssql/bin/sqlservr && \
chmod u+x /opt/mssql/bin/sqlservr

# Add init script
COPY init-script.sql /docker-entrypoint-initdb.d/
RUN chown mssql:root /docker-entrypoint-initdb.d/init-script.sql

USER mssql

# Configure SQL Server
ENV ACCEPT_EULA=Y \
    MSSQL_PID=Developer \
    MSSQL_TCP_PORT=1433 \
    MSSQL_MEMORY_LIMIT_MB=256 \
    MSSQL_AGENT_ENABLED=false \
    MSSQL_ENABLE_HADR=0 \
    MSSQL_ENABLE_CLR=0 \
    MSSQL_COLLATION=SQL_Latin1_General_CP1_CI_AS \
    MSSQL_BACKUP_DIR=/var/opt/mssql/backup \
    MSSQL_DATA_DIR=/var/opt/mssql/data \
    MSSQL_LOG_DIR=/var/opt/mssql/log

# Disable unused features
ENV MSSQL_AGENT_ENABLED=false 
ENV MSSQL_ENABLE_HADR=0
ENV MSSQL_ENABLE_CLR=0

EXPOSE 1433

CMD ["/opt/mssql/bin/sqlservr"]