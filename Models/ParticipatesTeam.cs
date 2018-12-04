namespace ReforgedApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParticipatesTeam")]
    public partial class ParticipatesTeam
    {
        [Key]
        [Column(Order = 0)]
        public int userId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int teamId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int armyTotal { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int totalKills { get; set; }

        public int? ffaPlacing { get; set; }

        public virtual Team Team { get; set; }

        public virtual User User { get; set; }
    }
}
