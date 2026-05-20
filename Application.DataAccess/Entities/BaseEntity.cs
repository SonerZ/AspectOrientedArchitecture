using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DataAccess.Entities.Concrete
{
    /// <summary>
    /// BaseEntity provides common properties to database entities.
    /// </summary>
    /// <typeparam name="TKey">Unique key.</typeparam>
    public abstract class BaseEntity<TKey>
    {
        /// <summary>
        /// Generic unique key.
        /// </summary>
        [Key]
        public TKey Id { get; set; }

        /// <summary>
        /// Date of entity creation.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public string? CreatedUsername { get; set; }

        /// <summary>
        /// Date of last modification.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedUsername { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string? DeletedUsername { get; set; }
    }
}
