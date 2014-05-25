using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastFmApiJsNet.Services
{
    /// <summary>
    /// Objects that implement this can be tagged on Last.fm.
    /// </summary>
    public interface ITaggable
    {
        void AddTags(params Tag[] tags);
        void AddTags(params String[] tags);
        void AddTags(TagCollection tags);
        Tag[] GetTags();
        TopTag[] GetTopTags();
        TopTag[] GetTopTags(int limit);
        void RemoveTags(params string[] tags);
        void RemoveTags(params Tag[] tags);
        void RemoveTags(TagCollection tags);
        void SetTags(Tag[] tags);
        void SetTags(string[] tags);
        void SetTags(TagCollection tags);
        void ClearTags();
    }
}
