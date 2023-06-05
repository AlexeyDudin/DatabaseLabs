using Lab7_1_2_Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Lab7_1_2_Framework
{
    public class Program
    {
        static void Main(string[] args)
        {
            TreeNode node = new TreeNode();
            node.Name = "2";
            node.ParentNode = null;
            node.Childrens = new List<TreeNode>() {
                new TreeNode()
                    {
                         Name = "222",
                         ParentNode = node
                    },
                new TreeNode()
                    {
                         Name = "33",
                         ParentNode = node
                    }
            };
            var worker = new DbWorker("Data Source=localhost;Initial Catalog=Lab7;Integrated Security=True");
            //var worker = new DbWorker("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Lab7;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            worker.SaveTree(node);
            var tmp = worker.GetNodeById(node.Id);
            var root = worker.GetTree();
            tmp = worker.GetAllChildsByParentId(node.Id);
            tmp = worker.GetAllParentsByNodeId(node.Childrens.First().Id);
            List<TreeNode> brothers = worker.GetAllBrothers(node.Childrens.First().Id);
            worker.AddChildNode(root.Id, new TreeNode());
            node = worker.GetTree();
            node.Childrens.Last().Name = "1111";
            worker.UpdateNodeInfo(node.Childrens.Last());
            node.Childrens.Last().ParentNode = node.Childrens.First();
            worker.ResetNodeParent(node.Childrens.Last());

            root = worker.GetTree();
            worker.DeleteNodeById(root.Id);
        }
    }
}
