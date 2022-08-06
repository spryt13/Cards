public class Deck
{
    public List<Card> cards = new List<Card>();
    private static Random Random = new Random();

    public void Shuffle()
    {
        int b = Random.Next(cards.Count()-1);
        List<Card> packetOne = new List<Card>();
        List<Card> packetTwo = new List<Card>();
        for(int i = 0; i < cards.Count(); i++)
        {
            if (i < b)
                packetOne.Insert(i, cards[i]);
            else
                packetTwo.Insert(i-b, cards[i]);
        }
        for(int i = 0; i < packetTwo.Count(); i++)
        {
            if (i < packetOne.Count())
                cards[2 * i] = packetTwo[i];
        }
        for(int i = 0; i < packetOne.Count(); i++)
        {
            if (i < packetTwo.Count())
                cards[2 * i + 1] = packetOne[i];
        }
        if (packetOne.Count() > packetTwo.Count())
            for(int i = packetTwo.Count(); i < packetOne.Count(); i++)
            {
                cards[i + packetTwo.Count()] = packetOne[i];
            }
            
        //shuffle cards here
    }

    public Card Pick(int number)
    {
        Card a = cards[number-1];
        cards.Remove(a);
        return a;
    }

    public void Show(Deck deck)
    {
        for (int i = 0; i < deck.cards.Count(); i++)
        {
            Console.Write(deck.cards[i].ToString());
            Console.WriteLine();
        }
    }

    public Deck(IList<string> symbol, IList<char> sign)
    {
        for (int i = 0; i < symbol.Count(); i++)
        {
            for (int j = 0; j < sign.Count(); j++)
            {
            Card card = new Card(symbol[i], sign[j]);
            cards.Add(card);
            }
        }
    }
}