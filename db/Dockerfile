FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

# Create SQL directories
RUN mkdir -p /var/opt/mssql/data && \
    mkdir -p /var/opt/mssql/log && \
    mkdir -p /var/opt/mssql/backup && \
    chown -R mssql:root /var/opt/mssql && \
    chmod -R 775 /var/opt/mssql

# Add init script
COPY init-script.sql /docker-entrypoint-initdb.d/
RUN chown mssql:root /docker-entrypoint-initdb.d/init-script.sql

USER mssql

# Configure SQL Server
ENV MSSQL_MEMORY_LIMIT_MB=400
ENV MSSQL_MIN_MEMORY_LIMIT_MB=100
ENV MSSQL_TCP_PORT=1433

EXPOSE 1433

HEALTHCHECK --interval=10s --timeout=3s \
    CMD /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q "SELECT 1" || exit 1

CMD ["/opt/mssql/bin/sqlservr"]