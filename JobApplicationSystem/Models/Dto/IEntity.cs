namespace JobApplicationSystem.Models.Dto
{
    public interface IEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        string Company { get; set; }

    }
}
