namespace DemoKanban.Models
{
    public class File
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; } //image/png, application/pdf
        public byte[] Content { get; set; }
    }
}
