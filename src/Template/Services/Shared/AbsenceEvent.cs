using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Template.Services.Shared
{
    public class AbsenceEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }
        public string EventType { get; set; }
        public string EventState { get; set; }
    }
}
