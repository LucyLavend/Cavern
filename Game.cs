using System;

namespace CavernCS
{
	public class Game
	{
		private Parser parser;
        private Player player;

		public Game ()
		{
            Console.Title = "Cavern";
            player = new Player();
			parser = new Parser();
			createRooms();
		}

		private void createRooms()
		{
			Room center, temple, hideout, waterfall, deadend, underwater;

			// create the rooms
			center = new Room("at the center of the cavesystem");
			temple = new Room("at the underground temple");
			hideout = new Room("at the hideout");
			waterfall = new Room("at the waterfall");
			deadend = new Room("at the cold dead end");
			underwater = new Room("underwater");

			// initialise room exits
			center.setExit("east", temple);
			center.setExit("south", waterfall);
			center.setExit("west", hideout);

			temple.setExit("west", center);

			hideout.setExit("east", center);

			waterfall.setExit("north", center);
			waterfall.setExit("east", deadend);
            waterfall.setExit("down", underwater);

			deadend.setExit("west", waterfall);

            underwater.setExit("up", waterfall);

			player.setCurrentRoom(center);  // start game center
		}


		/**
	     *  Main play routine.  Loops until end of play.
	     */
		public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (! finished) {
				Command command = parser.getCommand();
				finished = processCommand(command);
                if (!player.isAlive())
                {
                    finished = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You died!");
                }
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
            printLogo();
			Console.WriteLine();
            Console.WriteLine("Welcome to Cavern!");
			Console.WriteLine("Cavern is a small text adventure game.");
			Console.WriteLine("You ended up somewhere deep in a cave system and you have to find your way out!");
            Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine("------------------------------------------");
			Console.WriteLine(player.getCurrentRoom().getLongDescription());
		}

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Command not found!");
                Console.ForegroundColor = ConsoleColor.Gray;
				return false;
            }

			string commandWord = command.getCommandWord();
			switch (commandWord) {
				case "help":
					printHelp();
					break;
                case "go":
                    goRoom(command);
                    break;
                case "look":
                    Console.WriteLine(player.getCurrentRoom().getLongDescription());
                    break;
                case "quit":
					wantToQuit = true;
					break;
                case "compass":
                    showCompass();
                    break;
			}

			return wantToQuit;
		}

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void printHelp()
		{
	    	Console.WriteLine("<?--------------------?>");
            Console.WriteLine("You wanted to take a shortcut, but you got lost.");
			Console.WriteLine("You wander around in a cave system.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
	    	Console.WriteLine("<?--------------------?>");
        }

        private void showCompass()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            Console.WriteLine("     N    ");
            Console.WriteLine("     |    ");
            Console.WriteLine(" W--(*)--E");
            Console.WriteLine("     |    ");
            Console.WriteLine("     S    ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
        private void goRoom(Command command)
		{
			if(!command.hasSecondWord()) {
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.getCurrentRoom().getExit(direction);

			if (nextRoom == null) {
                Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("There is no cave to " + direction + "!");
                Console.ForegroundColor = ConsoleColor.Gray;
			} else
            {
                player.damage(10);
                player.setCurrentRoom(nextRoom);
	    		Console.WriteLine("----");
                Console.WriteLine(player.getCurrentRoom().getLongDescription());
                Console.Title = "Cavern: " + player.getCurrentRoom().getShortDescription();
			}
		}

        private void printLogo()
        {
            Console.WriteLine(@" ___  ___  _ _  ___  ___  _ _ ");
            Console.WriteLine(@"|  _|| . || | || __|| . \| \ |");
            Console.WriteLine(@"| <_ |   || ' || _| |   /|   |");
            Console.WriteLine(@"\___||_|_||__/ |___||_\_\|_\_|");
        }

	}
}
