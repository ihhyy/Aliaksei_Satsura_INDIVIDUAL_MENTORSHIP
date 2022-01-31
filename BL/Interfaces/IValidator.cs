using DAL.Entities;

namespace BL.Interfaces
{
    public interface IValidator
    {
        void ValidateInput(string input);
        void ValidateOutput(Weather output);
    }
}
