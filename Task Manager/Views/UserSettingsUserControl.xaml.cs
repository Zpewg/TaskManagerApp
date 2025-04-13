using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Entities;
using Task_Manager.Repository;
using Task_Manager.Service;

namespace Task_Manager.Views;

public partial class UserSettingsUserControl : UserControl
{
    private User _user;
    private UserService _userService;
    public UserSettingsUserControl(User user)
    {
        InitializeComponent();
        _user = user;
         _userService = App.ServiceProvider.GetRequiredService< UserService >() ;
         this.DataContext = new ChangeUsernameOrEmail(_userService, _user);

    }
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        var parentWindow = Window.GetWindow(this) as TasksWindow;
        if (parentWindow != null)
        {
            parentWindow.UserSettingsContainer.Visibility = Visibility.Collapsed;
            parentWindow.UserSettingsContainer.Content = null;
        }
    }
    private void RecoverAccountButton_Click(object sender, RoutedEventArgs e)
    {
        RecoverAccountWindow recoverAccountWindow = new RecoverAccountWindow(_user);
        recoverAccountWindow.Show();
    }

    private void ActionPopUpButton1_Click(object sender, RoutedEventArgs e)
    {
        TaskActionPopup1.IsOpen = true;
    }

    private void ActionPopUpButton_OnClick(object sender, RoutedEventArgs e)
    {
        TaskActionPopup.IsOpen = true;
    }

    private void ChangeUserNameButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (ChangeUsernameOrEmail)this.DataContext;
        viewModel.ChangeUserName();
    }

    private void ChangeMailButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel =(ChangeUsernameOrEmail)this.DataContext;
        viewModel.ChangeEmail();
    }
    private void Popup_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (!TaskActionPopup.IsMouseOver && !TaskActionPopup1.IsMouseOver)
        {
            TaskActionPopup.IsOpen = false;
            TaskActionPopup1.IsOpen = false;
        }
    }
}