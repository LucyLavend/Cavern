namespace CavernCS
{
    public class Player
    {
        private Room currentRoom;
        private Room lastRoom;
        private uint health;
        private bool isBleeding;

        public Inventory inventory;

        public Player()
        {
            inventory = new Inventory("Your inventory");
            health = 100;
        }

        public bool IsBleeding { get; set; }

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

        public Room getLastRoom()
        {
            return lastRoom;
        }

        public void setLastRoom(Room r)
        {
            lastRoom = r;
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
