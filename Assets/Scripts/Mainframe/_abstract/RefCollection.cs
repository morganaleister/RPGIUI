namespace Scripts.MainframeReference
{
    public class RefCollection<T> 
    {
        public T[] Values { get; set; }

        public static implicit operator T[](RefCollection<T> refCollection) => refCollection.Values;
        public T this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }
    }
}