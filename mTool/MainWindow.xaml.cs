using System;
using System.IO.Pipes;
using System.Windows;
using System.Windows.Input;


namespace mTool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            this.Left = 1;
            this.Top = 1;
            this.Topmost = true;
            this.ShowInTaskbar = false;


            string UserName = Shutter.GetUserName();

            Shutter.StartWaiter(UserName);

            //Shutter.StartShutter(UserName);

        }


        private void Viewbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            YesNo yn = new YesNo("Сбросить текущий сеанс?");

            yn.ShowDialog();
            
                if (yn.ReturnValue)
                {
                    //System.Diagnostics.Process.Start("Shutdown.exe", "-l");
                }

          }

    }
}
