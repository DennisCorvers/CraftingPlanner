namespace CraftingPlannerLib.Entities
{
    public abstract class Entity
    {
        public int Id { get; internal set; }

        public Entity()
        {
            Id = -1;
        }

        public Entity(int id)
        {
            Id = id;
        }

        public override int GetHashCode() => Id;
    }
}
