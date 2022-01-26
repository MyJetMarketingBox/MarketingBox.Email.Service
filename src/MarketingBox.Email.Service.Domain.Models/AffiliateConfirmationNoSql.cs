using MyNoSqlServer.Abstractions;

namespace Service.MarketingBox.Email.Service.Domain.Models
{
    public class AffiliateConfirmationNoSql : MyNoSqlDbEntity
    {
        public const string TableName = "marketingbox-affiliate-confirmation";
        private static string GeneratePartitionKey() => $"AffiliateConfirmation";
        private static string GenerateRowKey(long affiliateId) => affiliateId.ToString();
        public AffiliateConfirmation Entity { get; set; }
        
        public static AffiliateConfirmationNoSql Create(AffiliateConfirmation entity)
        {
            return new AffiliateConfirmationNoSql()
            {
                PartitionKey = GeneratePartitionKey(),
                RowKey = GenerateRowKey(entity.AffiliateId),
                Entity = entity
            };
        }
    }
}