namespace Core.Data
{
    public class HtmlWidget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }

    public class Newsletter
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}