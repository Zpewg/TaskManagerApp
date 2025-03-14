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
    [ForeignKey("id_user")]
    public int idUser{get;set;}
    [Column("journal_text")]
    [Required]
    public string journalText{get;set;}

    public TaskJournal(int idUser, string journalText)
    {
        this.idUser = idUser;
        this.journalText = journalText;
    }
    public override string ToString()
    {
        return $"Journal id: {idTaskJournal}"+
               $"\n Journal text: {journalText}";
    }
    
}