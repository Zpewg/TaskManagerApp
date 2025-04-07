using System.Collections.ObjectModel;
using Google.Protobuf.WellKnownTypes;
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
    
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    private ObservableCollection<UserTasks> _notes = new ObservableCollection<UserTasks>();

    /*
    public string NoteName
    {
        get => _noteName;
        set{_noteName = value; OnPropertyChange(nameof(NoteName)); }
    }*/
    public User User
    {
        get => _user;
        set => _user = value;
    }
    public string JournalMessage => $"{User?.name}'s journal";
    
}