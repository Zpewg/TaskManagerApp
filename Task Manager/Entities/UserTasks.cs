using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Manager.Entities;

[Table("user_tasks")]
public class UserTasks
{
       [Key]
       [Column("iduser_tasks")]
       private int idUserTasks{get;set;}
       [ForeignKey("iduser")]
       [Column("id_user")]
       private int idUser;
       [Column("description")]
       [Required]
       private string userTaskCol;
       [Column("date_of_task")]
       [Required]
       private DateOnly date;
       [Column("time_of_task")]
       [Required]
       private TimeOnly time;
       
       [Column("name_of_task")]
       [Required]
       private string nameOfTask;

       public UserTasks(string userTaskCol, DateOnly date, TimeOnly time)
       {
              this.userTaskCol = userTaskCol;
              this.date = date;
              this.time = time;
       }
       
       public override string ToString()
       {
              return $"Task Id {idUserTasks}\n Task Date {date}\n Task Time {time}" +
                     $"Task description {userTaskCol}";
       }
}