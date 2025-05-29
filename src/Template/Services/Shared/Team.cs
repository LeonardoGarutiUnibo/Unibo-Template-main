using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Template.Services.Shared
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Checks if password passed as parameter matches with the Password of the current user
        /// </summary>
        /// <param name="password">password to check</param>
        /// <returns>True if passwords match. False otherwise.</returns>
    }
}
