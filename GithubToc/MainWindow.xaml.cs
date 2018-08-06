namespace GithubToc
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel viewModel = new ViewModel();

        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = this.viewModel;
        }

        private void OnOpenClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Ookii.Dialogs.Wpf.VistaOpenFileDialog { Filter = "markdown files (*.md)|*.md" };
            if (fileDialog.ShowDialog(this) == true)
            {
                try
                {
                    this.viewModel.Source = new Uri(fileDialog.FileName, UriKind.Absolute);
                }
                catch (Exception ex)
                {
                    this.viewModel.TableOfContents = ex.Message;
                }
            }
        }

        private void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            this.viewModel.Refresh();
        }
    }
}
