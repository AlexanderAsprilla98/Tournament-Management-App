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

USER mssql

# Configure SQL Server
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Developer
ENV MSSQL_MEMORY_LIMIT_MB=400
ENV MSSQL_TCP_PORT=1433

EXPOSE 1433

CMD ["/opt/mssql/bin/sqlservr"]