using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;

namespace Task_Manager;

public class JournalUserControlViewModel
{
    private string _noteName;
    private string _noteText;
    
    private readonly TaskJournalService _journalService;
    private readonly TaskJournalRepository _journalRepository;
    private User _user;


    public JournalUserControlViewModel(TaskJournalService journalService, User user,
        TaskJournalRepository journalRepository)
    {
        _journalService = journalService;
        _journalRepository = journalRepository;
        _user = user;
        LoadTasks();
    }
    
    private ObservableCollection<TaskJournal> _notes = new ObservableCollection<TaskJournal>();

    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public string NoteName
    {
        get => _noteName;
        set{_noteName = value; OnPropertyChanged(nameof(NoteName)); }
    }

    public string NoteText
    {
        get => _noteText;
        set{_noteText = value; OnPropertyChanged(nameof(NoteText)); }
    }

    public User User
    {
        get => _user;
        set { _user = value; 
        OnPropertyChanged(nameof(User));
    } }
    public string JournalMessage => $"{User?.name}'s journal";

    public ObservableCollection<TaskJournal> Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            OnPropertyChanged(nameof(Notes));
        }
    }
    
    private void TextValidation()
    {
        if (string.IsNullOrEmpty(NoteText))
        {
            NoteText = "Untitled";
        }
    }

    private void NameValidation()
    {
        if (string.IsNullOrEmpty(NoteName))
        {
            NoteName = "Untitled";
        }
    }

    
    private async Task LoadTasks()
    {
        var journalList = await _journalRepository.GetTaskJournalsByUserIdAsync(User.idUser);
        Notes.Clear();
        foreach (var journal in journalList)
        {
            Console.WriteLine($"NOTE: {journal.journalName} - {journal.journalText}");
            Notes.Add(journal);
        }
        OnPropertyChanged(nameof(Notes));
    }

    public async Task addNote()
    {
        NameValidation();
        TextValidation();
        
        TaskJournal journal = new TaskJournal(_user.idUser,NoteName, NoteText);
        List<string> errors = await _journalService.AddTaskJournal(journal);
        if (errors.Any())
        {
            string errorMsg = string.Join("\n", errors);
            MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        MessageBox.Show("Success", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        Notes.Add(journal);
        OnPropertyChanged(nameof(Notes));
    }
}