using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Models
{
    public class Citizen
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
    }
}