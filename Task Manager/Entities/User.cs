using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Task_Manager.Entities;

[Table("user")]
public class User
{
   [Key]
   [Column("id_user")]
    public int idUser{get;set;}
    [Required]
    public string name{get;set;}
    [Required]
    public string email{get;set;}
    [Required]
    public string password{get;set;}
    [Required]
    [Column("phone_number")]
    public string phoneNumber{get;set;}

    public User(int idUser, string name, string email, string password, string phoneNumber)
    {
        this.idUser = idUser;
        this.name = name;
        this.email = email;
        this.password = password;
        this.phoneNumber = phoneNumber;
    }
    public User(string name, string email, string password, string phoneNumber)
    {
        this.name = name;
        this.email = email;
        this.password = password;
        this.phoneNumber = phoneNumber;
    }

    
    public int getIdUser() => idUser;
    public string getMail() => email;
    public override string ToString()
    {
        return $"Name: {name}\nEmail: {email}\n Phone: {phoneNumber}";
    }

}