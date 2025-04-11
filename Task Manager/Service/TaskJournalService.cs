using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Validations;

namespace Task_Manager.Service;
using Task_Manager.Entities;
using Task_Manager.Repository;

public class TaskJournalService
{
    private readonly TaskJournalRepository _repository;
    private readonly TaskJournalValidation _journalValidation = App.ServiceProvider.GetRequiredService<TaskJournalValidation>();

    public TaskJournalService(TaskJournalRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<string>> AddTaskJournal(TaskJournal taskJournal)
    {
        List<string> errors = await _journalValidation.JournalValidation(taskJournal);
        if (errors.Any())
        {
            return errors;
        }
        await _repository.AddTaskJournalAsync(taskJournal);
        return errors;
    }

    public async Task<List<string>> UpdateTaskJournal(TaskJournal taskJournal)
    {
        List<string> errors = await _journalValidation.JournalValidation(taskJournal);
        if (errors.Any())
        {
            return errors;
        }
        await _repository.UpdateTaskJournalAsync(taskJournal);
        return errors;
    }
    
    
}