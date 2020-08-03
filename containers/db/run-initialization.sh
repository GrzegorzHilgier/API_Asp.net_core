# Wait to be sure that SQL Server came up
sleep 30s

# Run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P admin1234! -d master -i init-database.sql