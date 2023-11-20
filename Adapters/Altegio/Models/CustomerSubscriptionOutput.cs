using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CrmIntegration.Adapters.Altegio.Models
{
    public class BalanceContainer
    {
        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    public class CustomerSubscriptionOutput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("balance_string")]
        public string BalanceString { get; set; }

        [JsonPropertyName("is_frozen")]
        public bool IsFrozen { get; set; }

        [JsonPropertyName("freeze_period")]
        public int FreezePeriod { get; set; }

        [JsonPropertyName("period")]
        public int Period { get; set; }

        [JsonPropertyName("period_unit_id")]
        public int PeriodUnitId { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("balance_container")]
        public BalanceContainer BalanceContainer { get; set; }

        [JsonPropertyName("type")]
        public Type Type { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("category")]
        public Category Category { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class CustomerSubscriptionRoot
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public List<CustomerSubscriptionOutput> Data { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    public class Status
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("extended_title")]
        public string ExtendedTitle { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("salon_group_id")]
        public int SalonGroupId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("period")]
        public int Period { get; set; }

        [JsonPropertyName("period_unit_id")]
        public int PeriodUnitId { get; set; }

        [JsonPropertyName("allow_freeze")]
        public bool AllowFreeze { get; set; }

        [JsonPropertyName("freeze_limit")]
        public int FreezeLimit { get; set; }

        [JsonPropertyName("is_allow_empty_code")]
        public bool IsAllowEmptyCode { get; set; }

        [JsonPropertyName("is_united_balance")]
        public bool IsUnitedBalance { get; set; }

        [JsonPropertyName("united_balance_services_count")]
        public int UnitedBalanceServicesCount { get; set; }

        [JsonPropertyName("balance_container")]
        public BalanceContainer BalanceContainer { get; set; }
    }
}
