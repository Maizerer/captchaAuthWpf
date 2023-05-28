using EasyCaptcha.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TradeAppWpf.Models;

namespace TradeAppWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string captcha;
        public bool showCaptcha;
        public MainWindow()
        {
            InitializeComponent();
            showCaptcha = false;
            generateCaptcha();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (showCaptcha == true && captchaInputValue.Text != captcha)
                {
                    MessageBox.Show("Введите капчу корректно");
                    generateCaptcha();
                    return;
                }
                var user = TradeEntities.GetContext().Users.Where(looser => looser.UserLogin == loginInput.Text && looser.UserPassword == passwordInput.Password).FirstOrDefault();
                if (user != null)
                {
                    toProductsPage();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз");
                    generateCaptcha();
                    captchaInput.Visibility = Visibility.Visible;
                    loginCaptcha.Visibility = Visibility.Visible;
                    showCaptcha = true;
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Попробуйте позже");
            }
        }

        private void guestLoginBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void toProductsPage()
        {
            Windows.ProductsWindow productsWindow = new Windows.ProductsWindow();
            productsWindow.Show();
            this.Hide();
        }

        private void generateCaptcha()
        {
            loginCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
            captcha = loginCaptcha.CaptchaText;
            captchaInputValue.Text = "";
        }
    }
}
