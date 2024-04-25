using BlankSolution.Business.DTOs.GenreDtos;
using BlankSolution.Core.Entities;

namespace BlankSolution.Business.Services.Interfaces;

public interface IGenreService
{
	Task<IEnumerable<GenreGetListDto>> GetAllAsync();
	Task<Genre> CreateAsync(GenreCreateDto genre);
	Task<Genre> UpdateAsync(GenrePutDto genrePutDto);
	Task DeleteAsync(int id);
	Task<IEnumerable<Genre>> GetAllPaginated(int page = 1, int pageSize=2);
}
