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
            var apiKey = "df875f7a22714912891f255125bfa53e";
            _newsApiClient = new NewsApiClient(apiKey);
        }

        public async Task<ArticlesResult> DisplayArticlesAsync(string query, SortBys sortBy, Languages language, DateTime from, ObservableCollection<Article> newsCollection, int pageNumber, int numberArticles)
        {
            if (newsCollection is null)
            {
                newsCollection = new ObservableCollection<Article>();
            }
            try
            {
                var articlesResponse = await GetArticlesAsync(query, sortBy, language, from, pageNumber, numberArticles);

                if (articlesResponse.Status == Statuses.Ok)
                {
                    newsCollection.Clear();
                    foreach (var article in articlesResponse.Articles)
                    {
                        if (!string.IsNullOrWhiteSpace(article.Title) && !string.IsNullOrWhiteSpace(article.Author))
                        {
                            newsCollection.Add(article);
                        }
                    }
                }
                return articlesResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private async Task<ArticlesResult> GetArticlesAsync(string query, SortBys sortBy, Languages language, DateTime from, int page, int numberArticles)
        {
            var request = new EverythingRequest
            {
                Q = query,
                SortBy = sortBy,
                Language = language,
                From = from,
                PageSize = numberArticles,
                Page = page
            };
            return await _newsApiClient.GetEverythingAsync(request);
        }
    }
}
