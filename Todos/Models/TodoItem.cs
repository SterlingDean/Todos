using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

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

        private ImageSource pictureSource;

        public ImageSource PictureSource {
            get { return pictureSource; }
            set {
                pictureSource = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler.Invoke(this, new PropertyChangedEventArgs(nameof(PictureSource)));
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

        public TodoItem(BitmapImage pictureSource, string title, string details, DateTime dueDate) {
            IsChecked = 0;
            PictureSource = pictureSource;
            Title = title;
            Details = details;
            DueDate = dueDate;
        }
    }
}
