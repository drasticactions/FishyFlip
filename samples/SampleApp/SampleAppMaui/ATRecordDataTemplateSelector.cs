using FishyFlip.Models;
using SampleApp;

namespace SampleAppMaui;

public class ATRecordDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate GenericTemplate { get; set; }

    public DataTemplate LikeTemplate { get; set; }

    public DataTemplate PostTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        switch (((ATRecord)item).Type)
        {
            case FishyFlip.Constants.FeedType.Like:
                return this.LikeTemplate;
            case FishyFlip.Constants.FeedType.Post:
                return this.PostTemplate;
            case FishyFlip.Constants.FeedType.Generator:
                return this.GenericTemplate;
            case FishyFlip.Constants.FeedType.Repost:
                return this.GenericTemplate;
            case FishyFlip.Constants.GraphTypes.Follow:
                return this.GenericTemplate;
            case FishyFlip.Constants.GraphTypes.List:
                return this.GenericTemplate;
            case FishyFlip.Constants.GraphTypes.ListItem:
                return this.GenericTemplate;
            case FishyFlip.Constants.GraphTypes.Block:
                return this.GenericTemplate;
            case FishyFlip.Constants.ActorTypes.Profile:
                return this.GenericTemplate;
            case FishyFlip.Constants.FeedType.ThreadGate:
                return this.GenericTemplate;
            default:
                return this.GenericTemplate;
        }
    }
}
