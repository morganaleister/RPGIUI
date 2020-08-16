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
    public interface IStackable
    {
        bool IsStackable { get; }
        int Stacked { get; }
        void Stack();
    }

}