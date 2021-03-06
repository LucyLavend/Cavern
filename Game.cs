﻿using System;

namespace CavernCS
{
	public class Game
	{
		private Parser parser;
        private Player player;
		private Room center, temple, hideout, waterfall, deadend, underwater;

        //Items
        private Item ring, crowbar, arrowhead, bandaid;

		public Game ()
		{
            Console.Title = "Cavern";
            player = new Player();
			parser = new Parser();

			createRooms();
            createItems();

            player.inventory.MaxWeight = 10f;
        }

		private void createRooms()
		{

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

        private void createItems()
        {
            ring = new Ring("ring", "An old looking ring", .1f);
            crowbar = new Crowbar("crowbar", "A rusty crowbar", 1.3f);
            arrowhead = new Arrowhead("arrowhead", "A sharp looking arrow tip", .2f, player);
            bandaid = new Bandaid("bandaid", "Just in case", .04f, player);

            //add to player inventory
            player.inventory.addItem(bandaid);

            //add items to the rooms
            center.inventory.addItem(ring);
            hideout.inventory.addItem(crowbar);
            deadend.inventory.addItem(arrowhead);
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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to Cavern!");
            Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("Cavern is a small text adventure game.");
			Console.WriteLine("You ended up somewhere deep in a cave \nsystem and you have to find your way out!");
            Console.WriteLine();
            Console.WriteLine("Type 'compass' for a compass.");
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
                    Console.WriteLine("--------------------");
                    Console.WriteLine(player.getCurrentRoom().getLongDescription());
                    printRoomContents();
                    break;
                case "quit":
					wantToQuit = true;
					break;
                case "compass":
                    showCompass();
                    break;
                case "check":
                    showStats();
                    break;
                case "clear":
                    clearConsole();
                    break;
                case "inv":
                    player.inventory.showContents();
                    break;
                case "take":
                    takeItem(command);
                    break;
                case "drop":
                    dropItem(command);
                    break;
                case "use":
                    useItem(command);
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
	    	Console.WriteLine("<?-------------------------------------------?>");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("You wanted to take a shortcut, but you got lost.");
			Console.WriteLine("You wander around in a cave system.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
            Console.ForegroundColor = ConsoleColor.DarkGray;
	    	Console.WriteLine("<?-------------------------------------------?>");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void printRoomContents()
        {
            if (player.getCurrentRoom().inventory.getItemCount() != 0)
            {
                Console.WriteLine("You see these items in the cave:");
                player.getCurrentRoom().inventory.showContents();
            }
            else
            {
                Console.WriteLine("There are no items in this cave.");
            }
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

        private void showStats()
        {
            Console.WriteLine("Your current health is " + player.getHealth() + ".");
        }

        /**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
        private void goRoom(Command command)
		{

            if (!command.hasSecondWord()) {
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.getSecondWord();

            if (direction != "back")
            {
                //set last room
            }

            // Try to leave current room.
            Room nextRoom = player.getCurrentRoom().getExit(direction);
            Room lastRoom = player.getCurrentRoom();


            if (nextRoom == null && direction != "back") {
                Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("There is no cave to " + direction + "!");
                Console.ForegroundColor = ConsoleColor.Gray;
			}
            else
            {
	    		Console.WriteLine("----");
                if(direction == "back")
                {
                    player.setCurrentRoom(player.getLastRoom());
                }
                else
                {
                    player.setCurrentRoom(nextRoom);
                }
                Console.WriteLine(player.getCurrentRoom().getLongDescription());
                //set text above the window to the current room
                Console.Title = "Cavern: " + player.getCurrentRoom().getShortDescription();
                //damage player if wounded
                if (player.IsBleeding)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    player.damage(1);
                    Console.WriteLine("You have a wound and lost a bit of blood!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            player.setLastRoom(lastRoom);

        }

        private void takeItem(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know what to take
                Console.WriteLine("Take what?");
                return;
            }

            string item = command.getSecondWord();
            bool test = player.inventory.swapItem(player.getCurrentRoom().inventory, player.getCurrentRoom().inventory.findItem(item));
            if (!test)
            {
                Console.WriteLine("Item could not be found.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You took the " + item + ".");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private void dropItem(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know what to drop
                Console.WriteLine("Drop what?");
                return;
            }

            string item = command.getSecondWord();
            bool test = player.getCurrentRoom().inventory.swapItem(player.inventory, player.inventory.findItem(item));
            if (!test)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Cannot drop " + item + ".");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You dropped the " + item + ".");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        //use item
        private void useItem(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know what to use
                Console.WriteLine("Use what?");
                return;
            }
            else if (!command.hasThirdWord())
            {
                // if there is no third word, we don't know what to use it on
                if (player.inventory.findItem(command.getSecondWord()) != null)
                {
                    player.inventory.findItem(command.getSecondWord()).use();
                }
                else
                {
                    Console.WriteLine(command.getSecondWord() +  " not found in inventory!");
                }
                return;
            }
            else
            {
                if (player.inventory.findItem(command.getSecondWord()) != null && player.inventory.findItem(command.getThirdWord()) != null)
                {
                    player.inventory.findItem(command.getSecondWord()).use(command.getThirdWord());
                }
                else
                {
                    Console.WriteLine(command.getSecondWord() + " or " + command.getThirdWord() + " not found in inventory!");
                }
            }

        }

        private void clearConsole()
        {
            Console.Clear();
            printWelcome();
        }

        private void printLogo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@" ___  ___  _ _  ___  ___  _ _ ");
            Console.WriteLine(@"|  _|| . || | || __|| . \| \ |");
            Console.WriteLine(@"| <_ |   || ' || _| |   /|   |");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"\___||_|_||__/ |___||_\_\|_\_|");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

    }
}
