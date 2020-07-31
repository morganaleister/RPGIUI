namespace Scripts.Mainframe
{
    public class ByRef<T>
    {
        public T Value { get; set; }
        public ByRef(T reference) => Value = reference;
    }
}