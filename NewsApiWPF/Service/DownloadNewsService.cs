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

        public async Task<ArticlesResult> DownloadArticlesAsync(string query, SortBys sortBy, Languages language, DateTime fromDate, ObservableCollection<Article> newsCollection, int pageNumber, int numberArticles)
        {
            var response = await _newsService.DisplayArticlesAsync(query, sortBy, language, fromDate, newsCollection, pageNumber, numberArticles);
            return response;
        }
    }
}
