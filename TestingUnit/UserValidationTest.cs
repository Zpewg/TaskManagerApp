using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Repository;

namespace TestingUnit;

using Task_Manager.Entities;
using Task_Manager.Service;
using Task_Manager.Validations;

public class UserValidationTest
{
   private readonly UserValidation _userValidation;
    private IServiceProvider ServiceProvider{get; set;}
    public UserValidationTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<MyAppDbContext>();
        serviceCollection.AddScoped<UserRepository>();
        serviceCollection.AddScoped<UserService>();
        serviceCollection.AddScoped<UserValidation>();
        ServiceProvider = serviceCollection.BuildServiceProvider();
        _userValidation = ServiceProvider.GetService<UserValidation>();
    }
    //Expected to succed for the given user
    [Fact]
    public async Task ValidUserTest()
    {
        var user = new User("Andrei", "Andrei@mail.com", "Andremail123!", "0712345678");
        Assert.Empty(await _userValidation.Validate(user));
    }

    [Theory]
    [InlineData("Andreimail.com")]
    [InlineData("Andrei@mailcom")]
    public async Task WrongMailTest(string email)
    {
        var user2 = new User("Andrei", email, "Andremail123!", "0712367890");
        List<string> test = await _userValidation.Validate(user2);
        Assert.Contains(test, x => x.Contains("Email is not valid"));
        
    }
    
    [Fact]
    public async Task AlreadyTakenMailTest()
    {
        var user2 = new User("Andrei", "ceva@gmail.com", "Andremail123!", "0712345678");
        List<string> test =await _userValidation.Validate(user2);
        Assert.Contains(test, x => x.Contains("Email is already taken"));
    }

    [Fact]
    public async Task AlreadyTakenPhoneNumber()
    {
        var user = new User("Andrei", "zpwg@gmail.com", "Andreimail123!", "0753969716");
        List<string> test = await _userValidation.Validate(user);
        Assert.Contains(test, x => x.Contains("Phone number is already taken"));
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
    public async Task WrongPasswordTest(string wrongPassword)
    {
        var user3 = new User("Andrei", "Andrei@mail.com", wrongPassword, "0712345678");
        List<string> test = await _userValidation.Validate(user3);
        Assert.Contains(test, x => x.Contains("Password is not valid"));
        
    }
    
    //Only phone numbers from romania are accepted.
    [Theory]
    [InlineData("071234567")]
    [InlineData("1712345678")]
    [InlineData("0912345678")]
    public async Task WrongPhoneNumberTest(string wrongPhoneNumber)
    {
        var user4 = new User("Adnrei", "Andrei@gmail.com", "Asdasdasd123!", wrongPhoneNumber);
        List<string> test = await _userValidation.Validate(user4);
        Assert.Contains(test, x => x.Contains("Phone number is not valid"));
    }
}