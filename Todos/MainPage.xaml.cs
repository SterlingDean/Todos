using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Todos.ViewModels;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            tdvm = App.tdvm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            // 去掉返回箭头
            if (SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility == AppViewBackButtonVisibility.Visible) {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(EditPage));
        }
        private TodoItemViewModel tdvm;

        private async void SearchButton_Click(object sender, RoutedEventArgs e) {
            string result = tdvm.Search(SearchTextBox.Text.ToString());
            await new Windows.UI.Popups.MessageDialog(result) { Title = "Result" }.ShowAsync();
        }
    }
}
