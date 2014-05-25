namespace LastFmApiJsNet.Services
{
    /// <summary>
    /// Objects that implement this have url pages at Last.fm
    /// </summary>
    public interface IHasURL
    {
        /// <summary>
        /// Returns the Last.fm page of this object.
        /// </summary>
        /// <param name="language">
        /// A <see cref="SiteLanguage"/>
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>
        /// </returns>
        string GetURL(SiteLanguage language);

        /// <value>
        /// The Last.fm page of this object.
        /// </value>
        string URL { get; }
    }
}
