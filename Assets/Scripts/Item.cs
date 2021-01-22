namespace Scripts.Mainframe
{
    public class Item : BaseObject
    {
        public Item(string itemName) : base(itemName) { }

    }
    public interface IStackable
    {
        bool IsStackable { get; }
        int Stacked { get; }
        void Stack();
    }

}