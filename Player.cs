using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Player
    {
        public Room currentRoom;

        public Player()
        {

        }

        public Room getCurrentRoom()
        {
            return currentRoom;
        }

        public void setCurrentRoom(Room r)
        {
            currentRoom = r;
        }
    }
}
