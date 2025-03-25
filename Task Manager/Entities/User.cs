using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;


namespace Task_Manager.Entities;

[Table("user")]
public class User
{
   [Key]
   [Column("id_user")]
    public int idUser{get;set;}
    [Required]
    [Column(TypeName = "varchar(20)")]
    [MaxLength(20)]
    public string name{get;set;}
    [Required]
    [Column(TypeName = "varchar(60)")]
    [MaxLength(60)]
    public string email{get;set;}
    [Required]
    [Column(TypeName = "varchar(16)")]
    [MaxLength(16)]
    public string password{get;set;}
    [Required]
    [Column("phone_number", TypeName = "varchar(10)")]
    [MaxLength(10)]
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
    public User(){}

    
    public int getIdUser() => idUser;
    public string getMail() => email;
    public override string ToString()
    {
        return $"Id:{idUser} Name: {name}\nEmail: {email}\n Phone: {phoneNumber}";
    }

}