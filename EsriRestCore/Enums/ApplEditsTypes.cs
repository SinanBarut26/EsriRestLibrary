using System.ComponentModel;

namespace EsriRestLibrary.Core.Enums
{
    internal enum ApplyEditsTypes
    {
        [Description("adds")] Add = 1,
        [Description("updates")] Update = 2,
        [Description("deletes ")] Delete = 3
    }
}
