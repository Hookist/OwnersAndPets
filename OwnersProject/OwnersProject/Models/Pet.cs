
using System.ComponentModel.DataAnnotations;


namespace OwnersProject.Models
{
    public class Pet
    {
        public int Id;
        [MaxLength(30), MinLength(3), Required]
        public string Name;
        public int OwnerId;

     
    }
}