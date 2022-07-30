namespace BlogMVC.Data
{
    public class Rosette:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

    }
}
