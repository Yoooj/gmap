using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapPhotos.ModelView
{
    public class ProfileWithPictures
    {
        public string NickName { get; set; }

        public IList<PhotoShortInfo> Photos { get; set; }
    }
}