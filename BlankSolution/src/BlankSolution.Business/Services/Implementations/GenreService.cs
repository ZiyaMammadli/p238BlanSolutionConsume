using AutoMapper;
using BlankSolution.Business.CustomExceptions.CommonExceptions;
using BlankSolution.Business.DTOs.GenreDtos;
using BlankSolution.Business.Services.Interfaces;
using BlankSolution.Core.Entities;
using BlankSolution.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BlankSolution.Business.Services.Implementations;

public class GenreService : IGenreService
{
	private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository genreRepository,
		IMapper mapper)
    {
		_genreRepository = genreRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GenreGetListDto>> GetAllAsync()
	{
		var datas = await _genreRepository.GetAllAsync(x => !x.IsDeleted);
		var dtos = _mapper.Map<List<GenreGetListDto>>(datas);

		return dtos;

    }

	public async Task<IEnumerable<Genre>> GetAllPaginated(int page = 1, int pageSize = 2)
	{
		var table = _genreRepository.Table.AsQueryable();

		return await table.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
	}

	public async Task<Genre> CreateAsync(GenreCreateDto genreDto)
	{
		if(_genreRepository.Table.Any(x=>x.Name == genreDto.Name))
		{
			throw new Exception("Name already exist!");
		}

		Genre genre = _mapper.Map<Genre>(genreDto);
		genre.CreatedDate = DateTime.UtcNow;
		genre.UpdatedDate = DateTime.UtcNow;
		await _genreRepository.InsertAsync(genre);
		await _genreRepository.CommitAsync();

		return genre;
	}
	public async Task<Genre> UpdateAsync(GenrePutDto genrePutDto)
	{
		var currentGenre=await _genreRepository.GetByIdAsync(genrePutDto.Id);
		if (currentGenre is null) throw new NotFoundException(404, "Genre is not found");
		_mapper.Map(genrePutDto,currentGenre);
		currentGenre.UpdatedDate = DateTime.UtcNow;
		await _genreRepository.CommitAsync();
		return currentGenre;
	}
	public async Task DeleteAsync(int id)
	{
		var deletedGenre=await _genreRepository.GetByIdAsync(id);
		if(deletedGenre is null)	throw new NotFoundException(404,"Genre is not found");
		_genreRepository.Delete(deletedGenre);
		await _genreRepository.CommitAsync();
	}

}
