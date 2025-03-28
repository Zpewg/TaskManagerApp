using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Repository;
using Task_Manager.Service;

namespace Task_Manager.Validations;
using Task_Manager.Entities;
public class UserValidation
{
    private readonly UserRepository _userRepository;

    public UserValidation(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public  Regex regexMail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                        + "@"
                                        + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
    //Only phonenumbers from romania are accepted
    public Regex regexPhone = new Regex(@"^07\d{8}$");
    
    //All characters from beggining to end must be letters or numbers
    public Regex regexUserName = new Regex("^[a-zA-Z0-9]{1,20}$");
    
    //Must contain atleast one digit, one lower case character, one upper case chcd  and one special character
    //The min length is 8 the max length is 15
    public Regex regexPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$");
    
    public List<User> users = new List<User>();
    
    public List<string> errors = new List<string>();
    
    
    public async  Task<List<string>> Validate(User user)
    {
        users = await _userRepository.GetUsersAsync();
        returnMailError(user);
        returnPasswordError(user);
        returnUserNameError(user);
        returnPhoneError(user);
        returnEmailAlreadyTaken(user);
        returnPhoneNumberAlreadyTaken(user);
            
        return errors;
    }

    public void returnMailError(User user)
    {
        if (!regexMail.IsMatch(user.email))
        {
            errors.Add("Email is not valid");
        }
    }

    public void returnPhoneError(User user)
    {
        if (!regexPhone.IsMatch(user.phoneNumber))
        {
            errors.Add("Phone number is not valid");
        }
    }

    public void returnUserNameError(User user)
    {
        if (!regexUserName.IsMatch(user.name))
        {
            errors.Add("Username is not valid");
        }
    }

    public void returnPasswordError(User user)
    {
        
        if (!regexPassword.IsMatch(user.password))
        {
            errors.Add("Password is not valid");
        }
    }

    public void returnEmailAlreadyTaken(User user)
    {
        if (users.Where(u=> u.email.Equals(user.email)).Count() > 0)
        {
            errors.Add("Email is already taken");
        }
    }

    public void returnPhoneNumberAlreadyTaken(User user)
    {
        if (users.Where(u => u.phoneNumber.Equals(user.phoneNumber)).Count() > 0)
        {
            errors.Add("Phone number is already taken");
        }

    }

    public async Task<string> returnEmailExists(string email)
    {
        users = await _userRepository.GetUsersAsync();
        if (users.Where(u => u.email.Equals(email)).Count() > 0)
        {
            return "Email exists!";
        }
     
        return string.Empty;
    }

    public string returnPasswordUpdate(string password)
    {
        if (!regexPassword.IsMatch(password))
        {
            return "Password is not valid";
        }
        return string.Empty;
    }
    
}