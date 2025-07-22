using System;

namespace StocksApi.Models;

public class Portfolio
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }
}
