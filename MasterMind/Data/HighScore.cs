namespace MasterMind.Data
{
    public class HighScore
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int Seconds { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
