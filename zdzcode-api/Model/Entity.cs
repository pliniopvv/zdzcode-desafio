namespace zdzcode_api.Model
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
    }

    public interface IEntity
    {
        public int Id { get; set; }
    }
}
