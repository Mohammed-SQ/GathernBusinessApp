namespace FMMSRestaurant.Models;

public class FoodItem
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public string? Category { get; set; }
    public int Quantity { get; set; } = 1; // Used in OrderItems
}