public class TransactionRequest
{
    public TransactionBody Body { get; }

    public int ProtocolVersion => 0;

    public int ServiceId => 26;

    public short MessageId { get; set; }

    public string Signature =>
        "2c234680adaa67f1e6573895f1557230ea5373b0972f8aa714611f78931c4bae49680580d41ac806977a7a4f9556781018f1061c9be4adcaabc3760c5a92a70b";

    public TransactionRequest(string id, string seed, short messageId, long? amount = null)
    {
        Body = new TransactionBody
        {
            Id = id,
            Seed = seed,
            Amount = amount?.ToString()
        };
        MessageId = messageId;
    }
}