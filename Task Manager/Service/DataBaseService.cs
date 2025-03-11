using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Repository;

namespace Task_Manager.Service;

public class DatabaseService
{
    private readonly MyAppDbContext _context;

    public DatabaseService(MyAppDbContext context)
    {
        _context = context;
    }

    public bool TestConnection()
    {
        try
        {
            return _context.Database.CanConnect();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Eroare la conectare: {ex.Message}");
            return false;
        }
    }
}