namespace Scripts
{
    public class Item : BaseObject
    {
        public string ItemName { get; set; }

    }
    public interface IStackable
    {
        bool IsStackable { get; }
        int Stacked { get; }
        void Stack();
    }

}