using HealthPA.Models;
namespace HealthPA.Views.NutritionViews;

public partial class ArticleDetailPage : ContentPage
{
    public Article SelectedArticle { get; set; }

    public ArticleDetailPage(Article article)
    {
        InitializeComponent();
        if (article != null)
        {
            SelectedArticle = article;
            BindingContext = article;
        }
        else
        {
            DisplayAlert("Ошибка", "Не удалось загрузить статью", "OK");
        }
    }
}
