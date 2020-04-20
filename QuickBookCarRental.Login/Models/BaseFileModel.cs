﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickBookCarRental.Login.Models
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }

    public class BaseFileModel
    {
        public List<UploadedFile> Files { get; set; }
    }
}