using Drastic.Tools;
using Drastic.ViewModels;
using FishyFlip;
using FishyFlip.Models;
using FishyFlip.Tools;

namespace SampleApp.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private string atIdentityEntry;
    private AsyncCommand profileSearchCommand;
    private ATProtocol protocol;
    
    public ProfileViewModel(IServiceProvider services)
        : base(services)
    {
        this.atIdentityEntry = "did:plc:yhgc5rlqhoezrx6fbawajxlh";
        var protocolBuilder = new ATProtocolBuilder().WithInstanceUrl(new Uri(FishyFlip.Constants.Urls.ATProtoServer.SocialApi));
        this.protocol = protocolBuilder.Build();
        this.profileSearchCommand = new AsyncCommand(this.ProfileSearchAsync, () => !string.IsNullOrEmpty(this.atIdentityEntry), this.Dispatcher, this.ErrorHandler);
    }

    public AsyncCommand ProfileSearchCommand => this.profileSearchCommand;

    public string AtIdentityEntry
    {
        get => this.atIdentityEntry;
        set
        {
            this.SetProperty(ref this.atIdentityEntry, value);
            this.RaiseCanExecuteChanged();
        }
    }

    public override void RaiseCanExecuteChanged()
    {
        base.RaiseCanExecuteChanged();
        this.ProfileSearchCommand.RaiseCanExecuteChanged();
    }

    private async Task ProfileSearchAsync()
    {
        ATIdentifier.TryCreate(this.AtIdentityEntry, out var identifier);
        if (identifier == null)
        {
            return;
        }

        await this.PerformBusyAsyncTask(
            async () =>
            {
                (var describeRepo, var error) = await this.protocol.Repo.DescribeRepoAsync(identifier);
                if (error is not null)
                {
                    // this.ErrorHandler.HandleError(error);
                    return;
                }
            },
            "Searching for Profile...");
    }
}