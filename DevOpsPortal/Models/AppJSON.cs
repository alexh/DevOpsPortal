using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOpsPortal.Models {
    public class AppJSON {

        public string AppName { get; set; }
        public AppJSON(string name) {
            this.AppName = name;

        }


    }
}