using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

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
       public string? userTaskCol { get; set; }
       [Column("date_of_task")]
       [Required]
       public DateOnly date { get; set; }
       [Column("time_of_task")]
       [Required]
       public TimeOnly time { get; set; }
       
       [Column("name_of_task", TypeName = "varchar(15)")]
       [Required]
       public string nameOfTask  { get; set; }
       
       [NotMapped]
       public string TaskName
       {
              get => nameOfTask;
              set => nameOfTask = value;
       }

       [NotMapped]
       public string Description
       {
              get => userTaskCol ?? "";
              set => userTaskCol = value;
       }

       [NotMapped]
       public string DueDateFormatted
       {
              get => date.ToString("yyyy-MM-dd");
              set
              {
                     if (DateOnly.TryParse(value, out DateOnly parsedDate))
                     {
                            date = parsedDate;
                     }
              }
       }

       [NotMapped]
       public string DueTimeFormatted
       {
              get => time.ToString("hh:mm tt");
              set
              {
                     if (TimeOnly.TryParse(value, out TimeOnly parsedTime))
                     {
                            time = parsedTime;
                     }
              }
       }


       
       public UserTasks(){}


       public UserTasks(int idUser,string nameOfTask, string userTaskCol, DateOnly date, TimeOnly time)
       {
              this.idUser = idUser;
              this.userTaskCol = userTaskCol;
              this.date = date;
              this.time = time;
              this.nameOfTask = nameOfTask;
       }

       public UserTasks(int idUser, string nameOfTask, DateOnly date, TimeOnly time)
       {
              this.idUser = idUser;
              this.nameOfTask = nameOfTask;
              this.date = date;
              this.time = time;
       }

       
       public override string ToString()
       {
              return $"Task Id {idUserTasks}\n Task Date {date}\n Task Time {time}" +
                     $"Task description {userTaskCol}";
       }
}