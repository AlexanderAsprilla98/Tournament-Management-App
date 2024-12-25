#!/bin/bash
set -e

if [ "$1" = '/opt/mssql/bin/sqlservr' ]; then
    # Start SQL Server
    /opt/mssql/bin/sqlservr &
    pid="$!"

    # Wait for SQL Server to start
    for i in {1..30}; do
        if /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" &> /dev/null; then
            break
        fi
        sleep 1
    done

    # Run initialization scripts
    for f in /docker-entrypoint-initdb.d/*; do
        case "$f" in
            *.sql)    echo "$0: running $f"; /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i "$f"; echo ;;
            *)        echo "$0: ignoring $f" ;;
        esac
    done

    # Wait for SQL Server
    wait "$pid"
fi

exec "$@"