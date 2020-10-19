using System;
using System.Collections.Generic;
using System.IO;
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
using LeftRecElim.Logics;
using Microsoft.Win32;

namespace LeftRecElim
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logic _logic;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(ofd.FileName))
                {
                    var fileContent = reader.ReadToEnd();
                    var a = fileContent.Split(new [] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

                    var vt = a[0].Split(new []{','}, StringSplitOptions.RemoveEmptyEntries);


                    var vn = a[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    VtTextBox.Text = "ϵ" + Environment.NewLine;
                    foreach (var s in vt)
                    {
                        VtTextBox.Text += s + Environment.NewLine;
                    }
                    VnTextBox.Text = "";
                    foreach (var s in vn)
                    {
                        VnTextBox.Text += s + Environment.NewLine;
                    }

                    PTextBox.Text = "";
                    RTextBox.Text = "";
                    for (int i = 2; i < a.Length; i++)
                    {
                        PTextBox.Text += a[i] + Environment.NewLine;
                    }
                }
            }

        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _logic = new Logic();
            string s = VtTextBox.Text;
            _logic.AddSymbols(s, true);
            s = VnTextBox.Text;
            _logic.AddSymbols(s, false);
            s = PTextBox.Text;
            _logic.AddRules(s);
            _logic.ElimTest();
            //_logic.EliminateRecursion();

            RTextBox.Text = "";
            var res = _logic.GetRules();
            foreach (var re in res)
            {
                RTextBox.Text += re;
                RTextBox.Text += Environment.NewLine;
            }
        }
    }
}
