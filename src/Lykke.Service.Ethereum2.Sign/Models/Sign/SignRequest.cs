using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lykke.Service.Ethereum2.Sign.Models.Sign
{
    [DataContract]
    public class SignRequest
    {
        [DataMember(Name = "transactionContext")]
        public string TransactionContext { get; set; }

        [DataMember(Name = "privateKeys")] 
        public IReadOnlyCollection<string> PrivateKeys { get; set; }
    }
}
