﻿using System;
using System.Linq;
using System.Windows.Markup;

namespace Common.MarkupExtensions
{
    public class Enumeration : MarkupExtension
    {
        private readonly Type _type;

        public Enumeration(Type type)
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var enums = Enum.GetValues(_type)
                .Cast<object>()
                .Select(e => new { Value = (int)e, DisplayName = e.ToString() });
            return enums;
        }
    }
}
