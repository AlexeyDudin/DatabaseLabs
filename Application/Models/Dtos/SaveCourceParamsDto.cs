namespace Application.Models.Dtos
{
    public class SaveCourceParamsDto
    {
        public Guid CourceId { get; set; } = Guid.NewGuid();
        public List<Guid> ModuleIds { get; set; } = new List<Guid>();
        public List<Guid> RequiredModuleIds { get; set; } = new List<Guid>();
    }
}
