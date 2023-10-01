# Cancellation in Dapper
### Prepare
```shell
docker run --name postgres -e POSTGRES_PASSWORD=password -p 5432:5432  -d postgres
```
```shell
docker run --name mysql -e MYSQL_ROOT_PASSWORD=password -p 3306:3306  -d mysql
```
```shell
docker run --name mssql -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password1" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

### Run
```shell
dotnet run
```

### Results:  

- **Npgsql**  
**Exception type:** System.OperationCanceledException  
**Exception message:** Query was cancelled  
**Elapsed:** 00:00:05.2134145  
- **MySql**  
No exception  
**Result:** -1  
**Elapsed:** 00:00:05.0427870   
- **MSSQL**  
**Exception type:** Microsoft.Data.SqlClient.SqlException  
**Exception message:** A severe error occurred on the current command.  The results, if any, should be discarded.
Operation cancelled by user.  
**Elapsed:** 00:00:05.1027518   