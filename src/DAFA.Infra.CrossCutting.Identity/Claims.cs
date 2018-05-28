using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAFA.Infra.CrossCutting.Identity
{
    [Table("AspNetClaims")]
    public class Claims
    {
        public Claims()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; private set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Claim must have a name")]
        [MaxLength(128, ErrorMessage = "Claim Name can not exceed {0} characters")]
        [Display(Name = "Claim Name")]
        public string Name { get; set; }

        /// <summary>
        /// Factory used to seed the database context.
        /// </summary>
        /// <param name="claimId">Claim Unique Id.</param>
        /// <param name="name">Name of the Claim.</param>
        /// <returns>See <see cref="Claims"/>.</returns>
        public static Claims FactorySeed(string claimId, string name)
        {
            return new Claims
            {
                Id = Guid.Parse(claimId),
                Name = name
            };
        }
    }
}
