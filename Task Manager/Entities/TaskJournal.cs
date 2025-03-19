using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Task_Manager.Entities;
[Table("task_journal")]
public class TaskJournal
{
    [Key]
    [Column("idtask_journal")]
    public int idTaskJournal{get;set;}
    [Column("id_user")]
    [ForeignKey("id_userJournal")]
    public int idUser{get;set;}
    [Column("journal_name", TypeName = "varchar(45)")]
    public string journalName{get;set;}
    [Column("journal_text")]
    [Required]
    public string journalText{get;set;}

    public TaskJournal(int idUser, string journalName, string journalText)
    {
        this.idUser = idUser;
        this.journalName = journalName;
        this.journalText = journalText;
    }
    public override string ToString()
    {
        return $"Journal name: {journalName}"+
               $"\n Journal text: {journalText}";
    }
    
}