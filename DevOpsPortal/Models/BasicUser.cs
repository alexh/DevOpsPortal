using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOpsPortal.Models {
    public class BasicUser {


        public BasicUser() {

        }

        public BasicUser(User u) {
            this.ID = u.User_ID.ToString();
            this.Name = u.FirstName.TrimEnd() + " " + u.LastName.TrimEnd();
        }
        public string ID { get; set; }

        public string Name { get; set; }

    }
}