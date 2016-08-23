using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Customers")]
    public class Customer : GeoEntity
    {
    }

    public class CustomerRequest : GeoEntityRequest
    {
    }
}