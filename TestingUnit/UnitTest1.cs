using Microsoft.Testing.Platform.Extensions.Messages;
using Moq;
using Task_Manager.Repository;

namespace TestingUnit;

using Task_Manager.Entities;
using Task_Manager.Service;
using Task_Manager.Validations;

public class UnitTest1
{
    [Fact]
    public void UserTest()
    {
        var user = new User("Andrei", "Andrei@mail.com", "Andremail123!", "0712345678");

        UserValidation userValidation = new UserValidation();

        Assert.Empty(userValidation.Validate(user));
    }

    [Fact]
    public void NotEmptyList()
    {
        var user2 = new User("Andrei", "Andreimail.com", "ndremail123!", "07123678");
        var user3 = new User("Andrei", "Andrei@mail.com", "ANDREMASDSAA123!", "07345678");
        var user4 = new User("Andrei", "Andreimail.com", "Al123!", "0712345678123");
        var user5 = new User("Andrei", "Andrei@mail.com", "Andremail123!", "5712345678");
        var user6 = new User("Andrei", "Andreimail.com", "Andremail123!", "0412345678");
        List<User> users = new List<User> { user2, user3, user4, user5, user6 };
        UserValidation userValidation = new UserValidation();

        foreach (var useri in users)
        {
            Assert.NotEmpty(userValidation.Validate(useri));
        }
    }
}