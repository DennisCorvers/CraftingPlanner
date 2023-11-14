namespace DataImport
{
    public interface IDataImporter
    {
        Task<RecipesImport> Import(string filePath);
    }
}
