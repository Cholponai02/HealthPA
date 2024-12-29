namespace HealthPA.Views.NutritionViews;
using HealthPA.Models;
using HealthPA.Services;

public partial class ArticlesPage : ContentPage
{
    public List<Article> Articles { get; set; }

    public ArticlesPage()
    {
        InitializeComponent();
        var articleService = new ArticleService();
        Articles = articleService.GetArticles();
        BindingContext = this;
    }

    private async void OnArticleTapped(object sender, EventArgs e)
    {
        var frame = (Frame)sender;
        var selectedArticle = frame.BindingContext as Article;
        if (selectedArticle != null)
        {
            await Navigation.PushAsync(new ArticleDetailPage(selectedArticle));
        }
    }

}
