using Lykke.Service.Ethereum2.Sign.Models.Wallet;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Signer;
using Nethereum.Util;

namespace Lykke.Service.Ethereum2.Sign.Controllers
{
    [Route("api/[controller]")]
    public class WalletsController
    {
        [HttpPost]
        public WalletCreationResponse CreateWallet()
        {
            var key = EthECKey.GenerateKey();
            var address = AddressUtil.Current.ConvertToChecksumAddress(key.GetPublicAddress());

            return new WalletCreationResponse
            {
                PublicAddress = address,
                PrivateKey = key.GetPrivateKey()
            };
        }
    }
}
