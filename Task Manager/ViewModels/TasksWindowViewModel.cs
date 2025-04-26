using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;
using CommunityToolkit.Mvvm.Input;


namespace Task_Manager;

public class TasksWindowViewModel : INotifyPropertyChanged
{
    private string _taskName;
    private DateOnly _dueDate;
    private string _timeInput;
    private string _description;
    private UserTasks _selectedTask; 
    
    
    private readonly UserTasksService _userTasksService;
    private readonly UserTasksRepository _userTasksRepository;
    private User _user;
    
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    private ObservableCollection<UserTasks> _tasks = new ObservableCollection<UserTasks>();
    
    public TasksWindowViewModel(UserTasksService userTasksService, User user, UserTasksRepository userTasksRepository)
    {
        _userTasksService = userTasksService;
        
        _user = user;
        
        _userTasksRepository = userTasksRepository; 
        BeforeLoadTasks();
       
    }
    
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
    public string DueDateFormatted
    {
        get => DueDate.ToString("yyyy-MM-dd");  
        set
        {
            if (DateOnly.TryParse(value, out DateOnly result))
            {
                DueDate = result;  
            }
        }
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

    public UserTasks SelectedTask
    {
        get => _selectedTask;
        set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
    }
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    
    public string WelcomeMessage => $"Welcome, {User?.name}";
   
    public event PropertyChangedEventHandler PropertyChanged;

    public bool HasErrors => _errors.Any();
    
    public ObservableCollection<UserTasks> Tasks
    {
        get => _tasks;
        set
        {
            _tasks = value;
            OnPropertyChanged(nameof(Tasks));
        }
    }
    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void BeforeLoadTasks()
    {
        await LoadTasks();
    }
    
    private async Task LoadTasks()
    {
        var userTasks = await _userTasksRepository.GetUserTasksByUserId(User.idUser);
        Tasks.Clear();
        foreach (var task in userTasks)
        {
            Tasks.Add(task);
            Console.WriteLine(task);
            
        }
        OnPropertyChanged(nameof(Tasks));
    }


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

    public void ValidateDescription()
    {
        if (string.IsNullOrEmpty(Description))
        {
            Description = "Untitled";
        }
    }


    public async Task CreateTask()
    {
        ValidateTaskName();
        ValidateDueDate(); 
        ValidateTimeInput();
        ValidateDescription();
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
    public async Task EditTask()
    {
        ValidateDueDate();
        ValidateTimeInput();
        if (HasErrors)
        {
            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        SelectedTask.DueTimeFormatted = TimeInput;
        SelectedTask.DueDateFormatted = DueDateFormatted;
        var index = Tasks.IndexOf(SelectedTask);
        List<string> error = await _userTasksService.UpdateUserTask(SelectedTask);
        if (error.Any())
        {
            string errorMessage = string.Join("\n", error);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        MessageBox.Show("Task updated succesfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        
       
        if (index >= 0)
        {
        
            Tasks[index] = SelectedTask;
        
            OnPropertyChanged(nameof(Tasks));
        }
       OnPropertyChanged(nameof(SelectedTask));
       await LoadTasks();
    }

    public async Task DeleteTask()
    {
        var selectedTask = SelectedTask;
        Tasks.Remove(selectedTask);
        _userTasksRepository.DeleteUserTask(selectedTask);
    }
    
}