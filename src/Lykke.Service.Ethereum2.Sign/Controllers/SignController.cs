using System;
using System.Linq;
using System.Net;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Service.Ethereum2.Sign.Models.Sign;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Swashbuckle.AspNetCore.Annotations;

namespace Lykke.Service.Ethereum2.Sign.Controllers
{
    [Route("api/[controller]")]
    public class SignController : Controller
    {
        [HttpPost]
        [SwaggerOperation(nameof(SignRawTx))]
        [ProducesResponseType(typeof(SignResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public IActionResult SignRawTx([FromBody] SignRequest sourceTx)
        {
            if (sourceTx.PrivateKeys.Count != 1)
            {
                BadRequest("One private key expected");
            }

            var unsignedTransaction = sourceTx.TransactionContext;

            RLPSigner rlpEncoder;
            byte[] transactionRawBytes;

            try
            {
                transactionRawBytes = unsignedTransaction.HexToByteArray();
            }
            catch (FormatException)
            {
                return BadRequest(ErrorResponse.Create($"Unsigned transaction is not a hex string: {unsignedTransaction}"));
            }

            try
            {
                rlpEncoder = SignedTransactionBase.CreateDefaultRLPSigner(transactionRawBytes);
            }
            catch (Exception)
            {
                return BadRequest(ErrorResponse.Create($"Invalid unsigned transaction format: {unsignedTransaction}"));
            }

            var secret = new EthECKey(sourceTx.PrivateKeys.First());
            
            rlpEncoder.Sign(secret);
            
            var signedTx = new Transaction(rlpEncoder);
            
            return Ok(new SignResponse
            {
                SignedTransaction = signedTx.GetRLPEncoded().ToHex()
            });
        }
    }
}
