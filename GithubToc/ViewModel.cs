namespace GithubToc
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    using JetBrains.Annotations;

    public sealed class ViewModel : INotifyPropertyChanged
    {
        private Uri source;

        private string tableOfContents;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TableOfContents
        {
            get
            {
                return this.tableOfContents;
            }
            set
            {
                if (value == this.tableOfContents) return;
                this.tableOfContents = value;
                this.OnPropertyChanged();
            }
        }

        public Uri Source
        {
            get
            {
                return this.source;
            }
            set
            {
                if (Equals(value, this.source)) return;
                this.source = value;
                this.OnPropertyChanged();
                this.Refresh();
            }
        }


        public void Refresh()
        {
            try
            {
                string markdown;
                switch (this.Source?.Scheme)
                {
                    case "http":
                    case "https":
                        ////using (var client = new WebClient())
                        ////{
                        ////    markdown = await client.DownloadStringTaskAsync(this.Source);
                        ////}
                        this.TableOfContents = "not implemented";
                        return;
                    case "file":
                        markdown = File.ReadAllText(this.Source.LocalPath);
                        break;
                    default:
                        this.TableOfContents = string.Empty;
                        return;
                }

                this.TableOfContents = string.IsNullOrEmpty(markdown)
                                           ? string.Empty
                                           : GithubToc.TableOfContents.Create(markdown);

            }
            catch (Exception ex)
            {
                this.TableOfContents = ex.Message;
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}