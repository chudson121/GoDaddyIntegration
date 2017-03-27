using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDaddyIntegration
{
    

    public class DomainDto
    {
        public int domainId { get; set; }
        public string domain { get; set; }
        public string status { get; set; }
        public object expires { get; set; }
        public bool expirationProtected { get; set; }
        public bool holdRegistrar { get; set; }
        public bool locked { get; set; }
        public bool privacy { get; set; }
        public bool renewAuto { get; set; }
        public bool renewable { get; set; }
        public DateTime renewDeadline { get; set; }
        public bool transferProtected { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime deletedAt { get; set; }
    }

}
