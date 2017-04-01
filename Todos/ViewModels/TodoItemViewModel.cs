﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todos.Models;
using Windows.UI.Xaml.Media.Imaging;

namespace Todos.ViewModels {
    class TodoItemViewModel {
        public ObservableCollection<TodoItem> TodoItems { get; set; }
        public TodoItem ClickedItem { get; set; }

        public TodoItemViewModel() {
            TodoItems = new ObservableCollection<TodoItem>();
            ClickedItem = null;
        }

        // 对TodoItem进行增、改和删的方法
        public void Create(BitmapImage pictureSource, string title, string details, DateTime dueDate) {
            TodoItems.Add(new TodoItem(pictureSource, title, details, dueDate));
        }

        public void Update(TodoItem original, BitmapImage pictureSource, string title, string details, DateTime dueDate) {
            original.PictureSource = pictureSource;
            original.Title = title;
            original.Details = details;
            original.DueDate = dueDate;
        }

        public void Delete(TodoItem toBeDeleted) {
            TodoItems.Remove(toBeDeleted);
        }
    }
}
