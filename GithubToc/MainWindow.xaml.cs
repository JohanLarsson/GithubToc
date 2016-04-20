namespace GithubToc
{
    using System;
    using System.IO;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void OnOpenClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Ookii.Dialogs.Wpf.VistaOpenFileDialog { Filter = "markdown files (*.md)|*.md" };
            if (fileDialog.ShowDialog(this) == true)
            {
                try
                {
                    var toc = TableOfContents.Create(File.ReadAllText(fileDialog.FileName));
                    this.TocTextBox.Text = toc;
                }
                catch (Exception ex)
                {
                    this.TocTextBox.Text = ex.Message;
                }
            }
        }
    }
}
