namespace CraftingPlannerLib.Models
{
    public sealed class Item : BaseModel
    {
        public Mod Mod { get; }

        public Item(int id, string name, Mod mod)
            :base(id, name)
        {
            Mod = mod;
        }
    }
}
