using BlankSolution.MVC.Areas.Admin.ViewModels.MovieImageViewModel;

namespace BlankSolution.MVC.Areas.Admin.ViewModels.MovieViewModel;

public class MovieGetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string GenreName { get; set; }
    public List<MovieImageGetViewModel> MovieImages { get; set; }
}
