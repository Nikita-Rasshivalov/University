namespace Common
{
    [Serializable]
    public class ImageFigures
    {
        public string FileName { get; set; }
        public List<RawFigure> Figures { get; set; }
    }
}
