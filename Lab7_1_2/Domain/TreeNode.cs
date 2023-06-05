namespace Lab7_1_2.Domain
{
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NodeDbName { get; set; } = "";
        public TreeNode ParentNode { get; set; }
        public List<TreeNode> Childrens { get; set; }        
    }
}
