using System.ComponentModel;

namespace Domain.Enums;

public enum SubstituteTypeSkuDataEnum : byte
{
    [Description("Delete product from list")]
    Delete = 2,
    [Description("Replace product instead of me")]
    Replace = 6,
    [Description("Ask me")]
    AskMe = 10
}