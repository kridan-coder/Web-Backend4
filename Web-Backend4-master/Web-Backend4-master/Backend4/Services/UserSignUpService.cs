using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Backend4.Services
{
    public sealed class UserSignUpService : IUserSignUpService
    {
        private readonly ILogger logger;
        private readonly List<UserData> users = new List<UserData>(){ new UserData("SuckityCockity", "a", 1, 11, 2011, "Male", "da@mail.ru", "123" )};


        public UserSignUpService(ILogger<IPasswordResetService> logger)
        {
            this.logger = logger;
        }


        public void CreateUser(String FirstName, String LastName, Int32 BirthdayDay, Int32 BirthdayMonth, Int32 BirthdayYear, String Gender, String Email, String Password)
        {
            lock (this.users)
            {
                this.logger.LogInformation($"Adding user {FirstName} {LastName}");
                this.users.Add(new UserData(FirstName,
                                            LastName,
                                            BirthdayDay,
                                            BirthdayMonth,
                                            BirthdayYear,
                                            Gender,
                                            Email,
                                            Password));
            }
        }

        public Boolean UserExists(String FirstName, String LastName, Int32 BirthdayDay, Int32 BirthdayMonth, Int32 BirthdayYear, String Gender)
        {
            lock (this.users)
            {
                this.logger.LogInformation($"Checking if user {FirstName} {LastName} already exists");
                return this.users.Any(x => 
                x.FirstName == FirstName &&
                x.LastName == LastName &&
                x.BirthdayDay == BirthdayDay &&
                x.BirthdayMonth == BirthdayMonth && 
                x.BirthdayYear == BirthdayYear && 
                x.Gender == Gender);
            }
        }

        private sealed class UserData
        {
            public UserData(String FirstName, String LastName, Int32 BirthdayDay, Int32 BirthdayMonth, Int32 BirthdayYear, String Gender, String Email, String Password)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.BirthdayDay = BirthdayDay;
                this.BirthdayMonth = BirthdayMonth;
                this.BirthdayYear = BirthdayYear;
                this.Gender = Gender;
                this.Email = Email;
                this.Password = Password;
            }

            public String FirstName { get; }

            public String LastName { get; }

            public Int32 BirthdayDay { get; }
            public Int32 BirthdayMonth { get; }
            public Int32 BirthdayYear { get; }

            public String Gender { get; }

            public String Email { get; }
            public String Password { get; }
        }
    }
}