namespace BL.Interfaces
{
    public interface IValidator
    {
        void ValidateInput(string input);

        void ValidateMultiInput(string input, int days);
    }
}
