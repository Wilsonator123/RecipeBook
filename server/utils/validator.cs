namespace server.utils;

public static class ValidatorHelper
{
    public static bool ValidateEmail(string email)
    {
        return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
    }

    public static bool ValidatePassword(string password)
    {
        return password.Length >= 8;
    }
}