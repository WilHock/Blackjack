using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
	class Card
	{
		private readonly static string[] allSuits = { "Heart", "Diamond", "Spade", "Club" };
		private readonly static string[] allSymbols = { "♥", "♦", "♠", "♣" };

		private readonly static string[] allRanks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
		private readonly static int[] allValues = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10};

		public string Rank;
		public string Suit;
		public string Symbol;


		//Used to provide whatever asks with all the suits 
		public static string[] getSuits()
		{ 
			return allSuits;
		}
		//Used to provide whatever asks with all the card values
		public static string[] getRanks()
		{
			return allRanks;
		}



		//Gets the value of the card
		public int getValue()
		{
			//The value of the card
			int cardValue = 0;
			int rankPosition = 0;

			//i is the position of the cards rank in the allRanks array
			rankPosition = Array.IndexOf(allRanks, this.Rank);

			//If i is greater than 0
			if(rankPosition > -1)
			{
				//The card value is equal to the same position as the rank in the allValues array
				//As the cards rank and value are in order, this returns the corresponding value
				cardValue = allValues[rankPosition];
			}

			//Return the calculated value
			return cardValue;
		}

		//Used to generate a card with the values given to it
		public Card(string suit, string rank)
		{
			//The suit the user passed in becomes the cards suit
			Suit = suit;
			//The value the user passed in becomes the cards suit
			Rank = rank;

			//Gets the symbol of the card based on the suit
			switch (Suit)
			{
				// 0 1 2 3
				// ♥ ♦ ♠ ♣

				case "Heart":
					Symbol = allSymbols[0];
					break;
				case "Diamond":
					Symbol = allSymbols[1];
					break;
				case "Spade":
					Symbol = allSymbols[2];
					break;
				case "Club":
					Symbol = allSymbols[3];
					break;
			}

		}

		//Writes the cards each on a new line
		public override string ToString()
		{
			string output = "";
			string line = "\r";

			output += " ____" + line;
			output += "| r |" + line;
			output += "| s  |" + line;
			output += "|____|" + line;
			
			//If the rank of the card is 1 character in length
			if(Rank.Length == 1)
			{
				//Replace the "r" in the output with the rank
				//Add a blank space after the number as otherwise the shape of the card will be wrong
				output = output.Replace("r", Rank+" ");
			}
			//If the rank of the card is 2 characters long
			else
			{
				//Replace the "r" in the output with the rank
				//Don't add a space at the end
				output = output.Replace("r", Rank);

			}
			//Replace the "s" with the suit symbol 
			output = output.Replace("s", Symbol);
			return output;
		}

		//Writes the cards next to each other on the same axis
		public void writeCard(int x, int y)
		{
			//Changes the colour of the card depending on its suit
			//Heart and Diamond are Red
			//Spade and Club are White
			switch (Suit)
			{
				case "Heart":
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case "Diamond":
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case "Spade":
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case "Club":
					Console.ForegroundColor = ConsoleColor.White;
					break;
			}

			//Sets the cursor position to the passed values
			Console.SetCursorPosition(x, y);
			//Draws the top of the card
			Console.WriteLine(" ____");

			//Moves the cursor down a line
			Console.SetCursorPosition(x, y + 1);
			//If the rank of the card is 1 character long
			if (Rank.Length == 1)
			{
				//Writes the next line with an extra space to align the sides correctly
				Console.WriteLine("| " + Rank + "  |");
			}
			else
			{
				//Else it writes it with no extra space, as the extra character in the rank will fill it
				Console.WriteLine("| " + Rank + " |");
			}

			//Moves the cursor down a line
			Console.SetCursorPosition(x, y + 2);
			//Writes the next line of the card, filling in the symbol
			Console.WriteLine("| "+Symbol+"  |");

			//Moves the cursor down a line
			Console.SetCursorPosition(x, y + 3);
			//Draws the bottom of the card
			Console.WriteLine("|____|");
			
			//Resets the cursors colour
			Console.ForegroundColor = ConsoleColor.White;

		}
	}


	class Deck
	{
		//The list of cards
		private List<Card> deck = new List<Card>();


		//Generates a new deck with a card of each rank for each suit
		public Deck()
		{
			//Gets all the card suits from the CARD class
			string[] Suits = Card.getSuits();
			//Gets all the card ranks from the CARD class
			string[] Ranks = Card.getRanks();
			
			//Iterates through each card suit
			foreach (string suit in Suits)
			{
				//Iterates through each card rank
				foreach(string rank in Ranks)
				{
					//Creates a new card with the current suit and rank
					Card newCard = new Card(suit, rank);
					//Adds the card to the deck
					this.deck.Add(newCard);
				}
			}
		}


		//Shuffles the deck
		public void shuffle()
		{
			Card movingCard;
			int initialPosition;
			int newPostion;

			Random randomPosition = new Random();

			//For each position, as long as the counts within the number of cards in the deck
			for (initialPosition = 0; initialPosition < deck.Count; initialPosition++)
			{
				//The new postion is a number between the current position and the end of the deck
				newPostion = randomPosition.Next(initialPosition, deck.Count);
				//Save the first card
				movingCard = deck[initialPosition];
				//The position of the first card is now set to the 
				deck[initialPosition] = deck[newPostion];
				deck[newPostion] = movingCard;
			}
		}


		//Selects a specified amount of cards to take off the deck and give to a player
		public List<Card> deal(int amount)
		{
			//This list will store the cards that are given to the player
			List<Card> dealtCards = new List<Card>();
			//Counter for while
			int i = 0;

			//While the counter is smaller than the amount of cards to deal
			while(i < amount)
			{
				if (deck.Count > 0)
				{
					//Add the card on the top of the deck to the dealt cards list
					dealtCards.Add(deck[deck.Count - 1]);
					//Remove the dealt card from the deck
					deck.Remove(deck[deck.Count - 1]);
					//Increase the counter
					i++;
				}
				else
				{
					Console.WriteLine("There are no cards left in the deck!");
					return dealtCards;
				}
			}

			//Return the list of dealt cards
			return dealtCards;
		}

		/*-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
		 *	TESTING FEATURES
		 *	These are not used in the main program, only to test the application
		 -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=*/

		//Used for testing the shuffle feature
		public void writeDeck()
		{
			//The cards are written horizontally using the set cursor position command
			//These variables control the cursor position
			int x = 0;
			int y = 2;

			//For every card the player has
			foreach (Card card in deck)
			{
				//Write the card at the positions specified
				card.writeCard(x, y);

				//After a card is written move the cursor 7 spaces along to prevent it writing over a card
				x = x + 7;
				//If the cursor is 64 spaces along
				if (x > 64)
				{
					//Reset it to prevent it going off screen
					x = 0;
					//Move it down the y axis to write the cards on the next row
					y = y + 5;
				}
			}
			Console.WriteLine("Card Count: "+deck.Count());
			return;
		}

		//Gets the total value of the deck
		public void deckValue()
		{
			int totalValue = 0;
			foreach (Card card in deck)
			{
				totalValue = totalValue + card.getValue();
			}
			Console.WriteLine(totalValue);
		}

		//Counts the amount of cards in the deck
		public void deckCount()
		{
			//Used to store the current cursor positions so it can be set back to normal
			int x, y;
			x = Console.CursorLeft;
			y = Console.CursorTop;

			//Sets the cursor position to the top left hand side of the screen
			Console.SetCursorPosition(Console.WindowWidth - 20, 0);
			//Writes the deck count
			Console.WriteLine("Deck Count: "+deck.Count());
			//Resets the cursor position
			Console.SetCursorPosition(x, y);
		}
	}


	class Player
	{
		//Holds the cards in a hand
		private List<Card> hand = new List<Card>();

		//Variables
		public string Name { get; set; }


		//Used to create a new player
		public Player(string name)
		{
			//Gets the name from the passed value
			Name = name;
			//Creates a new hand
			hand = new List<Card>();
		}

		//Adds a card to the players hand
		public void addCard(List<Card> dealtCard)
		{
			//Recieves a list of cards to be added
			//This allows the deal function to add multiple cards at once at the start of the game
			
			//For each card the functions given
			foreach (Card card in dealtCard)
			{
				//Add the card to the hand
				hand.Add(card);
			}
		}

		//Gets the value of the players hand
		public int value()
		{
			//Sets the hands value to 0
			int handValue = 0;
			//For each card in the hand
			foreach(Card card in hand)
			{
				//Get its value and add it to the hand value
				handValue = handValue + card.getValue();
			}

			//Return the hands value
			return handValue;
		}

		//Writes the cards to the console
		public void display()
		{
			//Writes the players name
			Console.WriteLine(Name + "'s Cards:");

			//The cards are written horizontally using the set cursor position command
			//These variables control the cursor position
			int x = 0;
			int y = 2;

			//For every card the player has
			foreach (Card card in hand)
			{
				//Write the card at the positions specified
				card.writeCard(x, y);

				//After a card is written move the cursor 7 spaces along to prevent it writing over a card
				x = x+7;
				//If the cursor is 64 spaces along
				if(x > 64)
				{
					//Reset it to prevent it going off screen
					x = 0;
					//Move it down the y axis to write the cards on the next row
					y = y + 5;
				}
			}

			//Write the value of the players hand
			Console.WriteLine("\nHand Value: " + value());
		}
	}





	class Workspace
	{
		public static void Main()
		{

			//Allows the console to write unicode characters (used to display suits)
			Console.OutputEncoding = Encoding.Unicode;

			string name;
			bool playing = true;
			string repeatAnswer;
			bool validRepeatAnswer;
			bool validAction;
			bool helpValid;
			bool canTwist;
			bool NPCStop;
			bool playerBust = false;

			//Gets name
			Console.WriteLine("Whats your name?");
				name = Console.ReadLine();
			
				do
				{
					//Ask if they want to here the rules
					Console.WriteLine("Would you like an explanaiton of how to play? (Y/N)");
					//Save responce
					string responce = Console.ReadLine().ToLower();
					Console.WriteLine("");
					//Test responce
					switch (responce)
					{
						//If yes
						case "y":
						{//They entered a valid answer
						helpValid = true;

						//Write the rules
						Console.WriteLine("The aim of the game is to get your cards totalling, or close 21 without going over.");
						Console.WriteLine("Players will be dealt 2 cards and can chose weather they stick (don't take any more) or twist (take another card) to try and get as close to 21 as possible.");
						Console.WriteLine("If the player goes over 21, they will go bust and lose.");
						Console.WriteLine("If neither player reaches 21, the player who got closer wins.");
						Console.WriteLine("A player can twist as many times as they want, as long as they havent gone bust");
						Console.WriteLine("");
						Console.WriteLine("When answering this program, please enter the letters in the brackets of the questions.");
						Console.ReadLine();

							//Move onto the next part
						}
							break;

						//If no
						case "n":
						{
							//They entered a valid answer
							helpValid = true;
							//Move onto the next part
						}
							break;

						//If anything else was entered
						default:
						{
							//It was not valid
							helpValid = false;
							//Prompt a proper responce
							Console.WriteLine("This input was invalid.");
							Console.WriteLine("Please enter a letter from the brackets at the end of the question.");
							//Close statement, repeating question
						}
							break;
					}

				} while (helpValid == false);
				
			//This section of code repeats if the player wants to play a 2nd game
			while (playing)
			{
				//Creates deck
				var deck = new Deck();
				deck.shuffle();

				
				//Creates players
				Player NPC = new Player("NPC");
				Player player = new Player(name);


				List<Card> blackjack = new List<Card>();
				blackjack.Add(new Card("Club", "K"));
				blackjack.Add(new Card("Club", "Q"));

				//Adds 2 cards to each players deck

				NPC.addCard(deck.deal(2));
				player.addCard(deck.deal(2));
				
				//Clears the console
				Console.Clear();

				
				//Welcomes the player
				 Console.WriteLine("Welcome " + name);

				//Displays the players hand
				player.display();

				//Do atleast once as the player can still twist
				do
				{
					//Repeat until the player gives a valid answer
					do
					{
						//Asks the player to stick or twist
						Console.WriteLine("Do you want to Stick or Twist? (S/T)");
						//Stores responce in the action variable
						string action = Console.ReadLine().ToLower();

						//Tests the players action
						switch (action)
						{
							//If they chose to stick
							case "s":
								//They made a valid action, closing the do while loop
								validAction = true;
								//Clear the console
								Console.Clear();
								//Display thier current cards
								player.display();
								//Write thier action
								Console.WriteLine("");
								Console.WriteLine("You chose to stick with your current hand");
								//The player can no longer twist, closing the do while loop
								canTwist = false;
								playerBust = false;
								//Exit the statment, moving on to the win checks
								break;


							//If they chose to twist
							case "t":
								//They made a valid action, closing the do while loop
								validAction = true;
								//Clear the console
								Console.Clear();
								//Deal the player 1 new card
								player.addCard(deck.deal(1));
								//Display thier hand
								player.display();
								//Write thier action
								Console.WriteLine("");
								Console.WriteLine("You chose to twist another card");
								//They can still twist, as they didnt chose to stick
								canTwist = true;
								break;


							//If the user gives an invalid answer
							default:
								//They made an ivalid action, causing the question to repeat
								validAction = false;
								//They are told to make a valid action
								Console.WriteLine("This input was invalid.");
								Console.WriteLine("Please enter a letter from the brackets at the end of the question.");
								//They can still twist
								canTwist = true;
								break;
						}

						//If the player's hand has a value of over 21
						if(player.value() > 21)
						{
							//They can no longer twist as they are bust, closing the while loop
							playerBust = true;
							canTwist = false;
						}


					} while (validAction == false);

				} while (canTwist);


				Console.WriteLine("");


				//Resets the NPCStop variable, allowing them to take turns
				NPCStop = false;

				//While the player is not bust
				while (!playerBust)
				{
					//Do the NPCs turn
					do
					{
						//If the non player character has a hand value over 18 they will stick
						if (NPC.value() > 18)
						{
							//Write choice
							Console.WriteLine(NPC.Name + " chose to stick!");
							//They chose to stop
							playerBust = true;
							NPCStop = true;
						}
						//If the hand value is under 18 they twist
						else if (NPC.value() <= 18)
						{
							//Write choice
							Console.WriteLine(NPC.Name + " chose to twist!");
							//Deal Card
							NPC.addCard(deck.deal(1));
							//They havent chose to stop
							NPCStop = false;
						}

					} while (NPCStop == false);
				}


				Console.WriteLine("");

			/*
			 Winning checks
				 */

				//Gets both players scores
				int playerScore = player.value();
				int NPCScore = NPC.value();

				//Changes the text colour to yellow
				Console.ForegroundColor = ConsoleColor.Yellow;

				//If player got higher than NPC and under 21
				if(playerScore>NPCScore  && playerScore <= 21)
				{
					//They win
					Console.WriteLine("You win!");
				}
				//If player gets over 21
				else if(playerScore > 21)
				{
					//The lose
					Console.WriteLine("You lose, you went bust!");
				}
				//If player score is the same as NPC score
				else if (playerScore == NPCScore)
				{
					//They draw
					Console.WriteLine("Its a draw!");
				}
				//If NPC score is greater than player score or over 21
				else if (playerScore < NPCScore && NPCScore <=21)
				{
					//Player loses
					Console.WriteLine("You lose, " +NPC.Name + " wins!");
				}
				//If NPC gets over 21
				else if (NPCScore > 21)
				{
					//Player wins
					Console.WriteLine(NPC.Name + " went bust! You win!");
				}
				

				//Writes the scores to the screen
				Console.WriteLine("");
				Console.WriteLine("Final Scores");
				Console.WriteLine("Player Score: " + playerScore);
				Console.WriteLine("NPC Score:    " + NPCScore);
				Console.ReadLine();

				//Resets the cursor colour
				Console.ForegroundColor = ConsoleColor.White;



				do
				{
					Console.WriteLine("Do you want to play again? (Y/N)");
					repeatAnswer = Console.ReadLine();
					switch (repeatAnswer.ToLower())
					{
						case "y":
							validRepeatAnswer = true;
							playing = true;
							Console.WriteLine("A new game will now begin!");
							Console.ReadLine();
							Console.Clear();
							break;
						case "n":
							validRepeatAnswer = true;
							playing = false;
							Console.WriteLine("Thank you for playing. Press enter to close.");
							Console.ReadLine();
							break;
						default:
							validRepeatAnswer = false;
							Console.WriteLine("This input was invalid.");
							Console.WriteLine("Please enter a letter from the brackets at the end of the question.");
							break;
					}
				} while (validRepeatAnswer == false);
			}
		}
	}
}
