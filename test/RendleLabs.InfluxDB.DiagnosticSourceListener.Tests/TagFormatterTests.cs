using System;
using System.Text;
using Xunit;

namespace RendleLabs.InfluxDB.DiagnosticSourceListener.Tests
{
    public class TagFormatterTests
    {
        [Fact]
        public void WritesStringTag()
        {
            var obj = new { foo = "42" };
            var property = obj.GetType().GetProperty("foo");
            var target = TagFormatter.TryCreate(property, NameFixer.Identity);
            var bytes = new byte[7];
            var span = bytes.AsSpan();
            target.TryWrite(obj, span, true, out int written);
            Assert.Equal(7, written);
            Assert.Equal(Encoding.UTF8.GetBytes(",foo=42"), bytes);
        }

        [Fact]
        public void WritesStringTag_FormatterPropertyName()
        {
            var obj = new { foo = "42" };
            var property = obj.GetType().GetProperty("foo");
            var target = TagFormatter.TryCreate(property, n => n.ToUpperInvariant());
            var bytes = new byte[7];
            var span = bytes.AsSpan();
            target.TryWrite(obj, span, true, out int written);
            Assert.Equal(7, written);
            Assert.Equal(Encoding.UTF8.GetBytes(",FOO=42"), bytes);
        }
    }
}