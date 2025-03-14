namespace Task_Manager.Service;
using Task_Manager.Repository;
using Task_Manager.Entities;

public class UserService
{

    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }


    public async Task createUser(User user)
    {
        await _userRepository.AddUserAsync(user);
    }
}