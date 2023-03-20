namespace TopSoSanh.DTO
{
    public class ErrorModel
    {
        public List<string> Errors { get; } = new List<string>();
        public bool IsEmpty
        {
            get
            {
                return !Errors.Any();
            }
        }

        public void Add(string error)
        {
            Errors.Add(error);
        }
    }
}
