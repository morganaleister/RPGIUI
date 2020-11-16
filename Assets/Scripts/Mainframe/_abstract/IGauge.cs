namespace Scripts.MainframeReference
{
    public interface IGauge
    {
        string Label { get; set; }
        string Current { get; set; }
        string Max { get; set; }
    }
}