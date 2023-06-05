// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Lab7_1_2;
using Lab7_1_2.Domain;

TreeNode node = new TreeNode();
node.Name = "1";
node.ParentNode = null;
node.Childrens = new List<TreeNode>();
//var worker = new DbWorker("Data Source=localhost;Initial Catalog=Lab7;Integrated Security=True");
var worker = new DbWorker("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Lab7;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
worker.SaveTree(node);