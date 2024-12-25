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

# Limitar el uso de memoria
ENV MSSQL_MEMORY_LIMIT=512MB

# Deshabilitar características de SQL Server que consumen memoria
# Ajuste de configuraciones de SQL Server
# Establece el uso de memoria máxima y desactiva características no necesarias
RUN /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'max server memory (MB)', 512; RECONFIGURE;" && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${MSSQL_SA_PASSWORD}" \
    -Q "EXEC sp_configure 'lightweight pooling', 1; RECONFIGURE;"

EXPOSE 1433

# Set the entrypoint
CMD ["/opt/mssql/bin/sqlservr"]

# COPY init-script.sql /docker-entrypoint-initdb.d/