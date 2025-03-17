using Microsoft.Testing.Platform.Extensions.Messages;
using Moq;
using Task_Manager.Repository;

namespace TestingUnit;

using Task_Manager.Entities;
using Task_Manager.Service;
using Task_Manager.Validations;

public class UnitTest1
{
    //Expected to succed for the given user
    [Fact]
    public void ValidUserTest()
    {
        var user = new User("Andrei", "Andrei@mail.com", "Andremail123!", "0712345678");

        UserValidation userValidation = new UserValidation();

        Assert.Empty(userValidation.Validate(user));
    }

    [Theory]
    [InlineData("Andreimail.com")]
    [InlineData("Andrei@mailcom")]
    public void WrongMailTest(string email)
    {
        var user2 = new User("Andrei", email, "Andremail123!", "0712367890");
        UserValidation userValidation = new UserValidation();
        List<string> test = userValidation.Validate(user2);
        Assert.Contains(test, x => x.Contains("Email is not valid"));
        
    }
    
    [Theory]
    //no capital letter
    [InlineData("asdasd123!")]
    //no small letter
    [InlineData("ASDASD123!")]
    //no special character
    [InlineData("Asdasd123")]
    //<8 characters
    [InlineData("Asdas1!")]
    //>16 characters
    [InlineData("ASDASDASDASDASDASDasdasd123!")]
    //no number in password
    [InlineData("asdASDasd!")]
    public void WrongPasswordTest(string wrongPassword)
    {
        var user3 = new User("Andrei", "Andrei@mail.com", wrongPassword, "0712345678");
        UserValidation userValidation = new UserValidation();
        List<string> test = userValidation.Validate(user3);
        Assert.Contains(test, x => x.Contains("Password is not valid"));
        
    }
    
    //Only phone numbers from romania are accepted.
    [Theory]
    [InlineData("071234567")]
    [InlineData("1712345678")]
    [InlineData("0912345678")]
    public void WrongPhoneNumberTest(string wrongPhoneNumber)
    {
        var user4 = new User("Adnrei", "Andrei@gmail.com", "Asdasdasd123!", wrongPhoneNumber);
        UserValidation userValidation = new UserValidation();
        List<string> test = userValidation.Validate(user4);
        Assert.Contains(test, x => x.Contains("Phone number is not valid"));
    }
}