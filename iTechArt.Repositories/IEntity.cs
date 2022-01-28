namespace iTechArt.Repositories
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
