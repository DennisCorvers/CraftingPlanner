namespace DataImport
{
    public interface IDataImporter
    {
        Task<RecipeDB> Import(string filePath);
    }
}
