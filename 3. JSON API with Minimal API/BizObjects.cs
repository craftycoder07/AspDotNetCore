namespace WebApplication1;

public record Person (string FirstName, string LastName);

public class Fruit
{
    public int Id {
        get;
        set;
    }

    public string Name { get; set; }
}

public class FruitHandler
{
    private static List<Fruit> Fruits = new List<Fruit>();
    
    public static void AddFruit(Fruit fruit) => Fruits.Add(fruit);
    public static void RemoveFruit(int id) => Fruits.Remove(Fruits.FirstOrDefault(x => x.Id == id));
    public static Fruit GetFruit(int id) => Fruits.FirstOrDefault(f => f.Id == id);
    public static List<Fruit> GetFruits() => Fruits;
}