using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Player
    {
        private Room currentRoom;
        private uint health;

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

        public void damage(uint amount)
        {
            health -= amount;
        }
            
        public void heal(uint amount)
        {
            health += amount;
        }
        
        public bool isAlive()
        {
            return health > 0;
        }
    }
}
