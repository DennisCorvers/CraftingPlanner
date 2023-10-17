namespace CraftingPlannerLib.Entities
{
    public class Mod : Entity
    {
        public string Name { get; set; }

        public Mod(string name)
        {
            Name = name;
        }
    }
}
