namespace BlankSolution.MVC.Areas.Admin.ViewModels.MovieViewModel;

public class MoviePostViewModel
{
    public int GenreId { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public double CostPrice { get; set; } // 40
    public double SalePrice { get; set; } // 40>
    public bool IsDeleted { get; set; }
    public IFormFile ImageFile { get; set; }
}
