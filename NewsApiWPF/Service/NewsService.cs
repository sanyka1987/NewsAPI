using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewsApiWPF.Service
{
    public class NewsService
    {
        private readonly NewsApiClient _newsApiClient;

        public NewsService()
        {
            // API ключ
            var apiKey = "6e456040f8404d7db1d38e45e4261100";
            _newsApiClient = new NewsApiClient(apiKey);
        }

        public async Task DisplayArticlesAsync(string query, SortBys sortBy, Languages language, DateTime from, ObservableCollection<Article> newsCollection)
        {
            try
            {
                var articlesResponse = await GetArticlesAsync(query, sortBy, language, from);

                if (articlesResponse.Status == Statuses.Ok)
                {
                    newsCollection.Clear();
                    foreach (var article in articlesResponse.Articles)
                    {
                        newsCollection.Add(article);
                    }
                }
                else
                {
                    MessageBox.Show($"Failed to fetch news data.\nError: {articlesResponse.Error.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<ArticlesResult> GetArticlesAsync(string query, SortBys sortBy, Languages language, DateTime from)
        {
            var request = new EverythingRequest
            {
                Q = query,
                SortBy = sortBy,
                Language = language,
                From = from
            };

            return await _newsApiClient.GetEverythingAsync(request);
        }
    }
}
