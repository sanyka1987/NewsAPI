using NewsAPI.Models;
using NewsApiWPF.Commands;
using NewsApiWPF.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NewsApiWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NewsService _newsService;
        private readonly SearchService _searchService;
        private readonly CommandService _commandService;
        private readonly DownloadNewsService _downloadNewsService;

        //для поля пошуку
        private string query;
        public string Query { get { return query; } set { query = value; OnPropertyChanged(); } }

        //обрана стаття
        private Article selectedArticle;
        public Article SelectedArticle { get { return selectedArticle; } set { selectedArticle = value; OnPropertyChanged(); } }

        //вся колекція
        public ObservableCollection<Article> NewsCollection { get; set; }





        public MainViewModel()
        {
            _newsService = new NewsService();
            _searchService = new SearchService(_newsService);
            _commandService = new CommandService();
            _downloadNewsService = new DownloadNewsService(_newsService);

            NewsCollection = new ObservableCollection<Article>();

            _searchService.InitializeSearch(NewsCollection);
            LoadNewsAsync();

        }

        private async Task LoadNewsAsync()
        {
            await _downloadNewsService.DownloadArticlesAsync(NewsCollection);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  var query = SearchTextBox.Text // і тут тоді байндінг замість цього?

            _searchService.OnSearchTextChanged(query);
        }

        public IAsyncCommand<Article> ReadMoreCommand => new AsyncCommand<Article>(async (article) =>
        {
            await _commandService.OpenUrlAsync(article?.Url);
        });

    }
}
