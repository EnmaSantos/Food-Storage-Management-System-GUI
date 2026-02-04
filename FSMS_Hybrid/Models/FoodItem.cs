using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace FSMS_Hybrid.Models
{
    [Table("inventory")]
    public class FoodItem : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; } = string.Empty;

        [Column("item_type")]
        public string ItemType { get; set; } = string.Empty;

        [Column("expiration_date")]
        public DateTime? ExpirationDate { get; set; }

        [Column("item_price")]
        public float ItemPrice { get; set; }

        [Column("item_quantity")]
        public int ItemQuantity { get; set; } = 1;

        [Column("storage_location")]
        public string StorageLocation { get; set; } = string.Empty;

        [Column("item_unique_code")]
        public string ItemUniqueCode { get; set; } = string.Empty;

        [Column("date_added")]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [Column("is_expired")]
        public bool IsExpired { get; set; }

        [Column("barcode")]
        public string? Barcode { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
