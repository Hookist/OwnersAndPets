
using System.ComponentModel.DataAnnotations;


namespace OwnersProject.Models
{
    public class Owner
    {
        public int Id { get; set; }
        [MaxLength(30), MinLength(3), Required]
        public string Name { get; set; }


    }
}