using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastFmApiJsNet.Services
{
    public class TopItem<T>
    {
        /// <summary>
        /// The concerned item.
        /// </summary>
        public T Item { get; private set; }

        /// <summary>
        /// The weight of this item in the list. A playcount, tagcount or a percentage.
        /// </summary>
        public int Weight { get; private set; }

        public TopItem(T item, int weight)
        {
            Item = item;
            Weight = weight;
        }
    }
}
