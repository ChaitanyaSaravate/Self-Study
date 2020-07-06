using System.Runtime.Serialization;

namespace WebServiceContracts
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string Address1 { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string Address3 { get; set; }
        [DataMember]
        public string Address4 { get; set; }
       
        [DataMember] 
        public string Address5 { get; set; }
        [DataMember]
        public string Address6 { get; set; }
        [DataMember]
        public string Address7 { get; set; }
        [DataMember]
        public string Address8 { get; set; }
    }
}