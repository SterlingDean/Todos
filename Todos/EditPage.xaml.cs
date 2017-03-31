using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Todos {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EditPage : Page {
        public EditPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            // 设置返回箭头
            if (this.Frame.CanGoBack) {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            } else {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

            // 点击Todo Item进入时

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e) {
            // 检查用户输入是否合法
            StringBuilder temp = new StringBuilder();
            if (TitleTextBox.Text == "") {
                temp.Append("The title cannot be empty.\n");
            }
            if (DetailsTextBox.Text == "") {
                temp.Append("The details cannot be empty.\n");
            }
            if (DueDateDatePicker.Date.CompareTo(DateTime.Today) < 0) {
                temp.Append("The due date cannot be earlier than today.\n");
            }
            string warningMessage = temp.ToString();
            if (warningMessage.Equals("")) {
                // 合法，创建Todo Item
                
            } else {
                // 不合法，弹出警告消息对话框

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            TitleTextBox.Text = "";
            DetailsTextBox.Text = "";
            DueDateDatePicker.Date = DateTime.Today;
        }
    }
}
