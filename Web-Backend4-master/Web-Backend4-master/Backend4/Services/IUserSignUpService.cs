using System;

namespace Backend4.Services
{
    public interface IUserSignUpService
    {
        void CreateUser(String FirstName,
                        String LastName,
                        Int32 BirthdayDay,
                        Int32 BirthdayMonth,
                        Int32 BirthdayYear,
                        String Gender,
                        String Email,
                        String Password);

        Boolean UserExists(String FirstName,
                                  String LastName,
                                  Int32 BirthdayDay,
                                  Int32 BirthdayMonth,
                                  Int32 BirthdayYear,
                                  String Gender);
    }
}
