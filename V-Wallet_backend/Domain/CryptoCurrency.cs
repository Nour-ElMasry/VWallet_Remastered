using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class CryptoCurrency
{
    [Key]
    public long CryptoId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public decimal Investment { get; set; }
    public DateTime DateOfInvestment { get; set; }
    public CryptoCurrency(string name, decimal value, decimal investment)
    {
        Name = name;
        Value = value;
        Investment = investment;
        DateOfInvestment = DateTime.Now;
    }
    public CryptoCurrency()
    {
    }
}
