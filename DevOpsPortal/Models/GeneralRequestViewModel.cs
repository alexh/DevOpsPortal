using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevOpsPortal.Models {
    public class GeneralRequestViewModel {

        public int GeneralRequestID { get; set; }

        [StringLength(50)]
        public string User { get; set; }

        [StringLength(50)]
        public string Team { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
        public HttpPostedFileBase Attachment { get; set; }

    }
}