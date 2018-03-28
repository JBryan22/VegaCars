using System.Linq;

namespace Vega_New.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedFileTypes.Any(s => s == System.IO.Path.GetExtension(fileName).ToLower());
        }
    }
}