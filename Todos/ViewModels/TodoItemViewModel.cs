using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todos.Models;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Todos.ViewModels {
    public class TodoItemViewModel {
        public ObservableCollection<TodoItem> TodoItems { get; set; }
        public TodoItem ClickedItem { get; set; }

        public TodoItemViewModel() {
            TodoItems = GetData();
            ClickedItem = null;
        }

        private ObservableCollection<TodoItem> GetData() {
            ObservableCollection<TodoItem> data = new ObservableCollection<TodoItem>();
            var db = App.conn;
            using (var stmt = db.Prepare("SELECT * FROM TodoItems")) {
                while (SQLiteResult.ROW == stmt.Step()) {
                    data.Add(new TodoItem(new BitmapImage(new Uri((string)stmt["PictureUri"])), (string)stmt["Title"], (string)stmt["Details"], DateTime.Parse((string)stmt["DueDate"])));
                }
            }
            return data;
        }

        // 对TodoItem进行增、改和删的方法
        public void Create(BitmapImage pictureSource, string title, string details, DateTime dueDate) {
            TodoItems.Add(new TodoItem(pictureSource, title, details, dueDate));

            var db = App.conn;
            using (var stmt = db.Prepare("INSERT INTO TodoItems(PictureUri, Title, Details, DueDate) VALUES (?, ?, ?, ?)")) {
                stmt.Bind(1, pictureSource.UriSource.ToString());
                stmt.Bind(2, title);
                stmt.Bind(3, details);
                stmt.Bind(4, dueDate.ToString());
                stmt.Step();
            }
        }

        public void Update(TodoItem original, BitmapImage pictureSource, string title, string details, DateTime dueDate) {
            var db = App.conn;
            using (var stmt = db.Prepare("UPDATE TodoItems SET PictureUri = ?, Title = ?, Details = ?, DueDate = ? WHERE Title = ? AND Details = ?")) {
                stmt.Bind(1, pictureSource.UriSource.ToString());
                stmt.Bind(2, title);
                stmt.Bind(3, details);
                stmt.Bind(4, dueDate.ToString());
                stmt.Bind(5, original.Title);
                stmt.Bind(6, original.Details);
                stmt.Step();
            }

            original.PictureSource = pictureSource;
            original.Title = title;
            original.Details = details;
            original.DueDate = dueDate;
        }

        public void Delete(TodoItem toBeDeleted) {
            TodoItems.Remove(toBeDeleted);

            var db = App.conn;
            using (var stmt = db.Prepare("DELETE FROM TodoItems WHERE Title = ? AND Details = ?")) {
                stmt.Bind(1, toBeDeleted.Title);
                stmt.Bind(2, toBeDeleted.Details);
                stmt.Step();
            }
        }
    }
}
