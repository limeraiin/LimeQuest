
public class Attribute
{
    public int value;

    public Attribute(int amount)
    {
        value = amount;
    }

    public void Add(int amount)
    {
        value += amount;
    }

    public void Set(int amount)
    {
        value = amount;
    }

    public void Multiply(float amount)
    {
        value = (int)(value*amount);
    }
}