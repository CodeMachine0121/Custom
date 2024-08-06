namespace CustomBlockChainLab.Models.Domains;

public class Block
{
    public int Id { get; set; }
    public string Data { get; set; }
    public string Hash { get; set; }
    public string PreviousHash { get; set; }
    public DateTime TimeStamp { get; set; }
    public int Nonce { get; set; }
}