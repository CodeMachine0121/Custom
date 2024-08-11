using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomBlockChainLab.Models.Entities;

public class Block
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Data { get; set; }

    [Required]
    public string Hash { get; set; }

    [Required]
    public string PreviousHash { get; set; }

    [Required]
    public DateTime TimeStamp { get; set; }

    [Required]
    public int Nonce { get; set; }
}