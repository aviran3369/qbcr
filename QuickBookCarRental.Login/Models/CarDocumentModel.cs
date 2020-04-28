using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickBookCarRental.Login.Models
{
    public enum CarDocumentType
    {
        Insurance = 1,
        License = 2,
        Other = 99
    }

    public class CarDocumentModel : BaseFileModel
    {
        public int Id { get; set; }
        public CarDocumentType DocumentTypeId { get; set; }
        public string MoreDetails { get; set; }
    }
}