using Lab7_1_2.Domain;
using System.Data;
using System.Data.SqlClient;

namespace Lab7_1_2
{
    public class DbWorker
    {
        private readonly string CONNECTION_STRING;
        private SqlConnection connection;
        private SqlTransaction transaction;
        private SqlCommand command;

        public DbWorker(string connectionString)
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

        public void Rollback()
        {
            transaction.Rollback();
        }
        public void OpenConnection()
        {
            connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
        }

        //public TreeNode GetTree()
        //{

        //}

        private int InsertToTreeNodesAndGetNodeId(TreeNode currNode)
        {
            command.CommandText = @$"INSERT INTO tree_of_life_node(path) VALUES ({currNode.Name});
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];
SELECT @@IDENTITY AS [@@IDENTITY];";
            var dataReader = command.ExecuteReader();
            var stringInDatabaseResponse = "";
            while (dataReader.Read())
            {
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetValue(i) != null)
                        stringInDatabaseResponse = String.Empty;
                    else
                        stringInDatabaseResponse = dataReader.GetValue(i).ToString();
                }
            }
            return Int32.Parse(stringInDatabaseResponse);
        }

        private void InsertToTreeNodesParent(TreeNode currNode)
        {
            command.CommandText = @$"INSERT INTO tree_of_life_materialized_path(node_id, path) VALUES ({currNode.Id}, {currNode.NodeDbName})";
            var dataReader = command.ExecuteReader();
        }

        private void RecourceSave(TreeNode currNode)
        {
            if (currNode != null)
            {
                currNode.Id = InsertToTreeNodesAndGetNodeId(currNode);
                currNode.Id = currNode.Id;
                currNode.NodeDbName += $"/{currNode.Id}";

                InsertToTreeNodesParent(currNode);

                foreach (var node in currNode.Childrens)
                {
                    node.NodeDbName += $"/{currNode.Id}";
                    RecourceSave(node);
                }
            }
        }

        public void SaveTree(TreeNode treeNode)
        {
            try
            {
                OpenConnection();
                BeginTransaction();

                command = new SqlCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = string.Empty;

                RecourceSave(treeNode);

                Commit();
                CloseConnection();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        //public TreeNode GetNode(int id)
        //{ 
        //}

        //private DbWorker Prepare(string sqlRequest, List<Parameter> parametersList)
        //{
        //    command.CommandText = sqlRequest;
        //    // command.Prepare();
        //    if (parametersList.Count == 0)
        //        return this;
        //    foreach (var tmpParameter in parametersList)
        //    {
        //        var parameter = new SqlParameter
        //        {
        //            ParameterName = tmpParameter.Name,
        //            Value = tmpParameter.ValueType switch
        //            {
        //                "" => tmpParameter.Value,
        //                "string" => tmpParameter.Value,
        //                "int" => Int32.Parse(tmpParameter.Value.ToString()),
        //                "bool" => Boolean.Parse(tmpParameter.Value.ToString()),
        //                _ => tmpParameter.Value
        //            }
        //        };

        //        command.Parameters.Add(parameter);
        //    }

        //    return this;
        //}

        //public DbDto Execute(string sqlRequest)
        //{
        //    return Execute(sqlRequest, new List<Parameter>());
        //}
    }
}
