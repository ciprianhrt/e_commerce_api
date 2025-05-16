using ECommerceDomain;

namespace ECommerceInfrastructure.CsvFileRepo;

public static class FileReader
{
    private static string PRODUCTS_FILE_PATH = "/Users/ciprianhirtescu/RiderProjects/ECommerceApi/ECommerceApplication/Repositories/CsvFileRepo/Products.csv";
    
    public static Product? ReadById(Guid productId)
    {
        if (!File.Exists(PRODUCTS_FILE_PATH))
        {
            throw new FileNotFoundException(PRODUCTS_FILE_PATH);
        }
        using var reader = new StreamReader(PRODUCTS_FILE_PATH);
        Product? product = null;
        while (reader.ReadLine() is { } line)
        {
            var columns = line.Split(',');
            if (columns.Length != 3)
            {
                throw new FormatException($"{line}");
            }

            if (productId == Guid.Parse(columns[0]))
            {
                product = new Product
                {
                    Id = Guid.Parse(columns[0]),
                    Name = columns[1],
                    Price = decimal.Parse(columns[2])
                };
                break;
            }
            
        }
        reader.Close();
        return product;
    }
    
    public static List<Product> ReadAll()
    {
        if (!File.Exists(PRODUCTS_FILE_PATH))
        {
            throw new FileNotFoundException(PRODUCTS_FILE_PATH);
        }

        using var reader = new StreamReader(PRODUCTS_FILE_PATH);
        var products = new List<Product>();
        while (reader.ReadLine() is { } line)
        {
            var columns = line.Split(',');
            if (columns.Length != 3)
            {
                throw new FormatException($"{line}");
            }
            products.Add(new Product
            {
                Id = Guid.Parse(columns[0]),
                Name = columns[1],
                Price = decimal.Parse(columns[2])
            });
        }
        reader.Close();
        return products;
    }
}