var symbol = new List<string>(4);
symbol.Add("hearts");
symbol.Add("diamonds");
symbol.Add("clubs");
symbol.Add("spades");

var sign = new char[] { 'A', '2', '3', '4', '5', '6', '7', '8', '9', '1', 'J', 'Q', 'K'};

Deck bicycle = new Deck(symbol, sign);

{
    //for (int i = 0; i < 10; i++)
    {
        bicycle.Shuffle();
    }

    int number = Convert.ToInt32(Console.ReadLine());
    bicycle.Pick(number);

    for (int i = 0; i < bicycle.cards.Count(); i++)
    {
        Console.Write(bicycle.cards[i].ToString());
        Console.WriteLine();
    }
}

var karta = new Card(symbol[1], sign[1], 1);

Console.Write(karta.back);

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