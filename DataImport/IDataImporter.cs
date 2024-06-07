namespace DataImport
{
    public interface IDataImporter
    {
        string Path { get; }
        Task<RecipesImport> Import();
    }
}
