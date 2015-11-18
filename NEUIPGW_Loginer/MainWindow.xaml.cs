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

using System.Net;

namespace NEUIPGW_Loginer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public ipgw ipgwc = new ipgw();

        // 点击“连接网络”按钮后的操作
        private void loginButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (Username_Box.Text.Equals("") || Username_Box_Copy.Password.Equals(""))
            {
                MessageBox.Show("用户名或密码为空", "连接失败");
                return;
            }
            ipgwc.username_doer = Username_Box.Text;
            ipgwc.password_doer = Username_Box_Copy.Password;
            ipgwc.ipgwi("connect");
            if (ipgwc.inf["SUCCESS"] != "YES")
            {
                MessageBox.Show("用户名密码不正确，或连接数超过1", "连接失败");
                return;
            }
            MessageBox.Show("状态：已连接\n用户："+ipgwc.inf["USERNAME"]+"\n包月状态："+ipgwc.inf["FIXRATE"]+"\n访问范围：国内\n欠费状态："+ipgwc.inf["DEFICIT"]+"\n余额："+ipgwc.inf["BALANCE"]+"\nIP地址："+ipgwc.inf["IP"],"连接成功");
            return;
        }

        // 点击"断开连接"
        private void loginButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            ipgwc.ipgwi("disconnect");
            if (ipgwc.inf["SUCCESS"] != "YES")
            {
                MessageBox.Show("发生错误", "断开连接失败");
                return;
            }
            MessageBox.Show("IP地址：" + ipgwc.inf["IP"], "断开连接成功");
            return;
        }

        // 断开全部连接
        private void loginButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            if (Username_Box.Text.Equals("") || Username_Box_Copy.Password.Equals(""))
            {
                MessageBox.Show("用户名或密码为空", "断开全部连接失败");
                return;
            }
            ipgwc.username_doer = Username_Box.Text;
            ipgwc.password_doer = Username_Box_Copy.Password;
            if (Username_Box.Text.Equals("") || Username_Box_Copy.Password.Equals(""))
            {
                MessageBox.Show("用户名或密码为空", "断开全部连接失败");
                return;
            }
            ipgwc.ipgwi("disconnectall");
            if (ipgwc.inf["SUCCESS"] != "YES")
            {
                MessageBox.Show("发生错误", "断开全部连接失败");
                return;
            }
            MessageBox.Show("IP地址：" + ipgwc.inf["IP"], "断开全部连接成功");
            return;
        }
    }
}
