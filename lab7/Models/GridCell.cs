using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab7.Models
{
    public class GridCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        string mark;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GridCell(string mark)
        {
            Mark = mark;
        }
        public IBrush Background { get; set; }
        public string Mark
        {
            get => mark;
            set
            {
                mark = value;
                double a;
                if (!double.TryParse(mark, out a))
                {
                    mark = "#ERROR";
                    Background = Brushes.White;
                }
                else
                {
                    if (a >= 0 && a < 1)
                        Background = Brushes.Red;
                    else if (a >= 1 && a < 1.5)
                        Background = Brushes.Yellow;
                    else if (a >= 1.5 && a <= 2)
                        Background = Brushes.Green;
                    else
                    {
                        mark = "#ERROR";
                        Background = Brushes.White;
                    }

                }
                NotifyPropertyChanged();
            }
        }
    }
}
