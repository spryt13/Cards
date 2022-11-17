public class Macau
{
    private int turn = -1;
    private int state = 0;
    private int penalty = 0;
    private char demand = '0';
    private int demandingPlayer = -1;
    private int pause = 0;
    private int waitingPlayer = -1;
    private string change = "1";
    public Macau() { }

    public void GameMacau(Deck deck, Players players)
    {
        var pile = new List<Card>();

        for (int i = 0; i < 52; i++)
            deck.Shuffle();
        for (int j = 0; j < 5; j++)
        {
            foreach (List<Card> hand in players.hands)
                hand.Add(deck.Pick(1));
        } // dealing cards
        pile.Add(deck.Pick(1));
        do
        {
            turn++;
            if (turn == players.hands.Count())
                turn = 0;
            if (turn == waitingPlayer && pause != 0)
            {
                pause--;
                if (pause == 0)
                    waitingPlayer = -1;
            }
            else
            {
                Console.WriteLine("Next is Player number " + (turn + 1) + " (" + players.names[turn] + ")");
                Pause();
                TurnMacau(players.hands[turn]);
            }
        } while (players.hands[turn].Count() != 0); // game
        Console.WriteLine("Congratulations! Player no. " + (turn + 1) + " has won the game!");



        void TurnMacau(List<Card> hand)
        {
            Card topCard = pile[pile.Count() - 1];
            TurnInfo(players.hands, topCard);
            switch (state)
            {
                case 0: //neutral state
                    if (HandCheckMacau(hand, topCard) == false)
                    {
                        Console.WriteLine("You cannot play any card.");
                        hand.Add(deck.Pick(1));
                        Pause();
                        return;
                    }
                    break;

                case 1: //draw attack
                    Console.WriteLine("Ongoing attack! You are about to draw " + penalty + " cards.");
                    if (HandAttackCheckMacau(hand, topCard) == false)
                    {
                        Console.WriteLine("You cannot play any card.");
                        while (penalty > 0)
                        {
                            hand.Add(deck.Pick(1));
                            penalty--;
                        }
                        state = 0;
                        Pause();
                        return;
                    }
                    break;

                case 2: //pause attack
                    Console.WriteLine("Ongoing attack! You are about to pause for " + pause + " turn(s).");
                    if (HandSignCheckMacau(hand, topCard) == false)
                    {
                        Console.WriteLine("You cannot play any card.");
                        if (pause > 1)
                        {
                            waitingPlayer = turn;
                            pause--;
                        }
                        state = 0;
                        Pause();
                        return;
                    }
                    break;

                case 3: //jack's demand
                    Console.WriteLine("Ongoing demand! Player no. " + (demandingPlayer + 1) + " demands " + demand + "'s.");
                    if (HandDemandCheckMacau(hand) == false)
                    {
                        Console.WriteLine("You cannot play any card.");
                        hand.Add(deck.Pick(1));
                        if (demandingPlayer == turn)
                        {
                            demandingPlayer = -1;
                            state = 0;
                        }
                        Pause();
                        return;
                    }
                    break;

                case 4: //queen's cancel
                    break;

                case 5: //king's attack
                    if (HandSignCheckMacau(hand, topCard) == false)
                    {
                        Console.WriteLine("You cannot play any card.");
                        while (penalty > 0)
                        {
                            hand.Add(deck.Pick(1));
                            penalty--;
                        }
                        state = 0;
                        Pause();
                        return;
                    }
                    break;

                case 6: //ace's change
                    Console.WriteLine("Ongoing change! Symbol has been changed to " + change + "!");
                    if (HandAceCheckMacau(hand, topCard.sign, change) == false)
                    {
                        Console.WriteLine("You cannot play any card.");
                        hand.Add(deck.Pick(1));
                        Pause();
                        return;
                    }
                    break;
            } // verification: Can any card be played during certain state?
            Console.WriteLine("Choose a card (by typing a number starting from 1), which you'd like to play. Type 0 in order to draw a card anyway.");
            int choice;
            bool validity;
            do
            {
                choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 0)
                    validity = true;
                else
                {
                    if (choice > 0 && choice <= hand.Count())
                        if (CardCheckMacau(hand[choice - 1], topCard) == true)
                            validity = true;
                        else
                        {
                            validity = false;
                            Console.WriteLine("You cannot play this card. Choose a different one.");
                        }
                    else
                    {
                        validity = false;
                        Console.WriteLine("You cannot play this card. Choose a different one.");
                    }
                }
            } while (validity == false);
            if (choice == 0) // zero draws a card either way
            {
                hand.Add(deck.Pick(1));
                return;
            }
            else
            {
                Card playedCard = Deck.Pick(hand, choice);
                pile.Add(playedCard);
                topCard = pile[pile.Count() - 1];
                CardIdMacau(playedCard);
                while (HandSignCheckMacau(hand, topCard) == true)
                {
                    Deck.Show(hand);
                    Console.WriteLine("You can play more cards. Type 0 to end your turn.");
                    do
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        if (choice == 0)
                            return;
                        else
                        {
                            if (choice >= 0 && choice <= players.hands[turn].Count())
                                if (hand[choice - 1].sign == topCard.sign)
                                    validity = true;
                                else
                                {
                                    validity = false;
                                    Console.WriteLine("You cannot play this card. Choose a different one.");
                                }
                            else
                            {
                                validity = false;
                                Console.WriteLine("You cannot play this card. Choose a different one.");
                            }
                        }
                    } while (validity == false);
                    if (choice == 0)
                        return;
                    playedCard = Deck.Pick(hand, choice);
                    pile.Add(playedCard);
                    topCard = pile[pile.Count() - 1];
                    CardIdMacau(playedCard);
                }
            }
            Console.Clear();
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
                    pause++;
                    break;

                case 'J':
                    state = 3;
                    Console.WriteLine("What is your demand? (sign, from 5 to 10 (please type 1 instead of 10)");
                    do
                    {
                        demand = Convert.ToChar(Console.ReadLine());
                    } while (demand != '5' && demand != '6' && demand != '7' && demand != '8' && demand != '9' && demand != '1');
                    demandingPlayer = turn;
                    break;

                case 'Q':
                    state = 4;
                    break;

                case 'K':
                    if (card.symbol == "hearts" || card.symbol == "spades")
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
                    Console.WriteLine("What's the change? (symbol, chosen from hearts, diamonds, clubs and spades)");
                    do
                    {
                        change = Convert.ToString(Console.ReadLine());
                    } while (change != "hearts" && change != "diamonds" && change != "clubs" && change != "spades");
                    break;

                default:
                    if (demandingPlayer == -1)
                        state = 0;
                    break;
            }
        } // verification: What does the chosen card do?

        bool CardCheckMacau(Card card, Card topCard)
        {
            bool check = false;
            switch (state)
            {
                case 0:
                    if (topCard.sign == card.sign || topCard.symbol == card.symbol || card.sign == 'Q')
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
                    if (demand == card.sign || card.sign == 'J')
                        check = true;
                    break;

                case 4:
                    check = true;
                    break;

                case 5:
                    if (topCard.sign == card.sign)
                        check = true;
                    break;

                case 6:
                    if (topCard.sign == card.sign || change == card.symbol)
                        check = true;
                    break;
            }
            return check;
        } // verification: Can chosen card be played?

        bool HandCheckMacau(List<Card> hand, Card topCard)
        {
            bool check = false;
            for (int i = 0; i < hand.Count(); i++)
            {
                if (topCard.sign == hand[i].sign || topCard.symbol == hand[i].symbol || hand[i].sign == 'Q')
                {
                    check = true;
                    break;
                }
            }
            return check;
        } // card's playability during state 0 (neutral)

        bool HandAceCheckMacau(List<Card> hand, char topCardSign, string choice)
        {
            bool check = false;
            for (int i = 0; i < hand.Count(); i++)
            {
                if (topCardSign == hand[i].sign || choice == hand[i].symbol)
                {
                    check = true;
                    break;
                }
            }
            return check;
        } // card's playability during state 6 (aces)

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
        } // card's playability during state 2 (4s) and 5 (kings)

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
        } // card's playability during state 1 (2s and 3s)

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
        } // card's playability during state 3 (jacks)

        // queens don't require a playability check, anything can be played!

        void TurnInfo(List<List<Card>> players, Card topCard)
        {
            int i = 1;
            foreach (var hand in players)
            {
                Console.WriteLine("Player no." + i + " has " + hand.Count() + " cards.");
                i++;
            }
            Console.WriteLine("This is the visible card.");
            Console.WriteLine(topCard);
            Console.WriteLine("This is your hand.");
            Deck.Show(players[turn]);
        } // basic player's info

        void Pause()
        {
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            Console.Clear();
        } // pause and clear screen of the program
    }
}