using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using server.database;
using server.utils;

namespace server.user;

public static class User
{
    public static async Task<Response> GetUser(string userid)
    {
        List<Object[]> rows;
        

        rows = await Database.Query("getUser", [userid]);

        
        if (rows == null || rows.Count == 0)
        {
            return new Response(false, "User not found");
        }
        
        return new Response(true, "User found", rows[0]);
    }

    public static async Task<Response> CreateUser(string email, string fname, string lname, string password)
    {
        if (!ValidatorHelper.ValidateEmail(email))
        {
            return new Response(false, "Invalid email");
        }
        // Validate password
        if (!ValidatorHelper.ValidatePassword(password))
        {
            return new Response(false, "Invalid password");
        }
        
        var emailCheck = await Database.Query("isEmail", email);
        
        if (emailCheck.Count > 0)
        {
            
            return new Response(false, "Email already taken");
        }
        
        string userId = await GUID.GenerateGUID();
        
        string hashedPassword = Hash.HashPassword(password);
        
       
        
        List<Object[]> response = await Database.Query("addUser", [userId, email, fname, lname]);
        
        if ((await Database.Query("getUser", userId))[0][0].ToString() != "0")
        {
            await Database.Query("addPassword", [userId, hashedPassword]);
            return new Response(true, "User created", userId);
        }

        return new Response(false, "User not created");

    }

    public static async Task<Response> Login(string email, string password)
    {
        // Validate email
        
        if (!ValidatorHelper.ValidateEmail(email))
        {
            return new Response(false, "Invalid email");
        }
        
        // Get user
        
        List<Object[]> user = await Database.Query("getUserByEmail", email);
        
        if (user.Count == 0)
        {
            return new Response(false, "User not found");
        }
        
        // Get password
        
        List<Object[]> passwordHash = await Database.Query("getPassword", user[0][0].ToString());
        
        if (passwordHash.Count == 0)
        {
            return new Response(false, "User not found");
        }
        
        if (!Hash.VerifyPassword(password, passwordHash[0][0].ToString()))
        {
            return new Response(false, "Invalid password");
        }
        
        return new Response(true, "User logged in", user[0][0].ToString());
    }
}

