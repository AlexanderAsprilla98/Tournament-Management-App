FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

# Create directory for SQL Server
RUN mkdir -p /var/opt/mssql && \
    chown -R mssql /var/opt/mssql && \
    chmod -R 775 /var/opt/mssql

# Grant permissions to sqlservr
RUN chown -R mssql:root /opt/mssql/bin/sqlservr && \
    chmod u+x /opt/mssql/bin/sqlservr

USER mssql

ENV MSSQL_MEMORY_LIMIT_MB=256
ENV MSSQL_MIN_MEMORY_LIMIT_MB=64
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Developer

EXPOSE 1433

HEALTHCHECK --interval=30s --timeout=3s \
    CMD /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q "SELECT 1" || exit 1

ENTRYPOINT ["/usr/local/bin/entrypoint.sh"]