namespace JobApplicationSystem.Models.Dto
{
    public class GetJobPostingDTO
    {
        public int JobPostingID { get; set; }
        public string JobPostingTitle { get; set; }
        public string JobPostingDescription { get; set; }
        public string Job { get; set; }
    }
}
