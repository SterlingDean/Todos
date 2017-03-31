using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Todos.Models;
using Todos.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Todos {
    public sealed partial class TodoItemView : UserControl {
        private TodoItemViewModel tdvm;

        public TodoItemView() {
            this.InitializeComponent();
            
            tdvm = new TodoItemViewModel();
            // 测试用例
            tdvm.Create("完成作业", "UWP HW", DateTime.Today);
            tdvm.Create("健身", "学校健身房", DateTime.Today);
        }

        private void TodoItemCheckBox_Click(object sender, RoutedEventArgs e) {
            dynamic item = e.OriginalSource;
            tdvm.ClickedItem = item.DataContext as TodoItem;
            tdvm.ClickedItem.IsChecked = (tdvm.ClickedItem.IsChecked) == 0 ? 1 : 0;
        }
    }
}
