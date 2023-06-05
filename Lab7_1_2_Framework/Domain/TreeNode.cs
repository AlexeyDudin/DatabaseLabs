using System.Collections.Generic;

namespace Lab7_1_2_Framework.Domain
{
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string NodeDbName { get; set; } = "";
        public TreeNode ParentNode { get; set; } = null;
        public List<TreeNode> Childrens { get; set; } = new List<TreeNode>();        
    }
}
