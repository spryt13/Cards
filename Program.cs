var symbol = new List<string>(4);
symbol.Add("hearts");
symbol.Add("diamonds");
symbol.Add("clubs");
symbol.Add("spades");

var sign = new char[] { 'A', '2', '3', '4', '5', '6', '7', '8', '9', '1', 'J', 'Q', 'K'};

Deck bicycle = new Deck(symbol, sign);

Console.WriteLine("You have a deck of cards. What would you like to do with it?");
Console.WriteLine("Write \"shuffle\" in order to shuffle the deck and \"pick\" if you'd like to pick out a card.");
string answer = Console.ReadLine();
int number;
if(answer == "shuffle")
{
    Console.WriteLine("How many times would you like to shuffle?");
    number = Convert.ToInt32(Console.ReadLine());
    for (int i = 0; i < number; i++)
        bicycle.Shuffle();
}

if(answer == "pick")
{
    Console.WriteLine("How many times would you like to pick a card?");
    number = Convert.ToInt32(Console.ReadLine());
    for (int i = 0; i < number; i++)
    {
        Console.WriteLine("Which card would you like to pick?");
        int choice = Convert.ToInt32(Console.ReadLine());
        bicycle.Pick(choice);
    }
}

bicycle.Show(bicycle);

public class Card
{
    public string symbol;
    public char sign;
    public int back;

    public Card(string a, char b)
    {
        symbol = a;
        sign = b;
    }

    public Card(string a, char b, int c)
    {
        symbol = a;
        sign = b;
        back = c * 3;
    }

    public override string ToString()
    {
        return this.sign + " of " + this.symbol;
    }
}