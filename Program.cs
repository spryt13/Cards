var symbol = new List<string>(4); //suit
symbol.Add("hearts");
symbol.Add("diamonds");
symbol.Add("clubs");
symbol.Add("spades");

var sign = new char[] { 'A', '2', '3', '4', '5', '6', '7', '8', '9', '1', 'J', 'Q', 'K'}; //value ||| '1' stands for 10

string answer = String.Empty;
int number;

var hand = new List<Card>();

Deck bicycle = new Deck(symbol, sign);

while (answer != "done")
{
    Console.WriteLine("You have a deck of cards. What would you like to do with it?");
    Console.WriteLine("Write:");
    Console.WriteLine("\"macau\" to play macau,");
    Console.WriteLine("\"shuffle\" to shuffle the deck,");
    Console.WriteLine("\"pick\" to pick out a card,");
    Console.WriteLine("\"show\" to see the deck,");
    Console.WriteLine("\"done\" to finish playing.");

    string? isNull = Console.ReadLine();
    answer = isNull!;

    switch (answer)
    {
        case "macau":
            Macau(bicycle);
            break;

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
                hand.Add(bicycle.Pick(choice));
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
Console.WriteLine("This is your hand.");
Deck.Show(hand);

static void Macau(Deck deck)
{
    var hand1 = new List<Card>();
    var hand2 = new List<Card>();
    var pile = new List<Card>();
    bool turn = false;
    int state = 0;
    int penalty = 0;
    char demand = '0';
    string change;
    //bool attack = false;
    for (int i = 0; i < 52; i++)
        deck.Shuffle();
    //dealing cards
    for (int i = 0; i < 5; i++)
    {
        hand1.Add(deck.Pick(1));
        hand2.Add(deck.Pick(1));
    }
    pile.Add(deck.Pick(1));
    //game starts
    while(hand1.Count() != 0 && hand2.Count() != 0)
    {
        if (turn == false)
        {
            TurnMacau(hand1);
            turn = true;
        }
        else
        {
            TurnMacau(hand2);
            turn = false;
        }
    }

    void TurnMacau(List<Card> hand)
    {
        Card topCard = pile[pile.Count() - 1];
        Console.WriteLine("To jest widoczna karta.");
        Console.WriteLine(topCard);
        Console.WriteLine("To jest twoja ręka.");
        Deck.Show(hand);
        switch (state)
        {
            case 0: //neutral state
                if (HandCheckMacau(hand, topCard) == false)
                {
                    Console.Write("Nie możesz zagrać żadnej karty.");
                    hand.Add(deck.Pick(1));
                    return;
                }
                break;

            case 1: //draw attack
                if (HandAttackCheckMacau(hand, topCard) == false)
                {
                    Console.Write("Nie możesz zagrać żadnej karty.");
                    while (penalty > 0)
                    {
                        hand.Add(deck.Pick(1));
                        penalty--;
                    }
                    state = 0;
                    return;
                }
                break;

            case 2: //pause attack
                if (HandSignCheckMacau(hand, topCard) == false)
                {
                    Console.Write("Nie możesz zagrać żadnej karty.");
                    //wait x turns
                    state = 0;
                    return;
                }
                break;

            case 3: //jack's demand
                if (HandDemandCheckMacau(hand) == false)
                {
                    Console.Write("Nie możesz zagrać żadnej karty.");
                    hand.Add(deck.Pick(1));
                    return;
                }
                break;

            case 4: //queen's cancel
                break;

            case 5: //king's attack
                if (HandSignCheckMacau(hand, topCard) == false)
                {
                    Console.Write("Nie możesz zagrać żadnej karty.");
                    while (penalty > 0)
                    {
                        hand.Add(deck.Pick(1));
                        penalty--;
                    }
                    state = 0;
                    return;
                }
                break;

            case 6: //ace's change
                break;
        }
        Console.WriteLine("Wybierz kartę (wpisując cyfrę zaczynając od 1), którą chcesz zagrać.");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 0) // zero draws a card either way
        {
            hand.Add(deck.Pick(1));
            return;
        }
        if (CardCheckMacau(hand[choice - 1], topCard) == true)
        {
            Card playedCard = Deck.Pick(hand, choice);
            pile.Add(playedCard);
            CardIdMacau(playedCard);
            while (HandSignCheckMacau(hand, pile[pile.Count() - 1]) == true)
            {
                Deck.Show(hand);
                choice = Convert.ToInt32(Console.ReadLine());
                playedCard = Deck.Pick(hand, choice);
                pile.Add(playedCard);
                CardIdMacau(playedCard);
            }
        }
    }

    void CardIdMacau(Card card)
    {
        switch (card.sign)
        {
            case '2':
                state = 1;
                penalty = penalty + 2;
                break;

            case '3':
                state = 1;
                penalty = penalty + 3;
                break;

            case '4':
                state = 2;
                break;

            case 'J':
                state = 3;
                Console.WriteLine("Jakiego znaku żądasz?");
                demand = Convert.ToChar(Console.Read());
                break;

            case 'Q':
                state = 4;
                break;

            case 'K':
                if(card.symbol == "hearts" || card.symbol == "spades")
                {
                    state = 5;
                    penalty = penalty + 5;
                }
                else
                {
                    state = 0;
                    penalty = 0;
                }
                break;

            case 'A':
                state = 6;
                Console.WriteLine("Jakiego symbolu/koloru żądasz?");
                change = Convert.ToString(Console.Read());
                break;
        }
    }

    bool CardCheckMacau(Card card, Card topCard)
    {
        bool check = false;
        switch (state)
        {
            case 0:
                if (topCard.sign == card.sign || topCard.symbol == card.symbol)
                    check = true;
                break;

            case 1:
                if (topCard.sign == card.sign || (topCard.symbol == card.symbol && (card.sign == '2' || card.sign == '3')))
                    check = true;
                break;

            case 2:
                if (topCard.sign == card.sign)
                    check = true;
                break;

            case 3:
                if (topCard.sign == card.sign || card.sign == 'J')
                    check = true;
                break;

            case 4:
                check = true;
                break;

            case 5:
                if (topCard.sign == card.sign)
                    check = true;
                break;
        }
        return check;
    }

    bool HandCheckMacau(List<Card> hand, Card topCard)
    {
        bool check = false;
        for (int i = 0; i < hand.Count(); i++)
        {
            if (topCard.sign == hand[i].sign || topCard.symbol == hand[i].symbol)
            {
                check = true;
                break;
            }
        }
        return check;
    }

    bool HandSignCheckMacau(List<Card> hand, Card topCard)
    {
        bool check = false;
        for (int i = 0; i < hand.Count(); i++)
        {
            if (topCard.sign == hand[i].sign)
            {
                check = true;
                break;
            }
        }
        return check;
    }

    bool HandAttackCheckMacau(List<Card> hand, Card topCard)
    {
        bool check = false;
        for (int i = 0; i < hand.Count(); i++)
        {
            if (topCard.sign == hand[i].sign || (topCard.symbol == hand[i].symbol && (hand[i].sign == '2' || hand[i].sign == '3')))
            {
                check = true;
                break;
            }
        }
        return check;
    }

    bool HandDemandCheckMacau(List<Card> hand)
    {
        bool check = false;
        for (int i = 0; i < hand.Count(); i++)
        {
            if (hand[i].sign == demand || hand[i].sign == 'J')
            {
                check = true;
                break;
            }
        }
        return check;
    }
}



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