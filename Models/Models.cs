using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListChallengeApi.Models
{
    [Table("child")]
    public class Child
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public Guid FactoryId { get; set; }
        public int Value { get; set; }
    }
    [Table("factory")]
    public class Factory
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public Guid RootId { get; set; }
        public int RangeLow { get; set; }
        public int RangeHigh { get; set; }
        [Column(TypeName = "varchar(30)")]
        [StringLength(30)]
        public string Label { get; set; }
        public IEnumerable<Child> Childs { get; set; }
    }
    [Table("root")]
    public class Root
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(30)")]
        [StringLength(30)]
        public string Label { get; set; }
        public List<Factory> Factories { get; set; }
    }
}