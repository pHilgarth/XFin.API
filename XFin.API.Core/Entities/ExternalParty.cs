using System.ComponentModel.DataAnnotations;

namespace XFin.API.Core.Entities
{
    public class ExternalParty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
