using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApiWPF.Service
{
    public class DownloadNewsService
    {
        private readonly NewsService _newsService;

        public DownloadNewsService(NewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task DownloadArticlesAsync(ObservableCollection<Article> newsCollection)
        {
            var fromDate = DateTime.Today.AddMonths(-1);
            await _newsService.DisplayArticlesAsync("Apple", SortBys.Popularity, Languages.EN, fromDate, newsCollection);
        }
    }
}
