namespace EspacioClases
{
        public class Flags
    {
        public bool nsfw { get; set; }
        public bool religious { get; set; }
        public bool political { get; set; }
        public bool racist { get; set; }
        public bool sexist { get; set; }
        public bool? @explicit { get; set; }
    }

    public class Joke
    {
        public string category { get; set; }
        public string type { get; set; }
        public string joke { get; set; }
        public Flags flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string lang { get; set; }
    }

    public class Bromas
    {
        public bool error { get; set; }
        public int amount { get; set; }
        public List<Joke> jokes { get; set; }
    }
}