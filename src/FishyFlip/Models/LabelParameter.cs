// <copyright file="LabelParameter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace FishyFlip.Models;

/// <summary>
/// Represents a label parameter.
/// </summary>
public class LabelParameter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LabelParameter"/> class.
    /// </summary>
    /// <param name="labelDid">The label did.</param>
    /// <param name="parameters">Optional list of parameters for the label.</param>
    internal LabelParameter(ATDid labelDid, List<string>? parameters = null)
    {
        this.LabelDid = labelDid;
        this.Parameters = parameters ?? new List<string>();
    }

    /// <summary>
    /// Gets the Default Bluesky Moderation Label.
    /// </summary>
    public static LabelParameter BlueskyModeration => new LabelParameter(ATDid.Create(Constants.BlueskyModerationServiceDid)!, ["redact"]);

    /// <summary>
    /// Gets the Label ATDid.
    /// </summary>
    public ATDid LabelDid { get; }

    /// <summary>
    /// Gets the optional parameter set.
    /// </summary>
    public List<string> Parameters { get; }

    /// <summary>
    /// Creates a new label parameter.
    /// </summary>
    /// <param name="labelDid">The label did.</param>
    /// <param name="parameters">Optional list of parameters for the label.</param>
    /// <returns>LabelParameter.</returns>
    public static LabelParameter Create(string labelDid, List<string>? parameters = null)
    {
        return new LabelParameter(ATDid.Create(labelDid)!, parameters);
    }

    /// <summary>
    /// Converts the label parameter to a string.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        return $"{this.LabelDid}{(this.Parameters.Count > 0 ? $";{string.Join(";", this.Parameters)}" : string.Empty)}";
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is LabelParameter lp)
        {
            return this.LabelDid.Equals(lp.LabelDid) && lp.Parameters.SequenceEqual(this.Parameters);
        }

        return false;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        int hash = 17;
        hash = (hash * 31) + this.LabelDid.GetHashCode();
        hash = (hash * 31) + this.Parameters.Aggregate(0, (acc, param) => (acc * 31) + param.GetHashCode());
        return hash;
    }
}