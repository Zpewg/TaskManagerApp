using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager.Entities;

[Table("user_tasks")]
public class UserTasks
{
       [Key]
       [Column("iduser_tasks")]
       public int idUserTasks{get;set;}
       [ForeignKey("iduser")]
       [Column("id_user")]
       public int idUser {get; set; }
       [Column("description")]
       [Required]
       public string userTaskCol { get; set; }
       [Column("date_of_task")]
       [Required]
       public DateOnly date { get; set; }
       [Column("time_of_task")]
       [Required]
       public TimeOnly time { get; set; }
       
       [Column("name_of_task")]
       [Required]
       public string nameOfTask  { get; set; }
       
       public UserTasks(){}


       public UserTasks(int idUser, string userTaskCol, DateOnly date, TimeOnly time, string nameOfTask)
       {
              this.idUser = idUser;
              this.userTaskCol = userTaskCol;
              this.date = date;
              this.time = time;
              this.nameOfTask = nameOfTask;
       }

       
       public override string ToString()
       {
              return $"Task Id {idUserTasks}\n Task Date {date}\n Task Time {time}" +
                     $"Task description {userTaskCol}";
       }
}