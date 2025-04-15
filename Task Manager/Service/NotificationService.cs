using System.Windows.Threading;
using Task_Manager.Entities;
using Task_Manager.Repository;

namespace Task_Manager.Service;

public class NotificationService
{
    private  List<UserTasks> _userTasks;
    private readonly DispatcherTimer _timer;
    private UserTasksRepository _repository;
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
       //_timer.Tick += Timer_Tick;
       Start();
     
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

        foreach (var task in _userTasks)
        {
           
        }
    }
}