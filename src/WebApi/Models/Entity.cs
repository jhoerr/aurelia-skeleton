using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Entity
    {
        public Entity()
        {
            CreatedOn = ModifiedOn = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class EntityRequest
    {
    }

    public class NamedEntity : Entity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        [Index("IX_Name", 1, IsUnique = true)]
        public string Name { get; set; }
    }

    public class NamedEntityRequest : EntityRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string Name { get; set; }
    }

    public class GeoEntity : NamedEntity
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [MaxLength(256)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [MaxLength(256)]
        public string City { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "State or Province")]
        public string StateOrProvince { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(256)]
        public string Country { get; set; }
    }

    public class GeoEntityRequest : NamedEntityRequest
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [MaxLength(256)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Required]
        [MaxLength(256)]
        public string City { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "State or Province")]
        public string StateOrProvince { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(256)]
        public string Country { get; set; }

    }
}