FROM mcr.microsoft.com/mssql/server:2017-CU21-ubuntu-16.04

# Environment variables required by mssql
ENV SA_PASSWORD admin1234!
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

# Port configuration
EXPOSE 1433

# Copy init scripts
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app
COPY . /usr/src/app

# Grant permissions for the run-initialization script to be executable
RUN chmod +x /usr/src/app/run-initialization.sh

# Run init script
CMD /bin/bash ./entrypoint.sh