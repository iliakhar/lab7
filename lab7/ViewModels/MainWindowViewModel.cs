using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ReactiveUI;
using System.Reactive;
using lab7.Models;

namespace lab7.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private StudentData averageLessons;
        public StudentData AverageLessons
        {
            get => averageLessons;
            set => this.RaiseAndSetIfChanged(ref averageLessons, value);
        }
        public ObservableCollection<StudentData> Data { get; set; }
        public MainWindowViewModel()
        {
            /*Data = new ObservableCollection<StudentData> { new StudentData("Ибрагим", new List<string> { "0", "0", "2", "1" }),
                                                          new StudentData("Нуржан", new List<string> { "1", "2", "2", "1" }),
                                                          new StudentData("Ербол", new List<string> { "2", "0", "0", "0" }),
            };*/
            Data = new ObservableCollection<StudentData>();
            AverageLessons = new StudentData("", new List<string> { "0", "0", "0", "0", "0" });
            LoadTable();
            FindAverageLessons();

            Exit = ReactiveCommand.Create(() => Environment.Exit(0));
            Save = ReactiveCommand.Create(() => SaveTable());
            Load = ReactiveCommand.Create(() => LoadTable());
            AddStudent = ReactiveCommand.Create(() => AddStudentToData());
            DellStudent = ReactiveCommand.Create(() => DellStudentFromData());
        }
        
        public ReactiveCommand<Unit, Unit> Exit { get; }
        public ReactiveCommand<Unit, int> Save { get; }
        public ReactiveCommand<Unit, int> Load { get; }
        public ReactiveCommand<Unit, int> AddStudent { get; }
        public ReactiveCommand<Unit, int> DellStudent { get; }

        private int SaveTable()
        {
            using (StreamWriter file = new StreamWriter("studData.txt"))
            {
                for(int i = 0; i < Data.Count; i++)
                {
                    file.WriteLine(Data[i].Name);
                    for (int j = 0; j < Data[i].Cells.Count; j++)
                    {
                        file.Write(Data[i].Cells[j].Mark);
                        if (j != Data[i].Cells.Count - 1)
                            file.Write(" ");
                    }
                    file.WriteLine();
                }
                
            }
            return 0;
        }
        private int LoadTable()
        {
            string? line = "";
            Data.Clear();
            using (StreamReader file = new StreamReader("studData.txt"))
            {
                while((line = file.ReadLine())!=null)
                {
                    string[] parts = file.ReadLine().Split(" ");
                    List<string> marks = new List<string>();
                    foreach (string part in parts)
                        marks.Add(part);
                    Data.Add(new StudentData(line, marks));
                }
            }
            FindAverageLessons();
            return 0;
        }
        private int AddStudentToData()
        {
            Data.Add(new StudentData("", new List<string> { "#ERROR", "#ERROR", "#ERROR", "#ERROR" }));
            FindAverageLessons();
            return 0;
        }

        private int DellStudentFromData()
        {
            for(int i = 0; i < Data.Count; i++)
            {
                if(Data[i].IsChecked)
                {
                    Data.RemoveAt(i);
                    i--;
                }
            }
            FindAverageLessons();
            return 0;
        }

        public void FindAverageLessons()
        {
            double count = 0;
            bool isErorr = false;
            for (int i = 0; i < 5; i++)
            {
                count = 0;
                for(int j = 0; j < Data.Count; j++)
                {
                    double a;
                    string str;
                    if (i < 4)
                        str = Data[j].Cells[i].Mark;
                    else str = Data[j].AverageMark;
                    if (double.TryParse(str, out a))
                        count += a;
                    else
                    {
                        AverageLessons.Cells[i].Mark = "#ERROR";
                        isErorr = true;
                        break;
                    }
                    
                }
                if (!isErorr)
                {
                    AverageLessons.Cells[i].Mark = (count / Data.Count).ToString("N3");
                }
                    
                isErorr = false;
            }
                
        }
        
    }
    
}
