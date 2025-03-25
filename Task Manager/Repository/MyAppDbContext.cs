﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.IO;
using System.Windows;
using Task_Manager.Entities;

namespace Task_Manager.Repository;

public class MyAppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskJournal> TaskJournal { get; set; }
    public DbSet<UserTasks> UserTasks { get; set; }
    public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }

    public bool TestConnection()
    {
        try
        {
            return Database.CanConnect();
        }
        catch (Exception ex)
        {
            Console.Write($"{ex.Message}\n{ex.StackTrace}");
            return false;
        }
    }
}