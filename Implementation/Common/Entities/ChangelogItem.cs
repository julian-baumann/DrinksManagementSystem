using System.Collections.Generic;

namespace Common.Entities
{
    public class ChangelogItem<T>
    {
        public ChangeTypes Type { get; set; }
        public List<T> Item { get; set; } = new();
    }
}