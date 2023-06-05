using Lab7_1_2_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Lab7_1_2_Framework
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

        private void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }
        private void CloseConnection()
        {
            connection.Close();
        }
        private void Commit()
        {
            transaction.Commit();
        }
        private void Rollback()
        {
            transaction.Rollback();
        }
        private void OpenConnection()
        {
            connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
        }

        private TreeNode GetParentNode(List<TreeNode> nodes)
        {
            string parentNodeName = nodes.First().NodeDbName;
            var elseRoot = nodes.Where(n => !n.NodeDbName.Contains(parentNodeName));
            if (elseRoot.Any())
                throw new Exception("Найдено более одного корня дерева");
            return nodes.Where(n => !n.NodeDbName.Contains($"{parentNodeName}/")).SingleOrDefault();
        }
        private int InsertToTreeNodesAndGetNodeId(TreeNode currNode)
        {
            command.CommandText = $"INSERT INTO tree_of_life_node(path) VALUES ('{currNode.Name}');SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];SELECT @@IDENTITY AS [@@IDENTITY];";
            var dataReader = command.ExecuteReader();
            var stringInDatabaseResponse = "";
            while (dataReader.Read())
            {
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetValue(i) == null)
                        stringInDatabaseResponse = string.Empty;
                    else
                        stringInDatabaseResponse = dataReader.GetValue(i).ToString();
                }
            }
            dataReader.Close();
            return Int32.Parse(stringInDatabaseResponse);
        }
        private void InsertToTreeNodesParent(TreeNode currNode)
        {
            command.CommandText = $@"INSERT INTO tree_of_life_materialized_path(node_id, path) VALUES ({currNode.Id}, '{currNode.NodeDbName}')";
            var dataReader = command.ExecuteReader();
            dataReader.Close();
        }
        private void RecourceSave(TreeNode currNode)
        {
            if (currNode != null)
            {
                currNode.Id = InsertToTreeNodesAndGetNodeId(currNode);
                //currNode.Id = currNode.Id;
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
        public TreeNode GetNodeById(int id)
        {
            TreeNode result = new TreeNode();
            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT t.id, t.path, tm.path FROM tree_of_life_node t INNER JOIN tree_of_life_materialized_path tm ON t.id = tm.node_id WHERE id = {id}";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                result.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                result.Name = dataReader.GetValue(1).ToString();
                result.NodeDbName = dataReader.GetValue(2).ToString();
            }
            dataReader.Close();


            Commit();
            CloseConnection();
            return result;
        }
        public TreeNode GetTree()
        {
            TreeNode result = null;
            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT tm.node_id, tm.path, t.path FROM tree_of_life_materialized_path tm INNER JOIN tree_of_life_node t ON tm.node_id = t.id";

            var dataReader = command.ExecuteReader();
            List<TreeNode> nodes = new List<TreeNode>();
            while (dataReader.Read())
            {
                var tmp = new TreeNode();
                tmp.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                tmp.NodeDbName = dataReader.GetValue(1).ToString();
                tmp.Name = dataReader.GetValue(2).ToString();
                nodes.Add(tmp);
            }
            dataReader.Close();

            result = GetParentNode(nodes);

            FillChilds(nodes);

            Commit();
            CloseConnection();
            return result;
        }
        private void FillChilds(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {

                var parentNodes = node.NodeDbName.Split('/');
                if (parentNodes.Count() > 2)
                {
                    var parentNodesList = new List<string>(parentNodes);
                    parentNodesList.RemoveAt(0);
                    parentNodesList.RemoveAt(parentNodesList.Count() - 1);
                    var parent = nodes.Where(n => n.NodeDbName == $"/{string.Join("/", parentNodesList)}").SingleOrDefault();
                    node.ParentNode = parent;
                    if (parent != null)
                        parent.Childrens.Add(node);
                }
            }
        }
        public TreeNode GetAllChildsByParentId(int id)
        {
            TreeNode result = new TreeNode();
            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT t.id, t.path, tm.path FROM tree_of_life_node t INNER JOIN tree_of_life_materialized_path tm ON t.id = tm.node_id WHERE id = {id}";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                result.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                result.Name = dataReader.GetValue(1).ToString();
                result.NodeDbName = dataReader.GetValue(2).ToString();
            }
            dataReader.Close();

            command.CommandText = $"SELECT t.id, t.path, tm.path FROM tree_of_life_node t INNER JOIN tree_of_life_materialized_path tm ON t.id = tm.node_id WHERE tm.path LIKE '%{result.NodeDbName}/%'";

            dataReader = command.ExecuteReader();
            List<TreeNode> nodes = new List<TreeNode>();
            nodes.Add(result);
            while (dataReader.Read())
            {
                var tmp = new TreeNode();
                tmp.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                tmp.Name = dataReader.GetValue(1).ToString();
                tmp.NodeDbName = dataReader.GetValue(2).ToString();
                nodes.Add(tmp);
            }
            dataReader.Close();

            FillChilds(nodes);

            Commit();
            CloseConnection();
            return result;
        }
        public TreeNode GetAllParentsByNodeId(int id)
        {
            TreeNode result = new TreeNode();
            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT t.id, t.path, tm.path FROM tree_of_life_node t INNER JOIN tree_of_life_materialized_path tm ON t.id = tm.node_id WHERE id = {id}";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                result.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                result.Name = dataReader.GetValue(1).ToString();
                result.NodeDbName = dataReader.GetValue(2).ToString();
            }
            dataReader.Close();

            var splitIds = result.NodeDbName.Split('/');
            List<string> splitIdList = new List<string>(splitIds);
            splitIdList.RemoveAt(splitIdList.Count - 1);
            splitIdList.RemoveAt(0);
            List<TreeNode> nodes = new List<TreeNode>
            {
                result
            };

            command.CommandText = $"SELECT t.id, t.path, tm.path FROM tree_of_life_node t INNER JOIN tree_of_life_materialized_path tm ON t.id = tm.node_id WHERE id IN ({string.Join(", ", splitIdList)})";
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                var tmp = new TreeNode();
                tmp.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                tmp.Name = dataReader.GetValue(1).ToString();
                tmp.NodeDbName = dataReader.GetValue(2).ToString();
                nodes.Add(tmp);
            }
            dataReader.Close();

            FillChilds(nodes);

            Commit();
            CloseConnection();
            return result;
        }
        public List<TreeNode> GetAllBrothers(int id)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT t.id, t.path, tm.path FROM tree_of_life_node t INNER JOIN tree_of_life_materialized_path tm ON t.id = tm.node_id WHERE id = {id}";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                TreeNode result = new TreeNode();
                result.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                result.Name = dataReader.GetValue(1).ToString();
                result.NodeDbName = dataReader.GetValue(2).ToString();
                nodes.Add(result);
            }
            dataReader.Close();

            var splitIds = nodes.First().NodeDbName.Split('/');
            List<string> splitIdList = new List<string>(splitIds);
            splitIdList.RemoveAt(splitIdList.Count - 1);
            splitIdList.RemoveAt(0);

            command.CommandText = $"SELECT t.id, t.path, tm.path FROM tree_of_life_node t INNER JOIN tree_of_life_materialized_path tm ON t.id = tm.node_id WHERE (tm.path LIKE '/{string.Join("/", splitIdList)}/%') AND (tm.path NOT LIKE '/{string.Join("/", splitIdList)}/%/')";
            nodes.Clear();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                TreeNode result = new TreeNode();
                result.Id = Int32.Parse(dataReader.GetValue(0).ToString());
                result.Name = dataReader.GetValue(1).ToString();
                result.NodeDbName = dataReader.GetValue(2).ToString();
                nodes.Add(result);
            }
            dataReader.Close();

            Commit();
            CloseConnection();
            return nodes;
        }
        public void AddChildNode(int rootId, TreeNode treeNode)
        {
            var node = GetNodeById(rootId);
            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = string.Empty;

            RecourceSetPath(treeNode, node.NodeDbName);

            RecourceSave(treeNode);

            Commit();
            CloseConnection();
        }
        private void RecourceSetPath(TreeNode treeNode, string nodeDbName)
        {
            treeNode.NodeDbName = nodeDbName;
            foreach (var node in treeNode.Childrens)
            {
                RecourceSetPath(node, nodeDbName);
            }
        }
        public void DeleteNodeById(int id)
        {
            var node = GetNodeById(id);

            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM tree_of_life_node WHERE id = {id}";

            var reader = command.ExecuteReader();
            reader.Close();

            command.CommandText = $"DELETE FROM tree_of_life_node WHERE id IN (SELECT node_id FROM tree_of_life_materialized_path WHERE tree_of_life_materialized_path.path LIKE '%{node.NodeDbName}/%')";
            reader = command.ExecuteReader();
            reader.Close();

            Commit();
            CloseConnection();
        }

        public void UpdateNodeInfo(TreeNode node)
        {
            var dbNode = GetNodeById(node.Id);

            OpenConnection();
            BeginTransaction();
            
            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE tree_of_life_node SET path = '{node.Name}' WHERE id = {node.Id}";

            command.ExecuteReader().Close();

            Commit();
            CloseConnection();
        }

        private void RecourceUpdate(TreeNode node, string oldPath, string newPath)
        {
            node.NodeDbName = node.NodeDbName.Replace(oldPath, newPath);
            command.CommandText = $"UPDATE tree_of_life_materialized_path SET path = '{node.NodeDbName}' WHERE node_id = {node.Id}";
            var reader = command.ExecuteReader();
            reader.Close();
            foreach (var child in node.Childrens)
            {
                RecourceUpdate(child, oldPath, newPath);
            }
        }

        public void ResetNodeParent(TreeNode node)
        {
            var dbNode = GetNodeById(node.ParentNode.Id);
            var splitIds = node.NodeDbName.Split('/');
            List<string> splitIdList = new List<string>(splitIds);
            splitIdList.RemoveAt(splitIdList.Count - 1);
            splitIdList.RemoveAt(0);

            OpenConnection();
            BeginTransaction();

            command = new SqlCommand();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandType = CommandType.Text;

            RecourceUpdate(node, $"/{string.Join("/", splitIdList)}",node.ParentNode.NodeDbName);

            Commit();
            CloseConnection();
        }
    }
}
