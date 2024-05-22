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
        private readonly CommandService _commandService;
        private readonly DownloadNewsService _downloadNewsService;

        public ObservableCollection<Article> NewsCollection { get; set; }
        public ICommand ReadMoreCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }

        private string _query = "Samsung";
        public string Query
        {
            get { return _query; }
            set
            {
                if (_query != value)
                {
                    _query = value;
                    OnPropertyChanged();
                    LoadNewsAsync();
                }
            }
        }

        private SortBys _sortBys = SortBys.PublishedAt;
        public SortBys SortBy
        {
            get { return _sortBys; }
            set
            {
                if (_sortBys != value)
                {
                    _sortBys = value;
                    OnPropertyChanged();
                    LoadNewsAsync();
                }
            }
        }

        private List<SortBys> _availableSortBy;
        public List<SortBys> AvailableSortBy
        {
            get { return _availableSortBy; }
            set
            {
                _availableSortBy = value;
                OnPropertyChanged();
            }
        }

        private Languages _language = Languages.EN;
        public Languages Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged();
                    LoadNewsAsync();
                }
            }
        }

        private List<Languages> _availableLanguages;
        public List<Languages> AvailableLanguages
        {
            get { return _availableLanguages; }
            set
            {
                _availableLanguages = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                    LoadNewsAsync();
                }
            }
        }

        private int _numberArticles = 6;
        public int NumberArticles
        {
            get { return _numberArticles; }
            set
            {
                if (_numberArticles != value)
                {
                    _numberArticles = value;
                    OnPropertyChanged();
                    LoadNewsAsync();
                }
            }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set
            {
                if (_totalPages != value)
                {
                    _totalPages = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            _newsService = new NewsService();
            _commandService = new CommandService();
            _downloadNewsService = new DownloadNewsService(_newsService);

            AvailableLanguages = Enum.GetValues(typeof(Languages)).Cast<Languages>().ToList();
            AvailableSortBy = Enum.GetValues(typeof(SortBys)).Cast<SortBys>().ToList();

            NewsCollection = new ObservableCollection<Article>();
            LoadNewsAsync();

            ReadMoreCommand = new AsyncCommandParameter<Article>(async (article) =>
            {
                await _commandService.OpenUrlAsync(article?.Url);
            });

            NextPageCommand = new AsyncCommand(async () =>
            {
                if (CurrentPage < TotalPages)
                {
                    CurrentPage++;
                }
            });

            PreviousPageCommand = new AsyncCommand(async () =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                }
            });
        }

        private async Task LoadNewsAsync()
        {
            var fromDate = DateTime.Today.AddMonths(-1);
            if (Query.Length > 2)
            {
                var result = await _downloadNewsService.DownloadArticlesAsync(Query, SortBy, Language, fromDate, NewsCollection, CurrentPage, NumberArticles);
                TotalPages = (int)Math.Ceiling(result.TotalResults / (double)NumberArticles);
            }
        }
    }
}
