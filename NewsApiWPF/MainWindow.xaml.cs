using NewsAPI.Constants;
using NewsAPI.Models;
using NewsApiWPF.Commands;
using NewsApiWPF.Service;
using NewsApiWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewsApiWPF
{
    public partial class MainWindow : Window
    {
<<<<<<< Updated upstream
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
=======
        /*private readonly NewsService _newsService;//це переносимо
        private readonly SearchService _searchService;
        private readonly CommandService _commandService;
        private readonly DownloadNewsService _downloadNewsService;

        public ObservableCollection<Article> NewsCollection { get; set; }//аж до сюда переносимо*/

        public MainWindow()
        {
            InitializeComponent();//це тут лишається
          /* _newsService = new NewsService();//а це в конструктор вьюмоделі
            _searchService = new SearchService(_newsService);
            _commandService = new CommandService();
            _downloadNewsService = new DownloadNewsService(_newsService);//до сюда перенесли*/

            DataContext = this;
           /* NewsCollection = new ObservableCollection<Article>(); // Ініціалізація колекції//теж перенесли

            _searchService.InitializeSearch(NewsCollection);//теж перенесли
            LoadNewsAsync(); // Виклик методу завантаження даних*/
        }

       /* private async Task LoadNewsAsync()
        {
            await _downloadNewsService.DownloadArticlesAsync(NewsCollection);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = SearchTextBox.Text;
            _searchService.OnSearchTextChanged(query);
        }

        public IAsyncCommand<Article> ReadMoreCommand => new AsyncCommand<Article>(async (article) =>
        {
            await _commandService.OpenUrlAsync(article?.Url);
        });*/
>>>>>>> Stashed changes
    }
}
