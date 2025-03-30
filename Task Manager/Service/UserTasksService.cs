using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Validations;

namespace Task_Manager.Service;
using Task_Manager.Repository;
using Task_Manager.Entities;

public class UserTasksService
{
    private readonly UserTasksRepository _userTasks;
    private readonly UserTasksValidation _userTasksValidation = App.ServiceProvider.GetRequiredService<UserTasksValidation>();
    

    public UserTasksService(UserTasksRepository userTasks)
    {
        _userTasks = userTasks;
    }

    public async Task<List<string>> CreateUserTask(UserTasks userTasks)
    {
        List<string> error = await _userTasksValidation.ValidateUserTask(userTasks);
        if (!error.Any())
        {
            await _userTasks.AddUserTask(userTasks);
            return error;
        }

        return error;

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