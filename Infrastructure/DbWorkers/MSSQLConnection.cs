using System.Data;
using System.Data.SqlClient;
using DomainLab3.Models.Dtos;
using Infrastructure.DbWorkers.Common;

namespace Infrastructure.DbWorkers
{
    public class MSSQLConnection: IDbConnection
    {
        private readonly string CONNECTION_STRING;
        private SqlConnection connection;
        private SqlTransaction transaction;
        private SqlCommand command;

        public MSSQLConnection(string connectionString)
        {
            CONNECTION_STRING = connectionString;
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public DbDto Execute(string sqlRequest)
        {
            return Execute(sqlRequest, new List<Parameter>());
        }

        public DbDto Execute(string sqlRequest, List<Parameter> parametersList)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = string.Empty;

            Prepare(sqlRequest, parametersList);
            var databaseDto = ReadDatabaseResponseResponse();
            return databaseDto;
        }

        public void OpenConnection()
        {
            connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        private MSSQLConnection Prepare(string sqlRequest, List<Parameter> parametersList)
        {
            command.CommandText = sqlRequest;
            // command.Prepare();
            if (parametersList.Count == 0) 
                return this;
            foreach (var tmpParameter in parametersList)
            {
                var parameter = new SqlParameter
                {
                    ParameterName = tmpParameter.Name,
                    Value = tmpParameter.ValueType switch
                    {
                        "" => tmpParameter.Value,
                        "int" => int.Parse(tmpParameter.Value),
                        "bool" => bool.Parse(tmpParameter.Value),
                        _ => tmpParameter.Value
                    }
                };

                command.Parameters.Add(parameter);
            }

            return this;
        }

        private DbDto ReadDatabaseResponseResponse()
        {
            var dataReader = command.ExecuteReader();
            var databaseDto = new DbDto();
            while (dataReader.Read())
            {
                var stringInDatabaseResponse = new List<string>();
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    stringInDatabaseResponse.Add(dataReader.GetValue(i).ToString());
                }

                databaseDto.Add(stringInDatabaseResponse);
            }

            dataReader.Close();
            return databaseDto;
        }
    }
}
