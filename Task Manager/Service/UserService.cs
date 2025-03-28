

using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Task_Manager.Service;
using Task_Manager.Repository;
using Task_Manager.Entities;
using Task_Manager.Validations;
using BCrypt.Net;

public class UserService
{

    private readonly UserRepository _userRepository;
    private readonly UserValidation _userValidation = App.ServiceProvider.GetRequiredService<UserValidation>();
    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        
    }
    

    public async Task<List<string>> createUser(User user)
    {
        List<string> errors =await  _userValidation.Validate(user);
        if (errors.Any())
        {
            return errors;
        }
            user.password = BCrypt.HashPassword(user.password);
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

    public async Task<string> updateUserPassword(string mail, string password)
    {
        
        var user = await _userRepository.ReturnUser(mail);

        if (user == null)
        {
            return "User not found";
        }

        string passwordValidation = _userValidation.returnPasswordUpdate(password);
        if (passwordValidation.IsNullOrEmpty())
        {
            var cryptPassword = BCrypt.HashPassword(password);
            user.password = cryptPassword;
            await _userRepository.UpdateUserAsync(user);
            return "Password successfully updated";
        }
        return passwordValidation;
    }



    public async Task<string> checkForUserMail(string Mail)
    {
        string error = await  _userValidation.returnEmailExists(Mail);
        if (error.IsNullOrEmpty())
        {
            return "Email doesn't exist";
        }
        
        return error;
    }

    public async Task<string> loginUser(string mail, string password)
    {
        var user = await _userRepository.ReturnUser(mail);
        if (user == null)
        {
            return "User not found";
        }

        var passwordHash = await _userRepository.ReturnUserPassword(mail);
        if (!BCrypt.Verify(password, passwordHash))
        {
            return "Invalid password";
        }
        return "User successfully logged in";
    }
}