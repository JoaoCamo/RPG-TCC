
namespace Game.Shared.Backend.Data
{
    [System.Serializable]
    public struct ImageRequestData
    {
        public string imageContext;

        public ImageRequestData(string imageContext)
        {
            this.imageContext = imageContext;
        }
    }
}