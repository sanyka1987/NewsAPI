using NewsAPI.Constants;
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
using System.Windows.Input;

namespace NewsApiWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NewsService _newsService;
        private readonly SearchService _searchService;
        private readonly CommandService _commandService;
        private readonly DownloadNewsService _downloadNewsService;
        //вся колекція
        public ObservableCollection<Article> NewsCollection { get; set; }
        public ICommand ReadMoreCommand { get; set; }

        //для поля пошуку
        private string query;
        public string Query { get { return query; } set { query = value; OnPropertyChanged(); } }

        //для поля типу пошуку
        private string sortBy;
        public string SortBy { get { return sortBy; } set { sortBy = value; OnPropertyChanged(); } }
        public List<Object> SortByType;
        public List<Object> SelectLanguage;


        //для поля мови
        private string language;
        public string Language { get { return language; } set { language = value; OnPropertyChanged(); } }

        //обрана стаття
        private Article selectedArticle;
        public Article SelectedArticle { get { return selectedArticle; } set { selectedArticle = value; OnPropertyChanged(); } }

        public MainViewModel()
        {

           var SortByType = Enum.GetValues(typeof(SortBys)).Cast<SortBys>().ToList();//тип сортування
           var SelectLanguage = Enum.GetValues(typeof(Languages)).Cast<Languages>().ToList();//мова


            _newsService = new NewsService();
            _searchService = new SearchService(_newsService);
            _commandService = new CommandService();
            _downloadNewsService = new DownloadNewsService(_newsService);

            NewsCollection = new ObservableCollection<Article>();

            _searchService.InitializeSearch(NewsCollection);
            LoadNewsAsync();
            
            ReadMoreCommand = new AsyncCommand<Article>(async (article) =>
            {
                await _commandService.OpenUrlAsync(article?.Url);
            });
        }

        private async Task LoadNewsAsync()
        {
            var fromDate = DateTime.Today.AddMonths(-1);
            await _downloadNewsService.DownloadArticlesAsync(Query, SortBy, Language, fromDate, NewsCollection);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  var query = SearchTextBox.Text // і тут тоді байндінг замість цього?

            _searchService.OnSearchTextChanged(query);
        }
    }
}
