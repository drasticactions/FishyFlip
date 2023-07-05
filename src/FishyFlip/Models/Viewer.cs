using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishyFlip.Models;

public record Viewer(
    bool Muted,
    bool BlockedBy,
    string Following
);
