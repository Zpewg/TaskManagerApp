using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Task_Manager.Entities;

[Table("user")]
public class User
{
   [Key]
   [Column("id_user")]
    private int idUser{get;set;}
    [Required]
    private string name{get;set;}
    [Required]
    private string email{get;set;}
    [Required]
    private string password{get;set;}
    [Required]
    [Column("phone_number")]
    private string phoneNumber{get;set;}
    

    public override string ToString()
    {
        return $"Name: {name}\nEmail: {email}\n Phone: {phoneNumber}";
    }

}