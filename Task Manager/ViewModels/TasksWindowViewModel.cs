using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;


namespace Task_Manager;

public class TasksWindowViewModel : INotifyPropertyChanged
{
    private string _taskName;
    private DateOnly _dueDate;
    private string _timeInput;
    private string _description;
    
    private readonly UserTasksService _userTasksService;
    private readonly UserTasksRepository _userTasksRepository;

 
    private User _user;

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(WelcomeMessage)); 
        }
    }
    public TasksWindowViewModel(UserTasksService userTasksService, User user, UserTasksRepository userTasksRepository)
    {
        _userTasksService = userTasksService;
        
        _user = user;
        
        _userTasksRepository = userTasksRepository;

       BeforeLoadTasks(user);

    }
    public string WelcomeMessage => $"Welcome, {User?.name}";
    public string TaskName
    {
        get => _taskName;
        set { _taskName = value; OnPropertyChanged(nameof(TaskName)); }
    }

    public DateOnly DueDate
    {
        get => _dueDate;
        set { _dueDate = value; OnPropertyChanged(nameof(DueDate)); }
    }

    public string TimeInput
    {
        get => _timeInput;
        set { _timeInput = value; OnPropertyChanged(nameof(TimeInput)); }
    }

    public string Description
    {
        get => _description;
        set { _description = value; }
    }
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    public event PropertyChangedEventHandler PropertyChanged;

    public bool HasErrors => _errors.Any();
    public ObservableCollection<UserTasks> Tasks { get; set; } = new ObservableCollection<UserTasks>();
    

    


    private void AddError(string propertyName, string errorMessage)
    {
        if (!_errors.ContainsKey(propertyName))
        {
            _errors[propertyName] = new List<string>();
        }

        _errors[propertyName].Add(errorMessage);
        OnErrorsChanged(propertyName);
    }

    private void RemoveError(string propertyName)
    {
        if (_errors.ContainsKey(propertyName))
        {
            _errors[propertyName].Clear();
                
            if (_errors[propertyName].Count == 0)
            {
                _errors.Remove(propertyName);
            }

            OnErrorsChanged(propertyName);
        }
    }

    public void ValidateTaskName()
    {
        if (string.IsNullOrEmpty(TaskName))
        {
            AddError("TaskName", "TaskName cannot be empty");
        }
        else
        {
            RemoveError("TaskName");
        }
    }

    public void ValidateDueDate()
    {
        if (DueDate == null)
        {
            AddError("DueDate", "DueDate cannot be empty");
        }
        else
        {
            RemoveError("DueDate");
        }
    }

    public void ValidateTimeInput()
    {
        if (string.IsNullOrEmpty(TimeInput) || !TimeSpan.TryParse(TimeInput, out TimeSpan timeSpan))
        {
            AddError("TimeInput", "Time cannot be empty");
        }
        else
        {
            RemoveError("TimeInput");
        }
    }

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void BeforeLoadTasks(User user)
    {
        await LoadTasks(user);
    }
    
    private async Task LoadTasks(User user)
    {
        var userTasks = await _userTasksRepository.GetUserTasksByUserId(User.idUser);
        Tasks.Clear();
        Console.WriteLine("Metoda se apeleaza" + User.idUser );
        foreach (var task in userTasks)
        {
            Tasks.Add(task);
            Console.WriteLine(task);
            
        }
        OnPropertyChanged(nameof(Tasks));
    }

    public async Task CreateTask()
    {
        ValidateTaskName();
        ValidateDueDate(); 
        ValidateTimeInput();
        if (HasErrors)
        {
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        TimeOnly timeOnly = TimeOnly.Parse(TimeInput);
        
        UserTasks userTasks = new UserTasks(User.idUser, TaskName, Description, DueDate, timeOnly);
       List<string> error = await _userTasksService.CreateUserTask(userTasks);
       if (error.Any())
       {
           string errorMessage = string.Join("\n", error);
           MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
           return;
       }
       Tasks.Add(userTasks);
       OnPropertyChanged(nameof(Tasks));
       MessageBox.Show(userTasks.idUser + " has been created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}