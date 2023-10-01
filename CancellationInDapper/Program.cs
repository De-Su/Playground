await Npgsql();
await MySql();
await MSSQL();
return;

async Task CancellationTest(string dbtype, DbConnection connection, string sleepQuery)
{
    Red(dbtype);
    Console.WriteLine();
    var stopwatch = new Stopwatch();
    try
    {
        await using (connection)
        {
            await connection.OpenAsync();
            stopwatch.Start();
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var queryResult = await connection.ExecuteAsync(new CommandDefinition(sleepQuery, cancellationToken: cts.Token));
            stopwatch.Stop();
            PrintResultInfo(queryResult, stopwatch.Elapsed);
        }
    }
    catch (Exception e)
    {
        stopwatch.Stop();
        PrintExceptionInfo(e, stopwatch.Elapsed);
    }
}

async Task Npgsql() => await CancellationTest("Npgsql",
    new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"),
    "SELECT PG_SLEEP(10);");

async Task MySql() => await CancellationTest("MySql",
    new MySqlConnection("Server=localhost;Port=3306;Database=mysql;User Id=root;Password=password;"),
    "SELECT SLEEP(10);");

async Task MSSQL() => await CancellationTest("MSSQL",
    new SqlConnection("Server=localhost;Database=master;User Id=sa;Password=Password1;Encrypt=false"),
    "WAITFOR DELAY '00:00:10';");


static void PrintExceptionInfo(Exception e, TimeSpan elapsed)
{
    Red("Exception type: ");
    Console.WriteLine(e.GetType());
    Red("Exception message: ");
    Console.WriteLine(e.Message);
    Red("Elapsed: ");
    Console.WriteLine(elapsed);
    Console.WriteLine(new string('=', 80));
}

static void PrintResultInfo(int result, TimeSpan elapsed)
{
    Console.WriteLine("No exception");
    Red("Result: ");
    Console.WriteLine(result);
    Red("Elapsed: ");
    Console.WriteLine(elapsed);
    Console.WriteLine(new string('=', 80));
}

static void Red(string msg)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write(msg);
    Console.ForegroundColor = ConsoleColor.Gray;
}