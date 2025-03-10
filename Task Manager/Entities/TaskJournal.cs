using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Task_Manager.Entities;
[Table("task_journal")]
public class TaskJournal
{
   [Key]
    [Column("idtask_journal")]
    private int idTaskJournal{get;set;}
    [Column("id_user")]
    [ForeignKey("id_user")]
    private int idUser{get;set;}
    [Column("journal_text")]
    [Required]
    private string journalText{get;set;}

    public override string ToString()
    {
        return $"Journal id: {idTaskJournal}"+
               $"\n Journal text: {journalText}";
    }
    
}