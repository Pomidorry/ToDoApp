using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Task> taskList = new ObservableCollection<Task>();
        public MainWindow()
        {
            InitializeComponent();
            //Task t1 =  new Task();
            //t1.Name = "Вивчити C#";
            //t1.isCompleted = false;
            //t1.Description = "Пройти курс по C#";

            //Task t2 = new Task();
            //t2.Name = "Написати резюме";
            //t2.isCompleted = false;
            //t2.Description = "Попросити допомогти написати";

            //Task t3 = new Task();
            //t3.Name = "Продати діда";
            //t3.isCompleted = false;
            //t3.Description = "Піти на базар";

            //taskList.Add(t1);
            //taskList.Add(t2);
            //taskList.Add(t3);

            Tasks.ItemsSource = taskList;
            Tasks.DisplayMemberPath = "Name";
        }

        private void Tasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Task selected = Tasks.SelectedItem as Task;
            if(selected != null)
            {
                MessageBox.Show(selected.Description,"Description",MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            AddTask window = new AddTask();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (window.ShowDialog()==true)
            {
                Task newTask = window.Result;
                taskList.Add(newTask);
            }
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            int index = Tasks.SelectedIndex;
            if (index != -1)
            {
                taskList[index].isCompleted=true;
            }
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            int index = Tasks.SelectedIndex;
            if(index != -1)
            {
                taskList.RemoveAt(index);
            }
        }

        private void R1_Checked(object sender, RoutedEventArgs e)
        {
            Tasks.ItemsSource = taskList;
        }

        private void R2_Checked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Task> filtered = new ObservableCollection<Task>();
            for(int i=0; i<taskList.Count; i++)
            {
                Task task = taskList[i];
                if (task.isCompleted == false)
                {
                    filtered.Add(task);
                }
            }
            Tasks.ItemsSource = filtered;
        }

        private void R3_Checked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Task> filtered = new ObservableCollection<Task>();
            for (int i = 0; i < taskList.Count; i++)
            {
                Task task = taskList[i];
                if (task.isCompleted == true)
                {
                    filtered.Add(task);
                }
            }
            Tasks.ItemsSource = filtered;
        }
        string fileName = "data.bin";

        private void Window_Closed(object sender, EventArgs e)
        {
            BinaryFormatter bin = new BinaryFormatter();
            Stream file = File.OpenWrite(fileName);
            bin.Serialize(file, taskList);
            file.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(fileName))
            {
                BinaryFormatter bin = new BinaryFormatter();
                Stream file = File.OpenRead(fileName);
                taskList = bin.Deserialize(file) as ObservableCollection<Task>;
                file.Close();
                Tasks.ItemsSource = taskList;
            }
        }
    }
}
