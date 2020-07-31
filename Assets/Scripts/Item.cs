namespace Scripts
{
    public class Item : BaseObject
    {
        public string ItemName { get; set; }

        public Item(string name): base(name)
        {
            ItemName = name;

        }
    }

}