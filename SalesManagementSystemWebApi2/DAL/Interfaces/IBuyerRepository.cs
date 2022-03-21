namespace SalesManagementSystemWebApi2.DAL.Interfaces
{
    public interface IBuyerRepository
    {
        public bool CheckExists(long id);

        public int Create(string name);
        
        public void DeleteAll();
    }
}
