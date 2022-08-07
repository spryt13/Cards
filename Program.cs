var symbol = new List<string>(4);
symbol.Add("hearts");
symbol.Add("diamonds");
symbol.Add("clubs");
symbol.Add("spades");

var sign = new char[] { 'A', '2', '3', '4', '5', '6', '7', '8', '9', '1', 'J', 'Q', 'K'};

string answer = "0";
int number;

Deck bicycle = new Deck(symbol, sign);

while (answer != "done")
{
    Console.WriteLine("You have a deck of cards. What would you like to do with it?");
    Console.WriteLine("Write:");
    Console.WriteLine("\"shuffle\" to shuffle the deck,");
    Console.WriteLine("\"pick\" to pick out a card,");
    Console.WriteLine("\"show\" to see the deck,");
    Console.WriteLine("\"done\" to finish playing.");
    switch (answer = Console.ReadLine())
    {
        case "shuffle":
            Console.WriteLine("How many times would you like to shuffle?");
            number = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < number; i++)
                bicycle.Shuffle();
            break;

        case "pick":
            Console.WriteLine("How many times would you like to pick a card?");
            number = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Which card would you like to pick?");
                int choice = Convert.ToInt32(Console.ReadLine());
                bicycle.Pick(choice);
            }
            break;

        case "show":
            bicycle.Show(bicycle);
            break;


        default:
            Console.WriteLine("Incorrect command. Please try again");
            break;
    }
}

Console.WriteLine("This is your deck.");
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