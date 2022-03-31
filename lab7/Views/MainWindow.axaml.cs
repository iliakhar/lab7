using Avalonia.Controls;
using System;
using Avalonia.Interactivity;
using lab7.Models;
using lab7.ViewModels;
using System.Collections.Generic;
using ReactiveUI;

namespace lab7.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
           
        }
        
        private void FindAverage(object sender,  RoutedEventArgs e)
        {
            var box = sender as TextBox;
            var student = box.DataContext as StudentData;
            student.FindAverageMark();

        }

        private void dataGrid_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            //int selectInd = (sender as DataGrid).SelectedIndex;
            ((sender as DataGrid).SelectedItem as StudentData).FindAverageMark();
            var st = (sender as DataGrid).SelectedItem as StudentData;
            //context.Data.Add(st);
            //(sender as DataGrid).;

        }
        private async void AboutClick(object sender, RoutedEventArgs e)
        {

            await new AboutDiagView().ShowDialog<string?>((Window)this.VisualRoot);
        }
    }
}
