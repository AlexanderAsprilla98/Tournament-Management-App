FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

# Create directory for SQL Server
RUN mkdir -p /var/opt/mssql && \
    chown -R mssql /var/opt/mssql && \
    chmod -R 775 /var/opt/mssql

# Grant permissions to sqlservr
RUN chown -R mssql:root /opt/mssql/bin/sqlservr && \
    chmod u+x /opt/mssql/bin/sqlservr

# Install mssql-tools
RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/mssql-tools/debian/apt.repo | tee /etc/apt/sources.list.d/mssql-tools.list && \
    apt-get update && \
    apt-get install -y mssql-tools

# Switch back to mssql user
USER mssql

# Limit memory usage
ENV MSSQL_MEMORY_LIMIT=512MB

# Disable SQL Server features that consume memory
# Adjust SQL Server settings
# Set the maximum memory usage and disable unnecessary features
RUN /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'max server memory (MB)', 512; RECONFIGURE;" && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'lightweight pooling', 1; RECONFIGURE;" && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'show advanced options', 1; RECONFIGURE;" && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'xp_cmdshell', 0; RECONFIGURE;" && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'filestream access level', 0; RECONFIGURE;" && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'Ad Hoc Distributed Queries', 0; RECONFIGURE;"

EXPOSE 1433

# Set the entrypoint
CMD ["/opt/mssql/bin/sqlservr"]

# COPY init-script.sql /docker-entrypoint-initdb.d/