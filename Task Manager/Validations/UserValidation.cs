using System.Text.RegularExpressions;

namespace Task_Manager.Validations;
using Task_Manager.Entities;

public class UserValidation
{
    public UserValidation(){}
    
    public List<string> Validate(User user)
    {
        Regex regexMail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                    + "@"
                                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        //Only phonenumbers from romania are accepted
        Regex regexPhone = new Regex(@"^07\d{8}$");
        //All characters from beggining to end must be letters or numbers
        Regex regexUserName = new Regex("^[a-zA-Z0-9]+$");
        //Must contain atleast one digit, one lower case character, one upper case ch and one special character
        //The min length is 8 the max length is 15
        Regex regexPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$");
        List<string> errors = new List<string>();

        if (!regexMail.IsMatch(user.email))
        {
            errors.Add("Email is not valid");
        }

        if (!regexPhone.IsMatch(user.phoneNumber))
        {
            errors.Add("Phone number is not valid");
        }

        if (!regexUserName.IsMatch(user.name))
        {
            errors.Add("Username is not valid");
        }

        if (!regexPassword.IsMatch(user.password))
        {
            errors.Add("Password is not valid");
        }
        
        
        return errors;
    }
}