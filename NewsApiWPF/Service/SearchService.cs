using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace NewsApiWPF.Service
{
    public class SearchService
    {
        private readonly NewsService _newsService;
        private readonly DispatcherTimer _searchDelayTimer;
        private ObservableCollection<Article> _newsCollection;

        public SearchService(NewsService newsService)
        {
            _newsService = newsService;
            _searchDelayTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _searchDelayTimer.Tick += async (sender, e) => await SearchDelayTimer_TickAsync();
        }

        public void InitializeSearch(ObservableCollection<Article> newsCollection)
        {
            _newsCollection = newsCollection;
        }

        public void OnSearchTextChanged(string query)
        {
            _searchDelayTimer.Stop();
            _searchDelayTimer.Tag = query;
            _searchDelayTimer.Start();
        }

        private async Task SearchDelayTimer_TickAsync()
        {
            _searchDelayTimer.Stop();
            var query = _searchDelayTimer.Tag as string;
            if (!string.IsNullOrEmpty(query))
            {
                await SearchArticlesAsync(query);
            }
        }

        private async Task SearchArticlesAsync(string query)
        {
            var fromDate = DateTime.Today.AddMonths(-1);
            await _newsService.DisplayArticlesAsync(query, NewsAPI.Constants.SortBys.Popularity, NewsAPI.Constants.Languages.EN, fromDate, _newsCollection);
        }
    }
}
