using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialNeeds.Cloudata.Entities
{
    public class Award
    {
        public Award() { }
        
        public Award(byte id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}