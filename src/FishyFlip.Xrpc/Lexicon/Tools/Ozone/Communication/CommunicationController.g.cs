// <auto-generated />
// This file was generated by FFSourceGen.
// Do not modify this file.

#nullable enable annotations
#nullable disable warnings

namespace FishyFlip.Xrpc.Lexicon.Tools.Ozone.Communication
{

    /// <summary>
    /// tools.ozone.communication XRPC Group.
    /// </summary>
    [ApiController]
    public abstract class CommunicationController : ControllerBase
    {

        /// <summary>
        /// Administrative action to create a new, re-usable communication (email for now) template.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.DuplicateTemplateNameError"/>  <br/>
        /// </summary>
        /// <param name="name">Name of the template.</param>
        /// <param name="contentMarkdown">Content of the template, markdown supported, can contain variable placeholders.</param>
        /// <param name="subject">Subject of the message, used in emails.</param>
        /// <param name="lang">Message language.</param>
        /// <param name="createdBy">DID of the user who is creating the template.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Communication.TemplateView"/></returns>
        [HttpPost("/xrpc/tools.ozone.communication.createTemplate")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Tools.Ozone.Communication.TemplateView>, ATErrorResult>> CreateTemplateAsync ([FromBody] FishyFlip.Lexicon.Tools.Ozone.Communication.CreateTemplateInput input, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a communication template.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="Success"/></returns>
        [HttpPost("/xrpc/tools.ozone.communication.deleteTemplate")]
        public abstract Task<Results<Ok, ATErrorResult>> DeleteTemplateAsync ([FromBody] FishyFlip.Lexicon.Tools.Ozone.Communication.DeleteTemplateInput input, CancellationToken cancellationToken);

        /// <summary>
        /// Get list of all communication templates.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Communication.ListTemplatesOutput"/></returns>
        [HttpGet("/xrpc/tools.ozone.communication.listTemplates")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Tools.Ozone.Communication.ListTemplatesOutput>, ATErrorResult>> ListTemplatesAsync (CancellationToken cancellationToken = default);

        /// <summary>
        /// Administrative action to update an existing communication template. Allows passing partial fields to patch specific fields only.
        /// <br/> Possible Errors: <br/>
        /// <see cref="FishyFlip.Lexicon.DuplicateTemplateNameError"/>  <br/>
        /// </summary>
        /// <param name="id">ID of the template to be updated.</param>
        /// <param name="name">Name of the template.</param>
        /// <param name="lang">Message language.</param>
        /// <param name="contentMarkdown">Content of the template, markdown supported, can contain variable placeholders.</param>
        /// <param name="subject">Subject of the message, used in emails.</param>
        /// <param name="updatedBy">DID of the user who is updating the template.</param>
        /// <param name="disabled"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Result of <see cref="FishyFlip.Lexicon.Tools.Ozone.Communication.TemplateView"/></returns>
        [HttpPost("/xrpc/tools.ozone.communication.updateTemplate")]
        public abstract Task<Results<ATResult<FishyFlip.Lexicon.Tools.Ozone.Communication.TemplateView>, ATErrorResult>> UpdateTemplateAsync ([FromBody] FishyFlip.Lexicon.Tools.Ozone.Communication.UpdateTemplateInput input, CancellationToken cancellationToken);
    }
}

