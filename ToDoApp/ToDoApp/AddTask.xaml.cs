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

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public Task Result{ get; set; }
        public AddTask()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (TaskBox.Text != "")
            {
                Task t = new Task();
                t.Name = TaskBox.Text;
                t.Description = Description.Text;
                t.isCompleted = check.IsChecked.Value;
                Result = t;
                DialogResult = true;
            }
            else
            {
                TaskBox.Background = Brushes.Red;
            }
        }
    }
}
