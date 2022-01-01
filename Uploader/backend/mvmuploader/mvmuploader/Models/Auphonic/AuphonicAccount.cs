using Newtonsoft.Json;
using System;

namespace MvM.Uploader.Backend.Web.Models.Auphonic
{
    public class AuphonicAccount
    {

        public string Username { get; set; }
        public string Email { get; set; }
        [JsonProperty("date_joined")]
        public DateTime Joined { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("notification_email")]
        public bool SendNotificationMail { get; set; }
        [JsonProperty("error_email ")]
        public bool SendErrorMail { get; set; }
        [JsonProperty("warning_email")]
        public bool SendWarningMail { get; set; }
        [JsonProperty("low_credits_email")]
        public bool SendLowCreditsMail { get; set; }
        [JsonProperty("low_credits_threshold")]
        public int LowCreditsThreshold { get; set; }
        public float Credits { get; set; }
        [JsonProperty("recurring_credits")]
        public float RecurringCredits { get; set; }
        [JsonProperty("onetime_credits")]
        public float OnetimeCredits { get; set; }
        [JsonProperty("recharge_date")]
        public DateTime RechargeDate { get; set; }
        [JsonProperty("recharge_recurring_credits")]
        public float RechargeRecurringCredits { get; set; }
    }
}
