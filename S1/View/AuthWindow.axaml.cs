using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S1.Classes;
using S1.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace S1.View;

public partial class AuthWindow : Window
{
    private TextBox login;
    private TextBox password;
    public static User curUser { get; set; }
    public AuthWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        login = this.FindControl<TextBox>("LoginTb");
        password = this.FindControl<TextBox>("PasswordTb");
    }

    private async void Auth(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(login.Text) && !string.IsNullOrWhiteSpace(password.Text))
        {
            var user = Context._con.Users.Include(p => p.Role)
                .Include(p => p.Department)
                .FirstOrDefault(p => p.Login == login.Text && p.Password == password.Text);
            if (user != null)
            {
                curUser = user;
                RecordTypeWindow recordTypeWindow = new RecordTypeWindow();
                recordTypeWindow.Show();
                this.Close();
            }
            /*HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.GetAsync($"http://localhost:5229/auth?login={login.Text}&password={password.Text}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(content);
                curUser = user;
                RecordTypeWindow recordTypeWindow = new RecordTypeWindow();
                recordTypeWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Внимание", "Пользователь не найден").Show();
            }*/
        } 
        else
        {
            
                MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Внимание", "Сначала заполните все поля!").Show();
        }
    }

    private void AuthAsGuest(object? sender, RoutedEventArgs e)
    {
        curUser = null;
        RecordTypeWindow recordTypeWindow = new RecordTypeWindow();
        recordTypeWindow.Show();
        this.Close();
    }
}