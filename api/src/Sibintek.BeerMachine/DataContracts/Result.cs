namespace Sibintek.BeerMachine.DataContracts
{
    public class Result
    {
        public bool Success { get; set; }

        public long? Balance { get; set; }

        public string Error { get; set; }

        private Result()
        {
        }

        public static Result Ok(long balance) =>
            new Result
            {
                Success = true,
                Balance = balance
            };

        public static Result NotFound() =>
            new Result
            {
                Success = false,
                Error = "Аккаунт не найден"
            };
    }
}