namespace Scripts
{
    public class Achievement
    {        
        public string Title { get; private set; }
        public string Description { get; private set; }
        public float Progress { get; private set; }
        public string ImgPath { get; private set; }

        public Achievement(string title, string description, string imgPath, float progress)
        {
            Title = title;
            Description = description;
            ImgPath = imgPath;
            Progress = progress;
        }

    }
}