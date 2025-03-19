namespace Task_Manager.Service;
using Task_Manager.Repository;
using Task_Manager.Entities;

public class UserTaskService
{
    private readonly UserTasksRepository _userTasks;

    public UserTaskService(UserTasksRepository userTasks)
    {
        _userTasks = userTasks;
    }

    public async Task CreateUserTask(UserTasks userTasks)
    {
        await _userTasks.AddUserTask(userTasks);
    }

    public async Task<string> DeleteUserTask(string nameOfTaskName)
    {
        int? id = await _userTasks.FindUserByTaskName(nameOfTaskName);
        if (id.HasValue)
        {
            await _userTasks.DeleteUserTask(id.Value);
            return "Task successfully deleted";
        }
        return "Task not found";
    }

    public async Task UpdateUserTask(UserTasks userTasks)
    {
        await _userTasks.UpdateUserTask(userTasks);
    }

    public async Task<List<UserTasks>> GetUserTasks()
    {
        List<UserTasks> userTasks = await _userTasks.GetUserTasks();
        return userTasks;
    }

 
}