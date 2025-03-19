using System.Text.RegularExpressions;
using Task_Manager.Entities;


namespace Task_Manager.Validations;
using Task_Manager.Service;

public class TaskJournalValidation
{
    private readonly TaskJournalService _journal;

    public TaskJournalValidation(TaskJournalService journal)
    {
        _journal = journal;
    }

    public async Task<List<string>> JournalValidation(TaskJournal journal)
    {
        List<string> result = new List<string>();
        List<TaskJournal> journals =await _journal.GetTaskJournals();
        
        //Any kind of character, numbers included, max 45 ch
        Regex regexJournalName = new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?\/~`=-]{1,45}$");


        if (!regexJournalName.IsMatch(journal.journalName))
        {
            result.Add("Invalid journal name");
        }

        if (journals.Where(j =>j.journalName.Equals(journal.journalName)).Count() > 0)
        {
            result.Add("Name already exists");
        }
        
        
        return result;
    }
}