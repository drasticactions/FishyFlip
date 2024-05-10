using FishyFlip.Models;

namespace SampleAppMaui;

public class ATRecordDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate GenericTemplate { get; set; }

    public DataTemplate LikeTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        switch (((ATRecord)item).Type)
        {
            case FishyFlip.Constants.FeedType.Like:
                return this.LikeTemplate;
            default:
                return this.GenericTemplate;
        }
    }
}
