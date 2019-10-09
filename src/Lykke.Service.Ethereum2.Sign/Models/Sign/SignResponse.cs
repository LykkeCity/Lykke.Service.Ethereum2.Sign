using System.Runtime.Serialization;

namespace Lykke.Service.Ethereum2.Sign.Models.Sign
{
    [DataContract]
    public class SignResponse
    {
        [DataMember(Name = "signedTransaction")]
        public string SignedTransaction { get; set; }
    }
}
