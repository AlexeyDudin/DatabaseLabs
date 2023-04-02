namespace DomainLab3.Models.Dtos
{
    public class Parameter
    {
        public string Name { get; set; } = String.Empty;
        public object Value { get; set; } = String.Empty;
        public string ValueType { get; set; } = String.Empty;

        public Parameter(string name, string value, string valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }

        public Parameter(string name, string value)
        {
            Name = name;
            Value = value;
            ValueType = "string";
        }

        public Parameter(string name, bool value) 
        {
            Name = name;
            Value = value;
            ValueType = "boolean";
        }

        public Parameter(string name, int value)
        {
            Name = name;
            Value = value;
            ValueType = "int";
        }
    }

}
