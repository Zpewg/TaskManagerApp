using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Task_Manager.Repository;

namespace Task_Manager.Validations;
using Task_Manager.Entities;
using Task_Manager.Service;

public class UserTasksValidation
{
    private readonly UserTasksRepository _userTasksRepository;

    public UserTasksValidation(UserTasksRepository userTasksRepository)
    {
        _userTasksRepository = userTasksRepository;
    }

    public async Task<List<string>> ValidateUserTask(UserTasks userTasks)
    {
        List<string> errorList = new List<string>();
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
        //Any character, max 15ch
        Regex regexTaskName = new Regex(@"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?\/~`=\s-]{1,15}$");



        if (!regexTaskName.IsMatch(userTasks.nameOfTask))
        {
            errorList.Add("Invalid Task Name");
        }

        if (userTasks.date < currentDate)
        {
            errorList.Add("Invalid Date Range");
        }

        if (userTasks.date <= currentDate && userTasks.time < currentTime)
        {
            errorList.Add("You cannot add a time in the past");
        }
        
        
        return errorList;
    }
}