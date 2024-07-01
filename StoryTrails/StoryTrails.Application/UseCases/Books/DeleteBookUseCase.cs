using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Books
{
    public class DeleteBookUseCase: IDeleteBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseSettings _repository;
        public DeleteBookUseCase(DatabaseSettings repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Execute(string id)
        {
          var bookToDelete = await _repository.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (bookToDelete is null)
            {
                throw new Exception("not found");           
            }
            _repository.Books.Remove(bookToDelete);
            _repository.SaveChanges();
        }
    }
    
}
