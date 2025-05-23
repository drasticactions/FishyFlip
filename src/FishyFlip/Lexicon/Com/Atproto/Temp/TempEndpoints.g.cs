// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Lexicon.Com.Atproto.Temp
{

    /// <summary>
    /// com.atproto.temp Endpoint Group.
    /// </summary>
    public static class TempEndpoints
    {

       public const string GroupNamespace = "com.atproto.temp";

       public const string AddReservedHandle = "/xrpc/com.atproto.temp.addReservedHandle";

       public const string CheckSignupQueue = "/xrpc/com.atproto.temp.checkSignupQueue";

       public const string RequestPhoneVerification = "/xrpc/com.atproto.temp.requestPhoneVerification";


        /// <summary>
        /// Add a handle to the set of reserved handles.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="handle"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput?>> AddReservedHandleAsync (this FishyFlip.ATProtocol atp, string handle, CancellationToken cancellationToken = default)
        {
            var endpointUrl = AddReservedHandle.ToString();
            var headers = new Dictionary<string, string>();
            var inputItem = new AddReservedHandleInput();
            inputItem.Handle = handle;
            return atp.Post<AddReservedHandleInput, FishyFlip.Lexicon.Com.Atproto.Temp.AddReservedHandleOutput?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoTempAddReservedHandleInput!, atp.Options.SourceGenerationContext.ComAtprotoTempAddReservedHandleOutput!, inputItem, cancellationToken, headers);
        }


        /// <summary>
        /// Check accounts location in signup queue.
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Com.Atproto.Temp.CheckSignupQueueOutput?"/></returns>
        public static Task<Result<FishyFlip.Lexicon.Com.Atproto.Temp.CheckSignupQueueOutput?>> CheckSignupQueueAsync (this FishyFlip.ATProtocol atp, CancellationToken cancellationToken = default)
        {
            var endpointUrl = CheckSignupQueue.ToString();
            var headers = new Dictionary<string, string>();
            if (atp.TryFetchProxy(GroupNamespace, out var proxyUrl))
            {
                headers.Add(Constants.AtProtoProxy, proxyUrl);
            }
            headers.Add(Constants.AtProtoAcceptLabelers, atp.Options.LabelDefinitionsHeader);
            return atp.Get<FishyFlip.Lexicon.Com.Atproto.Temp.CheckSignupQueueOutput>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoTempCheckSignupQueueOutput!, cancellationToken, headers);
        }


        /// <summary>
        /// Request a verification code to be sent to the supplied phone number
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success?"/></returns>
        public static Task<Result<Success?>> RequestPhoneVerificationAsync (this FishyFlip.ATProtocol atp, string phoneNumber, CancellationToken cancellationToken = default)
        {
            var endpointUrl = RequestPhoneVerification.ToString();
            var headers = new Dictionary<string, string>();
            var inputItem = new RequestPhoneVerificationInput();
            inputItem.PhoneNumber = phoneNumber;
            return atp.Post<RequestPhoneVerificationInput, Success?>(endpointUrl, atp.Options.SourceGenerationContext.ComAtprotoTempRequestPhoneVerificationInput!, atp.Options.SourceGenerationContext.Success!, inputItem, cancellationToken, headers);
        }

    }
}

