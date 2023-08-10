namespace BBMApi.Model
{
    public class Church
    {
        public int    churchId      { get; set; }
        public string? churchName    { get; set; }
        public string location      { get; set; }
        public string branch        { get; set; }
        public string province      { get; set; }    
        public string city          { get; set; }
        public string region        { get; set; }
        public int    pastorId      { get; set; }
    }
}
