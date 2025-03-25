

namespace Task_Manager.Service;
using Task_Manager.Repository;
using Task_Manager.Entities;
using Task_Manager.Validations;

public class UserService
{

    private readonly UserRepository _userRepository;
    private readonly UserValidation _userValidation;
    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
        
    }
    

    public async Task<List<string>> createUser(User user)
    {
        List<string> errors = await _userValidation.Validate(user);
        if (errors.Any())
        {
            return errors;
        }
            await _userRepository.AddUserAsync(user);
            return errors;
        
    }

    public async Task<string> deleteUser(string mail)
    {
        int? id = await _userRepository.GetUserByMailAsync(mail);
        if (id.HasValue)
        {
            await _userRepository.DeleteUserAsync(id.Value);
            return "User successfully deleted";
        }
        return "User not found";
            
    }

    public async Task<List<User>> getUsers()
    {
        return await _userRepository.GetUsersAsync();
    }

    public async Task updateUser(User user)
    {
        List<string> errors = await _userValidation.Validate(user);
        if (errors.Any())
        {
            foreach (var e in errors)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            _userRepository.UpdateUserAsync(user);
        }
    }
}