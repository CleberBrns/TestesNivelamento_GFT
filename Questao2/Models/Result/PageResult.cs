namespace Questao2.Models.Result
{
    public class PageResult
    {
        public string page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public FootballMatchResult[] data { get; set; }
    }
}
