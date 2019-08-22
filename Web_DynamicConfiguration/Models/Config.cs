namespace Web_DynamicConfiguration.Models
{
    public class Config
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public bool IsActive { get; set; }
    }
}
