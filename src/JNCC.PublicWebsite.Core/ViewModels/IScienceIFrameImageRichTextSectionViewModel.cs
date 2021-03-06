﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceIFrameImageRichTextSectionViewModel
    {
        ImageViewModel Image { get; set; }

        string ImagePosition { get; set; }

        IHtmlString Content { get; set; }
    }
}
