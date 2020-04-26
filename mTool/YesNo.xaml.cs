using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mTool
{
    /// <summary>
    /// Логика взаимодействия для YesNo.xaml
    /// </summary>
    public partial class YesNo : Window
    {
        public bool ReturnValue;
        public YesNo(string Capt)
        {
            InitializeComponent();
            this.Question.Text = Capt;
        }
       
        private void No_Click(object sender, RoutedEventArgs e)
        {
            ReturnValue = false;
            this.Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            ReturnValue = true;
            this.Close();
        }

        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }
    }
}
