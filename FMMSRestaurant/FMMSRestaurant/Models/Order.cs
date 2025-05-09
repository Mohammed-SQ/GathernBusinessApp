namespace FMMSRestaurant.Models;

public class Order
{
    public Table Table { get; set; } = new Table(); // Default to new Table instance
    public string Items { get; set; } = string.Empty; // Default to empty string
}