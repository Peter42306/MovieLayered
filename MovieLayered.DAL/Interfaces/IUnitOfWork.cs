using MovieLayered.DAL.Entities;
using System.Numerics;

namespace MovieLayered.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Movie> Movies { get; }        
        Task Save();
    }
}
