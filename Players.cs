public class Players
{
    public List<List<Card>> hands = new List<List<Card>>();
    public List<string> names = new List<string>();

    public Players()
    {
        Console.WriteLine("How many players?");
        int x = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < x; i++)
        {
            var hand = new List<Card>();
            hands.Add(hand);
            Console.WriteLine("What's your name, Player " + (i+1) + "?");
            names.Add(Console.ReadLine());
        }
    }
}