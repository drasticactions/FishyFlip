using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

/// <summary>
/// Record with Media Embed.
/// </summary>
public class RecordWithMediaViewEmbed : Embed
{
    [JsonConstructor]
    public RecordWithMediaViewEmbed(RecordViewEmbed? record, ImageViewEmbed? images)
    {
        this.Record = record;
        this.Images = images;
        this.Type = Constants.EmbedTypes.RecordWithMedia;
    }

    public RecordViewEmbed? Record { get; }

    public ImageViewEmbed? Images { get; }
}