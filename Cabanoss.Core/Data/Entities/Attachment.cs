using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class Attachment : BaseEentity
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        //Navigation
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
