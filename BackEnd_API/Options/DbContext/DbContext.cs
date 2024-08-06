using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Options.Attributes;
using Options.Helper;
using System.Data;
using System.Data.SqlClient;

namespace Options

{
    public class DbContext : IDbContext
    {
        private readonly ConnectionStringsOptions connectionString;
        private readonly ILogger<DbContext> _logger;
        const int defaultCommandTimeout = 30;
        public DbContext(IOptionsSnapshot<ConnectionStringsOptions> optionsSnapshot, ILogger<DbContext> logger)
        {
            connectionString = optionsSnapshot.Value;
            _logger = logger;
        }


        public async Task<TResponse> ExecuteScalarAsync<TRequest, TResponse>(string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null)
      where TRequest : class
        {
            _logger.LogInformation("DBContext: ExecuteScalarAsync");
            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);

            if (entity != null)
                _logger.LogInformation("DBContext: requestParam = {entity}", JObject.FromObject(entity).ToString());

            using var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB);

            return await connection.ExecuteScalarAsync<TResponse>(storedProcedure, entity, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout);

        }
        private void SetOutputParameters<TEntity>(TEntity entity, DynamicParameters parameter)
        {
            var properties = entity?.GetType().GetProperties()
                .Where(x => !(x.DeclaringType is TEntity) && x.GetCustomAttributes(false).Any(a => a is DBParameterAttribute)
       && !x.PropertyType.Name.Contains("List"));

            foreach (var property in properties ?? throw new ArgumentNullException("properties variable is null in SetOutputParameters"))
            {
                var attr = property?.GetCustomAttributes(true)?.SingleOrDefault() as DBParameterAttribute;
                if (attr?.ParameterDirection == System.Data.ParameterDirection.Output)
                {
                    if (parameter.ParameterNames.Contains(property?.Name))
                    {
                        var value = parameter.Get<object>(property?.Name);
                        property?.SetValue(entity, value);
                    }
                }
            }
        }
        public async Task<List<TResponse>> ExecuteListAsync<TRequest, TResponse>(string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null)

    where TRequest : class where TResponse : class

        {


            _logger.LogInformation("DBContext: ExecuteListAsync");

            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);

            if (entity != null)

                _logger.LogInformation("DBContext: requestParam = {entity}", JObject.FromObject(entity).ToString());

            var parameter = DynamicParametersBuilder.Parse(entity);


            using (var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB))

            {

                var result = (await connection.QueryAsync<TResponse>(storedProcedure, parameter, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout)).ToList();

                SetOutputParameters(entity, parameter);

                return result;

            }

        }

        public async Task<List<TResponse>> ExecuteListAsync<TResponse>(string storedProcedure, string? connectionStrings = null, int? commandTimeout = null)

           where TResponse : class

        {

            _logger.LogInformation("DBContext: ExecuteListAsync");

            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);


            using var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB);

            return (await connection.QueryAsync<TResponse>(storedProcedure, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout)).ToList();

        }


        public DataTable ExecuteDataTable<TRequest>(string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null) where TRequest : class

        {


            using (var connection = new SqlConnection(connectionString.DefaultDB))

            {

                connection.Open();

                var parameter = DynamicParametersBuilder.Parse(entity);

                using (var reader = connection.ExecuteReader(storedProcedure, parameter, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout))

                {

                    var dataTable = new DataTable();

                    dataTable.Load(reader);

                    SetOutputParameters(entity, parameter);

                    return dataTable;

                }

            }

        }

        public DataSet ExecuteDataSet<TRequest>(string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null) where TRequest : class

        {


            using SqlConnection sqlConnection = new SqlConnection(connectionString.DefaultDB);

            sqlConnection.Open();

            DynamicParameters dynamicParameters = DynamicParametersBuilder.Parse(entity);

            DataSet dataSet = new DataSet();

            using (SqlCommand sqlCommand = new SqlCommand(storedProcedure, sqlConnection))

            {

                sqlCommand.CommandType = CommandType.StoredProcedure;

                if (commandTimeout.HasValue)

                {

                    sqlCommand.CommandTimeout = commandTimeout.Value;

                }

                if (dynamicParameters != null)

                {

                    foreach (var param in dynamicParameters.ParameterNames)

                    {

                        sqlCommand.Parameters.Add(new SqlParameter(param, dynamicParameters.Get<object>(param) ?? DBNull.Value));

                    }

                }

                using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataSet);

            }

            SetOutputParameters(entity, dynamicParameters);

            return dataSet;

        }

        public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null)

           where TRequest : class where TResponse : class

        {

            _logger.LogInformation("DBContext: ExecuteAsync");

            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);

            if (entity != null)

                _logger.LogInformation("DBContext: requestParam = {entity}", JObject.FromObject(entity).ToString());

            var parameter = DynamicParametersBuilder.Parse(entity);

            using var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB);

            var result = await connection.QueryFirstAsync<TResponse>(storedProcedure, parameter, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout);

            SetOutputParameters(entity, parameter);

            return result;

        }

        public async Task<TResponse> ExecuteAsync<TResponse>(string storedProcedure, string? connectionStrings = null, int? commandTimeout = null)

          where TResponse : class

        {

            _logger.LogInformation("DBContext: ExecuteAsync");

            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);


            using var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB);

            return await connection.QueryFirstAsync<TResponse>(storedProcedure, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout);


        }

        public async Task<TRequest> ExecuteAsync<TRequest>(string storedProcedure, TRequest? entity, string? connectionStrings = null, int? commandTimeout = null)

            where TRequest : class

        {

            _logger.LogInformation("DBContext: ExecuteAsync");

            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);

            if (entity != null)

                _logger.LogInformation("DBContext: requestParam = {entity}", JObject.FromObject(entity).ToString());

            var parameter = DynamicParametersBuilder.Parse(entity);

            using var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB);

            await connection.ExecuteAsync(storedProcedure, parameter, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout);

            SetOutputParameters(entity, parameter);

            return entity ?? throw new Exception("entity is null in ExecuteAsync");


        }

        public async Task<(List<TFirst>, List<TSecond>)> ExecuteQueryMultipleAsync<TRequest, TFirst, TSecond>(string storedProcedure, TRequest? entity, string? connectionStrings = null, int? commandTimeout = null)
   where TRequest : class where TFirst : class where TSecond : class
        {
            _logger.LogInformation("DBContext: ExecuteQueryMultipleAsync");
            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);

            if (entity != null)
                _logger.LogInformation("DBContext: parameters = {parameters}", JObject.FromObject(entity).ToString());

            var parameter = DynamicParametersBuilder.Parse(entity);

            using var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB);
            using var multi = await connection.QueryMultipleAsync(storedProcedure, parameter, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout);

            var firstResult = (await multi.ReadAsync<TFirst>()).ToList();
            var secondResult = (await multi.ReadAsync<TSecond>()).ToList();
            SetOutputParameters(entity, parameter);
            return (firstResult, secondResult);
        }

        // Example for three result sets
        public async Task<(List<TFirst>, List<TSecond>, List<TThird>)> ExecuteQueryMultipleAsync<TFirst, TSecond, TThird>(string storedProcedure, object? parameters = null, string? connectionStrings = null, int? commandTimeout = null)
             where TFirst : class where TSecond : class where TThird : class
        {
            _logger.LogInformation("DBContext: ExecuteQueryMultipleAsync");
            _logger.LogInformation("DBContext: storedProcedureName = {storedProcedure}", storedProcedure);

            if (parameters != null)
                _logger.LogInformation("DBContext: parameters = {parameters}", JObject.FromObject(parameters).ToString());

            using var connection = new SqlConnection(connectionStrings ?? connectionString.DefaultDB);
            using var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout ?? defaultCommandTimeout);

            var firstResult = (await multi.ReadAsync<TFirst>()).ToList();
            var secondResult = (await multi.ReadAsync<TSecond>()).ToList();
            var thirdResult = (await multi.ReadAsync<TThird>()).ToList();

            return (firstResult, secondResult, thirdResult);
        }
    }
}
