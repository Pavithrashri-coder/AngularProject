using System.Data;

namespace Options
{
    public interface IDbContext
    {

        Task<List<TResponse>> ExecuteListAsync<TRequest, TResponse>

            (string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null)

                where TRequest : class where TResponse : class;

        Task<List<TResponse>> ExecuteListAsync<TResponse>(string storedProcedure, string? connectionStrings = null, int? commandTimeout = null)

                where TResponse : class;

        Task<TResponse> ExecuteAsync<TRequest, TResponse>(string storedProcedure, TRequest? entity, string? connectionStrings = null, int? commandTimeout = null)

                where TRequest : class where TResponse : class;

        Task<TResponse> ExecuteAsync<TResponse>(string storedProcedure, string? connectionStrings = null, int? commandTimeout = null)

                where TResponse : class;

        Task<TRequest> ExecuteAsync<TRequest>(string storedProcedure, TRequest? entity, string? connectionStrings = null, int? commandTimeout = null)

                where TRequest : class;

        Task<TResponse> ExecuteScalarAsync<TRequest, TResponse>(string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null)

                where TRequest : class;

        Task<(List<TFirst>, List<TSecond>)> ExecuteQueryMultipleAsync<TRequest, TFirst, TSecond>(string storedProcedure, TRequest? parameters, string? connectionStrings = null, int? commandTimeout = null)

            where TRequest : class where TFirst : class where TSecond : class;

        Task<(List<TFirst>, List<TSecond>, List<TThird>)> ExecuteQueryMultipleAsync<TFirst, TSecond, TThird>(string storedProcedure, object? parameters = null, string? connectionStrings = null, int? commandTimeout = null)

            where TFirst : class where TSecond : class where TThird : class;


        DataTable ExecuteDataTable<TRequest>

             (string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null)

                 where TRequest : class;

        DataSet ExecuteDataSet<TRequest>(string storedProcedure, TRequest? entity = null, string? connectionStrings = null, int? commandTimeout = null)

             where TRequest : class;

    }
}
