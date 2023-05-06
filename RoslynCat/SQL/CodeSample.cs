﻿using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace RoslynCat.SQL
{
    public class CodeSample
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 255,IsNullable = false)]
        public string Group { get; set; }

        [Required]
        [SugarColumn(IsNullable = false)]
        public string Title { get; set; }

        [Required]
        [CodeValidation]
        [MaxLength(10000)]
        [SugarColumn(IsNullable = false,Length = 10000)]
        public string Code { get; set; }

        [SugarColumn(Length = 50,IsNullable = false)]
        public string Language { get; set; }

        [SugarColumn(IsNullable = false,ColumnDataType = "datetime2(0)",ColumnDescription = "UTC时间")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [SugarColumn(IsNullable = false,ColumnDataType = "datetime2(0)",ColumnDescription = "UTC时间")]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
