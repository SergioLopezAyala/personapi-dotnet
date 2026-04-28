FROM mcr.microsoft.com/dotnet/sdk:7.0.410

USER root

RUN apt-get update \
  && apt-get install -y curl apt-transport-https gnupg2 \
  && curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /usr/share/keyrings/microsoft.gpg \
  && echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/debian/11/prod bullseye main" > /etc/apt/sources.list.d/mssql-release.list \
  && apt-get update \
  && ACCEPT_EULA=Y apt-get install -y mssql-tools18 unixodbc-dev \
  && apt-get clean \
  && rm -rf /var/lib/apt/lists/*

ENV PATH="/opt/mssql-tools18/bin:${PATH}"
