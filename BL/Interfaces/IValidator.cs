using DAL.Entities;

namespace BL.Interfaces
{
    public interface IValidator
    {
        void ValidateInput(string input);
    }
}
