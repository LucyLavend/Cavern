﻿using System;

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
			Room outside, theatre, pub, lab, office;

			// create the rooms
			outside = new Room("at the center of the cavesystem");
			theatre = new Room("at the temple");
			pub = new Room("at the hideout");
			lab = new Room("at the waterfall");
			office = new Room("at the cold dead end");

			// initialise room exits
			outside.setExit("east", theatre);
			outside.setExit("south", lab);
			outside.setExit("west", pub);

			theatre.setExit("west", outside);

			pub.setExit("east", outside);

			lab.setExit("north", outside);
			lab.setExit("east", office);

			office.setExit("west", lab);

			player.setCurrentRoom(outside);  // start game outside
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
			Console.WriteLine("Welcome to Cavern!");
			Console.WriteLine("Cavern is a small text adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
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
                Console.ForegroundColor = ConsoleColor.White;
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
			Console.WriteLine("You wanted to take a shortcut, but you got lost.");
			Console.WriteLine("You wander around in a cave system.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
		}

        private void showCompass()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("    N    ");
            Console.WriteLine("    |    ");
            Console.WriteLine("W--(*)--E");
            Console.WriteLine("    |    ");
            Console.WriteLine("    S    ");
            Console.ForegroundColor = ConsoleColor.White;
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
                Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("There is no cave to " + direction + "!");
                Console.ForegroundColor = ConsoleColor.White;
			} else
            {
                player.damage(10);
                player.setCurrentRoom(nextRoom);
                Console.WriteLine(player.getCurrentRoom().getLongDescription());
                Console.Title = player.getCurrentRoom().getShortDescription();
			}
		}

	}
}
