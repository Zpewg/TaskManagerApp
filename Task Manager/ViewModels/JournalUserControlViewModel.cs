using System.Collections.ObjectModel;
using System.ComponentModel;
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
    }
    
    private ObservableCollection<UserTasks> _notes = new ObservableCollection<UserTasks>();

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
    }
}
    public string JournalMessage => $"{User?.name}'s journal";

    public ObservableCollection<UserTasks> Notes
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

    public async Task addNote()
    {
        NameValidation();
        TextValidation();
        
        TaskJournal journal = new TaskJournal(_user.idUser,NoteName, NoteText);
        await _journalService.AddTaskJournal(journal);
    }
}