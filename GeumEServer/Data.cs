using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GeumEServer
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Adress { get; set; }
        public string Comment { get; set; }
        public bool HasImage { get; set; }
    }

    public class Dog
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        public string Species { get; set; }
    }

    public class Walk
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public int Distance { get; set; }

    }

    public class WalkPlace
    {
        [Key]
        public int Id { get; set; }
        public int WalkId { get; set; }
        public int PlaceId { get; set; }
    }

    public class WalkDog
    {
        [Key]
        public int Id { get; set; }
        public int WalkId { get; set; }
        public int DogId { get; set; }
    }

    public class Place
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Kind { get; set; }
        [Required]
        public int Area { get; set; }
    }

    public class Msg
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string sendEmail { get; set; }
        [Required]
        public string recieveEmail { get; set; }
        [Required]
        public string Note { get; set; }
    }
}
