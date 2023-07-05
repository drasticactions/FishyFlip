// <copyright file="Label.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public record Label(AtUri Src, string Uri, string Cid, string Val, bool Neg, DateTime Cts)
{
}
