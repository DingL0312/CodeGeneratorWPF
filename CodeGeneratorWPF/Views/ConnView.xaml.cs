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
using CodeGeneratorWPF.ViewModel;

namespace CodeGeneratorWPF.Views
{
    /// <summary>
    /// ConnView.xaml 的交互逻辑
    /// </summary>
    public partial class ConnView : UserControl
    {
        public ConnView()
        {
            InitializeComponent();
        }
        private void Test_Conn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("连接成功");
        }

        private void Confirm_Cick(object sender, RoutedEventArgs e)
        {
            StringBuilder connSb = new();
            SQLHelper sqlHelper = new SQLHelper(HostInput.Text,UserInput.Text,PwdInput.Text);
        }
    }
}
