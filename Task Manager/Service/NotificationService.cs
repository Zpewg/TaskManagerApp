using System.Windows.Threading;
using Microsoft.Toolkit.Uwp.Notifications;
using Task_Manager.Entities;
using Task_Manager.Repository;

namespace Task_Manager.Service;

public class NotificationService
{
    private  List<UserTasks> _userTasks;
    private readonly DispatcherTimer _timer;
    private UserTasksRepository _repository;
    private DateTime _lastUpdate =DateTime.MinValue;
    private User _loggedUser;
    private readonly HashSet<string> _notifications = new();

    public NotificationService(UserTasksRepository userTasksRepository, User user)
    {
        _repository = userTasksRepository;
        _loggedUser = user;
        _timer = new DispatcherTimer();
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
        }
        
       BeforeReturningUserTasks();
       Start();
       _timer.Tick += Timer_Tick;
       
     
    }

    public async Task BeforeReturningUserTasks()
    {
        await Task.Delay(TimeSpan.FromSeconds(30));
        await ReturnUserTasks();
    }

    public async Task ReturnUserTasks()
    {
        _userTasks = await _repository.GetUserTasksByUserId(_loggedUser.idUser);
    } 
    public void Start() => _timer.Start();
    
    private void Timer_Tick(object sender, EventArgs e)
    {
        var now = DateTime.Now;
        if ((now - _lastUpdate).TotalMinutes >= 5)
        {
            _ = ReturnUserTasks();
            _lastUpdate = now;
        }
        if (_userTasks != null)
        {
            foreach (var task in _userTasks)
            {
                var taskDateTime = task.date.ToDateTime(task.time);
                int minutesUntilTask = (int)(taskDateTime - now).TotalMinutes;
                if ((minutesUntilTask == 120 || minutesUntilTask == 1440) &&
                    !_notifications.Contains($"{task.idUserTasks}-{minutesUntilTask}"))
                {
                    
                    ShowToast(task.Description, taskDateTime, minutesUntilTask);
                    _notifications.Add($"{task.idUserTasks}-{minutesUntilTask}");
                }
            }
        }
    }

    private void ShowToast(string message, DateTime time, int hours)
    {
        new ToastContentBuilder()
            .AddText($"Reminder: in {hours/60}h")
            .AddText($"{message} at {time:HH:mm}")
            .Show();
    }
}