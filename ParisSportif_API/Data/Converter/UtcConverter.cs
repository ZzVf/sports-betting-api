using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ParisSportif_API.Data.Converter;

public class UtcConverter : ValueConverter<DateTime, DateTime>
{
    public UtcConverter()
        : base(
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc),   // vers DB
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)    // depuis DB
        )
    {
    }
}



