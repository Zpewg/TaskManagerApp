using System.Text.RegularExpressions;
using Task_Manager.Entities;


namespace Task_Manager.Validations;
using Task_Manager.Service;

public class TaskJournalValidation
{
   


    public async Task<List<string>> JournalValidation(TaskJournal journal)
    {
        List<string> result = new List<string>();
        
        //Any kind of character, numbers included, max 18 ch
        Regex regexJournalName = new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?\/~`=-]{1,18}$");


        if (!regexJournalName.IsMatch(journal.journalName))
        {
            result.Add("Invalid journal name");
        }
        
        
        
        return result;
    }
}