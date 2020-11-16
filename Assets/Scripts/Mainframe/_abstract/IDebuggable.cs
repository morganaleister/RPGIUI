namespace Scripts.MainframeReference
{
    public interface IDebuggable
    {
        System.Type Type { get; }
        void Debug(string s);

    }
}