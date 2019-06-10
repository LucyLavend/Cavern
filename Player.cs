namespace CavernCS
{
    public class Player
    {
        private Room currentRoom;
        private uint health;

        public Inventory inventory;

        public Player()
        {
            inventory = new Inventory();
            health = 100;
        }

        public uint getHealth()
        {
            return this.health;
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
