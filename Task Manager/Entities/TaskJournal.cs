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
    [ForeignKey("id_user_journal")]
    public int iduser{get;set;}
    [Column("journal_name", TypeName = "varchar(18)")]
    public string journalName{get;set;}
    [Column("journal_text")]
    [Required]
    public string journalText{get;set;}

    public TaskJournal(int iduser, string journalName, string journalText)
    {
        this.iduser = iduser;
        this.journalName = journalName;
        this.journalText = journalText;
    }
    public override string ToString()
    {
        return $"Journal name: {journalName}"+
               $"\n Journal text: {journalText}";
    }
    
}