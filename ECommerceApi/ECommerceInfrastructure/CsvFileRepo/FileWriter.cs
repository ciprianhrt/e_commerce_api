using System.Globalization;
using ECommerceDomain;

namespace ECommerceInfrastructure.CsvFileRepo;

public static class FileWriter
{
    private static string PRODUCTS_FILE_PATH = "/Users/ciprianhirtescu/RiderProjects/ECommerceApi/ECommerceApplication/Repositories/CsvFileRepo/Products.csv";
    
    public static void Write(Product product)
    {
        if (!File.Exists(PRODUCTS_FILE_PATH))
        {
            throw new FileNotFoundException(PRODUCTS_FILE_PATH);
        }

        using var writer = new StreamWriter(PRODUCTS_FILE_PATH);
        writer.WriteLine(string.Join(",", new List<string>{product.Id.ToString(), product.Name, product.Price.ToString(CultureInfo.InvariantCulture)}));
        writer.Close();
    }
    
    public static void RemoveAt(Guid id)
    {
        if (!File.Exists(PRODUCTS_FILE_PATH))
        {
            throw new FileNotFoundException(PRODUCTS_FILE_PATH);
        }

        var products = FileReader.ReadAll();
        
        if(products.All(p => p.Id != id))
        {
            throw new ArgumentException("Product not found");
        }
        
        products.RemoveAll(x => x.Id == id);
        using var writer = new StreamWriter(PRODUCTS_FILE_PATH);
        products.ForEach(product => writer.WriteLine(string.Join(",", new List<string>
        {
            product.Id.ToString(),
            product.Name, product.Price.ToString(CultureInfo.InvariantCulture)
        })));
        writer.Close();
    }
}