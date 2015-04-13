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
using System.Threading;
using System.Windows.Threading; // required for dispatcher

namespace Text_Append_Delegate
{

    public delegate void TextAppendDelegate(string text);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextAppendDelegate appTxt;
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public void TextAppend(string text)
        {
            // invoke not included in wpf so we use dispatcher
            // Note that CheckAccess() is hidden in intellisense!! 
            if (this.TxtBox.Dispatcher.CheckAccess())
            {
                this.TxtBox.Text = text;
            }
            else
            {
                this.TxtBox.Dispatcher.BeginInvoke(new TextAppendDelegate(TextAppend), text);
            }

            // The following is an example of how to do the same thing in windows forms
            //if (this.TxtBox.InvokeRequired)
            //{
            //    this.TxtBox.Invoke(TextAppend, text);
            //}
            //else this.TxtBox.Text = text;
        }
        public void RealAppend(string text)
        {
            this.TxtBox.AppendText(text);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            test myClass = new test(appTxt);
            Thread myThread = new Thread(myClass.sendText);
            myThread.Start("sample string");
            if(myThread.IsAlive) myThread.Join();
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            appTxt = new TextAppendDelegate(TextAppend);

        }
    }

}
