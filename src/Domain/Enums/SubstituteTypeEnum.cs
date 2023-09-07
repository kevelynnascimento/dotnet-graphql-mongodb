using System.ComponentModel;

namespace Domain.Enums;

public enum SubstituteTypeEnum : byte
{
    [Description("Delete product from list")]
    Delete = 3,
    [Description("Replace product instead of me")]
    Replace = 6,
    [Description("Ask me")]
    AskMe = 10
}