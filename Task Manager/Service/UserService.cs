namespace Task_Manager.Service;
using Task_Manager.Repository;
using Task_Manager.Entities;
using Task_Manager.Validations;

public class UserService
{

    private readonly UserRepository _userRepository;
    private readonly UserValidation userValidation = new UserValidation();
    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public UserService(){}

    public async Task createUser(User user)
    {
        List<string> errors = userValidation.Validate(user);
        if (errors.Any())
        {
            foreach (var e in errors)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            await _userRepository.AddUserAsync(user);
        }
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
}