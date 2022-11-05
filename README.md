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
* Whenever a player cannot play any card from hand, then one card needs to be drawn unless action cards state otherwise,
* No runs are legal in the game. The player can play several cards of the same sign in one turn, but not cards with consecutive signs (straights),
* Queens does not cancel any action cards,
* Action Kings cannot be forwarded by 2s and 3s and vice-versa.

## Implementation and execution of the game

Created application does not include the game only. As the initial idea of making an application in C# was to create a deck of cards and allow some ordinary operation using it, a main menu interface is created from where User can proceed to performing any action desired including playing Macau game. This option of displaying possibilities allows extending application's variety of playable games or other actions that can be performed with a deck of cards. In current state, beside playing Macau, User is able to try basic operations including shuffling, picking a card out of the deck or displaying the order of cards in the deck. The implementation of the game of Macau itself consists of four parts:
* preparation of the game,
* check of Player's hand,
* choice and verification of played cards,
* application of effects.

### Preparation of the game

This simple stage of the game uses mostly simple method calls and loops in order to supply Players with necessary setup for playing the game. After shuffling the deck and gaining the amount of players taking part in the game, the application generates a list of cards for each Player (hand) and deals appropriate amount of cards so that every Player has 5 cards at the beginning of the game. Lastly, one card from top is picked as a start of the pile and the game is ready to go. From that point on Players will play in turn until one of them runs out of cards on hand.

### Check of Player's hand

Before Player has any ability of performing actions during his/her turn, a check of Player's hand is performed. This operation has the target of immediately verifying if any action can be performed and ending the turn if no actions are possible. In order to do so, the application needs to understand different situations during the game and apply different rules for this check. This is done by using *switch* function and *state* variable which redirect application to adequate method, where each card is examinated separately. Whenever there's any card that can be played under current circumstances, the turn may continue and let the Player choose which card should be played.

### Choice and verification of played cards

Only once the application knows that there are cards that can be legally played comes Player's turn to choose which card from hand should be played. There is an option to decline playing of a card, which results in forced draw of one card, stated by the rules. The Player types a number in order to select a card from the list/hand. Here, the application needs to handle three problems:
* is the entered number different than zero?
* is the entered number pointing at a card in hand?
* is the selected card legal in current situation?
Typing zero instead of a positive number is the designed way of not playing a card and ending turn as if The Player couldn't play anything. This can be handled differently and simply with an *if* function in order to quickly end turn and proceed to the next player. Second issue obviously appears whenever something other than zero is entered. Reason for this is that the input must point the application to a certain card in hand. When this is accomplished, the card itself is verified once again in terms of playability. As much as these two issues can also be handled with *if* functions, the key to assuring the stability of the application is to seperate the operations. This ensures that given input can define which card is chosen and a verification of a card can be conducted.

Playing more than a single card during turn is executed in the same manner. The only difference can be visible in verification, where different rules need to be used in order to confirm validity of the chosen card.

### Application of effects

Since the Player has decided on which card to play and the application has confirmed that the card is allowed to be put onto the top of the discard pile, the game may change it's *state* based on what card exactly is being played. In order to do so, a method *CardIDMacau* is designed. This method is capable of "reading" the card using *switch* function and *state* variable, change the state of the game whenever an action card is played and modify appropriate values responsible for all kinds of penalties and demands. Once the cards are played and their effects acknowledged, Player ends his/her turn and the game can continue.
