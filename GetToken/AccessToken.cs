using System.Runtime.Serialization;

namespace GetToken
{
    [DataContract]
    public class AccessToken
    {
        [DataMember]
        public string Token { get; set; }
    }
}
