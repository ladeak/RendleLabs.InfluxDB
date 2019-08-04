using System;
using System.Reflection;

namespace RendleLabs.InfluxDB.DiagnosticSourceListener.TypedFormatters
{
    internal class NullableDateTimeTagFormatter : TypedFormatter<DateTime?>, IFormatter
    {
        public NullableDateTimeTagFormatter(PropertyInfo property, Func<string, string> propertyNameFormatter)
            : base(property, propertyNameFormatter)
        {
        }

        public bool TryWrite(object obj, Span<byte> span, bool commaPrefix, out int bytesWritten) =>
            TagHelpers.TryWriteDateTime(Name.AsSpan(), Getter(obj).GetValueOrDefault(), span, out bytesWritten);
    }
}