public class TransactionStatus
{
    public string Type { get; set; }

    public StatusModel Status { get; set; }

    public bool IsSuccess => Type == "committed" && Status?.Type == "success";

    public bool IsError => Status?.Type == "error";

    public class StatusModel
    {
        public string Type { get; set; }
    }
}