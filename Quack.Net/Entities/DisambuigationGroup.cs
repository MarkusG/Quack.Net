namespace DuckDuckGo
{
    /// <summary>
    /// A group of topics for a disambiguation answer
    /// </summary>
    public class DisambiguationGroup
    {
        public string Name { get; set; }

        public Topic[] Topics { get; set; }
    }
}
