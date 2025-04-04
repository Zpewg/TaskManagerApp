﻿namespace Task_Manager.Service;
using Task_Manager.Entities;
using Task_Manager.Repository;

public class TaskJournalService
{
    private readonly TaskJournalRepository _repository;

    public TaskJournalService(TaskJournalRepository repository)
    {
        _repository = repository;
    }

    public async Task AddTaskJournal(TaskJournal taskJournal)
    {
        await _repository.AddTaskJournalAsync(taskJournal);
    }

    public async Task UpdateTaskJournal(TaskJournal taskJournal)
    {
        await _repository.UpdateTaskJournalAsync(taskJournal);
    }

    public async Task<string> DeleteTaskJournal(string taskJournalName)
    {
        int? id =  await _repository.GetTaskJournalIdAsync(taskJournalName);
        if (id.HasValue)
        {
            await _repository.DeleteTaskJournalAsync(id.Value);
            return "Task journal deleted";
        }
        return "Task journal not found";
    }

    public async Task<List<TaskJournal>> GetTaskJournals()
    {
        return await _repository.GetTaskJournalsAsync();
    }
}