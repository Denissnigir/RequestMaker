using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using S1.Classes;
using S1.Model;

namespace S1.View;

public partial class SoloRecordWindow : Window
{
    private Grid guest;
    private Grid employee;
    private ComboBox departmentCb;
    private ComboBox employeeCb;
    private User employeeData;
    private User guestData;
    private Request requestData;
    private CalendarDatePicker startDate;
    private CalendarDatePicker endDate;
    private ComboBox purposeCb;
    private Grid main;
    public SoloRecordWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        departmentCb.Items = Context._con.Departments.ToList();
        if (AuthWindow.curUser != null)
        {
            if (AuthWindow.curUser.RoleId == 2)
            {
                guest.DataContext = AuthWindow.curUser;
            }
            else
            {
                employee.DataContext = AuthWindow.curUser;
                departmentCb.SelectedItem = AuthWindow.curUser.Department;
                employeeCb.SelectedItem = AuthWindow.curUser;
                guestData = new User();
                guest.DataContext = guestData;
            }
        }

        purposeCb.Items = new List<string>()
        {
            "Цель1",
            "Цель2",
            "Цель3"
        };
        requestData = new Request();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        employee = this.FindControl<Grid>("EmployeeGrid");
        guest = this.FindControl<Grid>("GuestGrid");
        departmentCb = this.FindControl<ComboBox>("DepartmentCb");
        employeeCb = this.FindControl<ComboBox>("EmployeeCb");
        startDate = this.FindControl<CalendarDatePicker>("StartDatePicker");
        endDate = this.FindControl<CalendarDatePicker>("EndDatePicker");
        purposeCb = this.FindControl<ComboBox>("PurposeCb");
        main = this.FindControl<Grid>("MainGrid");
    }

    private void UploadPhoto(object? sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowAsync(this).IsCompletedSuccessfully)
        {
        }
    }

    private void DepartmentCb_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Filter();
    }

    private void Filter()
    {
        if (departmentCb.SelectedItem is Department dep)
        {
            var employees = Context._con.Users.Include(p => p.Department).ToList()
                .Where(p => p.RoleId == 1 && p.Department == dep)
                .ToList();
            employeeCb.Items = employees;
        }
    }

    private void NewRequest(object? sender, RoutedEventArgs e)
    {
        if (EmptyChecker(main) == 0)
        {
            if (AuthWindow.curUser != null)
            {
                if (AuthWindow.curUser.RoleId == 2)
                {
                    requestData.DateStart = startDate.SelectedDate.ToString() ?? "aaa";
                    requestData.DateEnd = endDate.SelectedDate.ToString() ?? "aaa";
                    requestData.GuestId = AuthWindow.curUser.Id;
                    requestData.EmployeeId = (employeeCb.SelectedItem as User).Id;
                    requestData.Purpose = purposeCb.SelectedItem.ToString();
                    requestData.Id = Context._con.Requests.OrderBy(p => p.Id)
                        .Last().Id + 1;
                    Context._con.Requests.Add(requestData);
                    Context._con.SaveChanges();
                }
                else
                {
                    guestData.Id = Context._con.Users.OrderBy(p => p.Id)
                        .Last().Id + 1;
                    guestData.RoleId = 2;
                    Context._con.Users.Add(guestData);
                    Context._con.SaveChanges();
                    requestData.DateStart = startDate.SelectedDate.ToString() ?? "aaa";
                    requestData.DateEnd = endDate.SelectedDate.ToString() ?? "aaa";
                    requestData.GuestId = guestData.Id;
                    requestData.EmployeeId = (employeeCb.SelectedItem as User).Id;
                    requestData.Purpose = purposeCb.SelectedItem.ToString();
                    requestData.Id = Context._con.Requests.OrderBy(p => p.Id)
                        .Last().Id + 1;
                    Context._con.Requests.Add(requestData);
                    Context._con.SaveChanges();
                }
            }
            else
            {
            
            }
        }
        else
        {
            Console.WriteLine("HUY");
        }
        
    }
    
    private int EmptyChecker(Control element)
    {
        int emptyCount = 0;
        if (element is Grid grid)
        {
            foreach (var item in grid.Children)
            {
                emptyCount += EmptyChecker(item as Control);
            }
        }

        if (element is StackPanel stack)
        {
            foreach (var item in stack.Children)
            {
                emptyCount += EmptyChecker(item as Control);
            }
        }

        if (element is TextBox box)
        {
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                emptyCount += 1;
            }
        }

        return emptyCount;
    }
}