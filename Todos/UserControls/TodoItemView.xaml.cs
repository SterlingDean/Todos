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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Todos {
    public sealed partial class TodoItemView : UserControl {
        public TodoItemViewModel tdvm { get; set; }

        public TodoItemView() {
            this.InitializeComponent();

            tdvm = App.tdvm;
            //// 测试用例
            //tdvm.Create(new BitmapImage(new Uri("ms-appx:///Assets/background.jpg")), "完成作业", "UWP HW", DateTime.Today);
            //tdvm.Create(new BitmapImage(new Uri("ms-appx:///Assets/background.jpg")), "健身", "学校健身房", DateTime.Today);
        }

        private void TodoItemCheckBox_Click(object sender, RoutedEventArgs e) {
            tdvm.ClickedItem = (e.OriginalSource as CheckBox).DataContext as TodoItem;
            tdvm.ClickedItem.IsChecked = (tdvm.ClickedItem.IsChecked) == 0 ? 1 : 0;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e) {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(EditPage), e.ClickedItem);
        }

        private void DeleteMenuFlyoutItem_Click(object sender, RoutedEventArgs e) {
            dynamic item = e.OriginalSource;
            tdvm.Delete(item.DataContext);
        }
    }
}
