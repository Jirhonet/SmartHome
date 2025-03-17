namespace SmartHome.Models
{
    public abstract class EntityModel : IEntity<int>
    {
        /// <inheritdoc />
        public int Id { get; set; }
    }
}