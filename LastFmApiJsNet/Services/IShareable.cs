namespace LastFmApiJsNet.Services
{
    public interface IShareable
    {
        void Share(Recipients recipients, string message);
        void Share(Recipients recipients);
    }
}
