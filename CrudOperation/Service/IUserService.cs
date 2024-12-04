using CrudOperation.Model;

namespace CrudOperation.Service
{
    public interface IUserService
    {

        Task<List<UserContact>> GetResult();


        Task<bool> AddResult(UserContact userContact);

        Task<bool> DeleteResult(int id);

        Task<bool> UpdateResult(int id, UserContact userContact);

    }
}
