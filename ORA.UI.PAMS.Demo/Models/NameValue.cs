namespace ORA.UI.PAMS.Demo.Models
{
    public class NameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
