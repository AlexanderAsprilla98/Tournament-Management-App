FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

# Create directory for SQL Server
RUN mkdir -p /var/opt/mssql && \
    chown -R mssql /var/opt/mssql && \
    chmod -R 775 /var/opt/mssql

# Grant permissions to sqlservr
RUN chown -R mssql:root /opt/mssql/bin/sqlservr && \
    chmod u+x /opt/mssql/bin/sqlservr

# Install curl and other dependencies
RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg

# Download and install SQL Server command-line tools
RUN curl -o /tmp/mssql-tools.deb https://packages.microsoft.com/debian/10/prod/pool/main/m/mssql-tools/mssql-tools_17.9.1-1_amd64.deb && \
    dpkg -i /tmp/mssql-tools.deb && \
    apt-get install -f -y

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