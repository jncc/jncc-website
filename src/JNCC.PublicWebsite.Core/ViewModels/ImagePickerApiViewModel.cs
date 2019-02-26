using System;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ImagePickerApiViewModel
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string FileType { get; set; }
        public DateTime LastEdited { get; set; }
        public double SizeInKB { get; set; }
        public IDictionary<string, string> Crops { get; set; }
    }
}
