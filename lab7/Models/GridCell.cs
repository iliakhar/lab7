using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace lab7.Models
{
    public class GridCell
    {
        string mark;
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
                switch (value)
                {
                    case "0":
                        Background = Brushes.Red;
                        break;
                    case "1":
                        Background = Brushes.Yellow;
                        break;
                    case "2":
                        Background = Brushes.Green;
                        break;
                    default:
                        mark = "#ERROR";
                        Background = Brushes.White;
                        break;
                }
            }
        }
    }
}
