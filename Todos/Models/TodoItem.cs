using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.Models {
    class TodoItem : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private double isChecked;

        public double IsChecked {
            get { return isChecked; }
            set {
                isChecked = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked)));
                }
            }
        }

        private string title;

        public string Title {
            get { return title; }
            set {
                title = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }

        private string details;

        public string Details {
            get { return details; }
            set {
                details = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(Details)));
                }
            }
        }

        private DateTime dueDate;

        public DateTime DueDate {
            get { return dueDate; }
            set {
                dueDate = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(DueDate)));
                }
            }
        }

        public TodoItem(string title, string details, DateTime dueDate) {
            IsChecked = 0;
            Title = title;
            Details = details;
            DueDate = dueDate;
        }
    }
}
