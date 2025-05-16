using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceDomain;

[Table("products")]
public class Product
{
    [Key]
    [Column]
    public Guid Id { get; set; }
    
    [Column]
    public string Name { get; set; }
    
    [Column]
    public decimal Price { get; set; }
}

//validations: 
//mediator validators, distinct validators for each handler

//can't update/create a product that has aleary the name 
//delete: product with the id searched exists 