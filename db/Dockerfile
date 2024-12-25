FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

# Create directory for SQL Server
RUN mkdir -p /var/opt/mssql && \
    chown -R mssql /var/opt/mssql && \
    chmod -R 775 /var/opt/mssql

# Grant permissions to sqlservr
RUN chown -R mssql:root /opt/mssql/bin/sqlservr && \
    chmod u+x /opt/mssql/bin/sqlservr

# Switch back to mssql user
USER mssql

EXPOSE 1433

# Set the entrypoint
CMD ["/opt/mssql/bin/sqlservr"]

# COPY init-script.sql /docker-entrypoint-initdb.d/

EXPOSE 1433