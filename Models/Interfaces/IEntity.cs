namespace SmartHome.Models
{
    public interface IEntity<TId>
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public TId Id { get; set; }
    }
}
