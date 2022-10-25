# Macau card game

A C# application being a fully playable Macau game.

## The scope of the project

This program is written with a target of creating a computed version of popular card game using object-oriented programming language. The idea is to create a standard 52-card deck and provide full model of the game. It is essential for the project to make a game operate on a certain set of rules characteristic for the game, but there is also an important element of understanding and coping with different behaviours and tactics that players can come up with.

## Current set of rules

The general game of Macau starts by each playing being dealt 5 cards and the remaining part of the deck is placed in the open (draw pile) with top card flipped next to it (discard pile). Players can play cards accordingly to the top card of the discard pile, that is by matching played card with sign or symbol to the card on top of the discard pile. Game continues in turns until at least one player runs out of cards. Whoever does so first is the winner!

The game uses some cards' signs as action cards, making it complicated to some extend and allowing the players to keep control of the events throughout the deal. Due to popularity of the game and spreading its rules by word, it is common to play the game using different rules, which mostly vary in terms of the impact of action cards. Game rules used in this version of program are as follows:
* 2s and 3s force next player to draw said amount of cards, unless the player has other 2 or 3 fitting with same sign or symbol. Playing such cards sum the amount of cards to be drawn and follow them to the next player,
* 4s force next player to lose turn, where each 4 stands for losing one turn. Next player can play other 4 if possible to relay and extend the loss to the next player,
* 5s to 10s are non-action cards,
* Jacks demand all the players to play a non-action card chosen by the player who discarded Jack. No other cards than Jacks and demanded sign can be played until the player who made the demand fulfills it. It is neccessary to demand a card which the player has, otherwise Jack lose its power and operate as a non-action card. Playing other Jacks change current demand and refreshes its duration.
* Queens are wild cards which can be played no matter the sign or symbol, as long as no other action is being considered during player's turn (except for aces).
* Kings are distinguished into the non-action kings of diamonds and clubs and the action kings of hearts and spades. The action ones cause next and previous player to draw 5 cards respectively.
* Aces allow the player to decide what symbol needs to be played next. This effect persists until first legal card is played. This does not prevent queens from being played which are still considered legal cards.

Additional mentions about the rules:
* No runs are legal in the game. The player can play several cards of the same sign in one turn, but not cards with consecutive signs (straights).
* Queens does not cancel any action cards.
* Action Kings cannot be forwarded by 2s and 3s and vice-versa.

Please keep in mind that since the application is still in development, some of the mentioned functionality may not be implemented yet.
