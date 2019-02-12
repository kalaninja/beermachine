namespace Sibintek.BeerMachine.DataContracts
{
    public class Result
    {
        public bool Success { get; set; }

        public decimal? Balance { get; set; }

        public string Error { get; set; }

        private Result()
        {
        }

        public static Result Ok(decimal balance) =>
            new Result
            {
                Success = true,
                Balance = balance
            };

        public static Result LowBalance(decimal balance) =>
            new Result
            {
                Success = false,
                Error = "Недостаточно баллов",
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