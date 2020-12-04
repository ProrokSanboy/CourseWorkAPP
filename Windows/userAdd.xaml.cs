using CourseWorkAPP.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWorkAPP.Windows
{
    /// <summary>
    /// Логика взаимодействия для userAdd.xaml
    /// </summary>
    public partial class userAdd : Window
    {
        Clients myClients { get; set; }

        public userAdd(Clients c)
        {
            InitializeComponent();
            myClients = c;

            userRole.Items.Add("Администратор");
            userRole.Items.Add("Менеджер");
            userRole.Items.Add("Редактор");

            userMale.Items.Add("Мужской");
            userMale.Items.Add("Женский");

            if (c.id != 0)
            {
                actionButton.Content = "Сохранить";
                this.Title = "Изменить";
                userActive.IsChecked = true;

                userName.Text = myClients.Profile.name;
                userPhone.Text = myClients.Profile.phone;
                userPassport.Text = myClients.Profile.passport;

                if(myClients.Profile.active == true)
                    userActive.IsChecked = true;
                else
                    userActive.IsChecked = false;

                if (myClients.role_id == 1) userRole.SelectedValue = "Администратор";
                if (myClients.role_id == 2) userRole.SelectedValue = "Менеджер";
                if (myClients.role_id == 3) userRole.SelectedValue = "Редактор";

                if (myClients.Profile.male == true) userMale.SelectedValue = "Мужской";
                else userMale.SelectedValue = "Женский";

                userBirthdate.SelectedDate = myClients.Profile.birthdate;
                userLogin.Text = myClients.login;
                userPassword.Text = myClients.password;
            }
            else
            {
                userRole.SelectedValue = "Редактор";
                userMale.SelectedValue = "Мужской";
                userBirthdate.SelectedDate = DateTime.Now;
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Profile profile;
            if (myClients.id == 0) profile = new Profile();
            else profile = myClients.Profile;

            if (userName.Text.Length > 0)
            {
                profile.name = userName.Text;
                profile.birthdate = (DateTime)userBirthdate.SelectedDate;
                if (userPhone.Text.Length > 0)
                {
                    profile.phone = userPhone.Text;
                    if (userPassport.Text.Length > 0)
                    {
                        if (userMale.SelectedItem.ToString() == "Мужской") profile.male = true;
                        if (userMale.SelectedItem.ToString() == "Женский") profile.male = false;
                        profile.passport = userPassport.Text;

                        profile.active = userActive.IsChecked;
                        profile.avatar = new byte[0];
                        if (userLogin.Text.Length > 0)
                        {
                            myClients.login = userLogin.Text;
                            if (userPassword.Text.Length > 0)
                            {
                                myClients.password = userPassword.Text;

                                if (userRole.SelectedItem.ToString() != "")
                                {
                                    if (userRole.SelectedItem.ToString() == "Администратор") myClients.role_id = 1;
                                    if (userRole.SelectedItem.ToString() == "Менеджер") myClients.role_id = 2;
                                    if (userRole.SelectedItem.ToString() == "Редактор") myClients.role_id = 3;
                                    
                                    if(profile.id == 0)
                                    {
                                        myClients.Profile = Helper.Connection.Profile.Add(profile);
                                        Helper.Connection.SaveChanges();
                                        
                                    }
                                    if (myClients.id == 0)
                                    {
                                        Helper.Connection.Clients.Add(myClients);
                                    }
                                    Helper.Connection.SaveChanges();
                                    Helper.navigationService.Navigate(new usersList());
                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
