namespace ORA.UI.PAMS.Demo.Models
{
    public class Validate
    {
        public bool isValid { get; set; } = true;
        public bool notValid { get { return !isValid; } }
        public string data { get; set; } = "";

        public Validate()
        {
        }

        public Validate(bool _isValid, string _data)
        {
            isValid = _isValid;
            data = _data;
        }
    }
}
