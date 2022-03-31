using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia.Media;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab7.Models
{
    public class StudentData : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        IBrush background;
        public IBrush Background
        {
            get => background;
            set
            {
                background = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<GridCell> Cells { get; set; }
        string averageMark;
        public string AverageMark
        {
            get => averageMark;
            set
            {
                averageMark = value;
                if(value == "#ERROR")
                {
                    Background = Brushes.White;
                }
                else
                {
                    double val = Convert.ToDouble(value);
                    if (val < 1) Background = Brushes.Red;
                    else if (val < 1.5) Background = Brushes.Yellow;
                    else Background = Brushes.Green;
                }
                
                NotifyPropertyChanged();
            }
        }
        public bool IsChecked { get; set; }

        public StudentData(string name, List<string> marks)
        {
            Name = name;
            Cells = new ObservableCollection<GridCell>();
            foreach (var mark in marks)
                Cells.Add(new GridCell(mark));
            FindAverageMark();
            
        }
        public void FindAverageMark()
        {
            double avMark = 0;
            foreach (var cell in Cells)
            {
                int a;
                if (int.TryParse(cell.Mark, out a))
                    avMark += a;
                else
                {
                    AverageMark = "#ERROR";
                    return;
                }
            }
                
            AverageMark = (avMark / Cells.Count).ToString();
        }

    }
}
