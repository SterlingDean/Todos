using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Todos.Models;
using Todos.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Todos {
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EditPage : Page {
        private TodoItemViewModel tdvm;

        public EditPage() {
            this.InitializeComponent();

            tdvm = App.tdvm;
        }

        private TodoItem clickedItem;

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
            if (e.Parameter != null) {
                clickedItem = e.Parameter as TodoItem;
                Picture.Source = clickedItem.PictureSource;
                TitleTextBox.Text = clickedItem.Title;
                DetailsTextBox.Text = clickedItem.Details;
                DueDateDatePicker.Date = clickedItem.DueDate;
                CreateButton.Content = "Update";
            }
        }

        private BitmapImage selectedPicture;
        private async void SelectPictureAppBarButton_Click(object sender, RoutedEventArgs e) {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");
            
            StorageFile pictureFile = await openPicker.PickSingleFileAsync();
            
            if (pictureFile != null) {
                var destFile = await pictureFile.CopyAsync(ApplicationData.Current.LocalFolder, pictureFile.Name, NameCollisionOption.ReplaceExisting);
                selectedPicture = new BitmapImage(new Uri(new Uri("ms-appdata:///local/"), destFile.Name));

                using (IRandomAccessStream fileStream = await pictureFile.OpenAsync(FileAccessMode.Read)) {
                    Picture.Source = selectedPicture;
                    if (clickedItem != null) clickedItem.PictureSource = selectedPicture;
                }
            }

        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e) {
            if ((sender as Button).Content.Equals("Create")) {  // 添加Todo Item
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
                    tdvm.Create((BitmapImage)Picture.Source, TitleTextBox.Text.ToString(), DetailsTextBox.Text.ToString(), DueDateDatePicker.Date.DateTime);
                    this.Frame.GoBack();
                } else {
                    // 不合法，弹出警告消息对话框
                    await new Windows.UI.Popups.MessageDialog(warningMessage) { Title = "Warning" }.ShowAsync();
                }
            } else {  // 更新Todo Item
                tdvm.Update(clickedItem, selectedPicture, TitleTextBox.Text.ToString(), DetailsTextBox.Text.ToString(), DueDateDatePicker.Date.DateTime);
                (sender as Button).Content = "Create";
                this.Frame.GoBack();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            TitleTextBox.Text = "";
            DetailsTextBox.Text = "";
            DueDateDatePicker.Date = DateTime.Today;
        }
    }
}
