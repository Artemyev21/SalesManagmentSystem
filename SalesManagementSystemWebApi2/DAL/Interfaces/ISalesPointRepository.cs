using System;

namespace SalesManagementSystemWebApi2.DAL.Interfaces
{
    public interface ISalesPointRepository
    {
        bool CheckExists(int id);

        bool Create(string name);

        void DeleteAll();
    }
}
