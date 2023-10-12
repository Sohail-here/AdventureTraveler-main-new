using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripeData
{
    //Need to add the login information after build
    public static string UserName = "penguin_test";
    public static string UserMail = "cutesamllpgin@gmail.com";
    public static string UserToken = "cus_Kzavx8cyCo8VI7";
    //Must be modified to restricted content, otherwise there will be danger of being tampered with.  note by Penguin
    public static string SKey = "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";
    public class Product
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public int created { get; set; }
        public string description { get; set; }
        public string images { get; set; }
        public bool livemode { get; set; }
        public string plink { get; set; }
        public string updated { get; set; }
    }

    public class Price
    {
        public class Metadata
        {
        }

        public class Recurring
        {
            public object aggregate_usage { get; set; }
            public string interval { get; set; }
            public int interval_count { get; set; }
            public string usage_type { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string @object { get; set; }
            public bool active { get; set; }
            public string billing_scheme { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public bool livemode { get; set; }
            public object lookup_key { get; set; }
            public Metadata metadata { get; set; }
            public object nickname { get; set; }
            public string product { get; set; }
            public Recurring recurring { get; set; }
            public string tax_behavior { get; set; }
            public object tiers_mode { get; set; }
            public object transform_quantity { get; set; }
            public string type { get; set; }
            public int unit_amount { get; set; }
            public string unit_amount_decimal { get; set; }
        }

        public class Root
        {
            public string @object { get; set; }
            public string url { get; set; }
            public bool has_more { get; set; }
            public List<Datum> data { get; set; }
        }
    }

    public class UserData
    {
        public class InvoiceSettings
        {
            public object custom_fields { get; set; }
            public object default_payment_method { get; set; }
            public object footer { get; set; }
        }

        public class Metadata
        {
        }

        public class Root
        {
            public string id { get; set; }
            public string @object { get; set; }
            public object address { get; set; }
            public int balance { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public object default_source { get; set; }
            public bool delinquent { get; set; }
            public string description { get; set; }
            public object discount { get; set; }
            public object email { get; set; }
            public string invoice_prefix { get; set; }
            public InvoiceSettings invoice_settings { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public object name { get; set; }
            public int next_invoice_sequence { get; set; }
            public object phone { get; set; }
            public List<object> preferred_locales { get; set; }
            public object shipping { get; set; }
            public string tax_exempt { get; set; }
        }
    }

    public class InvoiceItem
    {
        public class Metadata
        {
        }

        public class Period
        {
            public int end { get; set; }
            public int start { get; set; }
        }

        public class Price
        {
            public string id { get; set; }
            public string @object { get; set; }
            public bool active { get; set; }
            public string billing_scheme { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public bool livemode { get; set; }
            public object lookup_key { get; set; }
            public Metadata metadata { get; set; }
            public object nickname { get; set; }
            public string product { get; set; }
            public object recurring { get; set; }
            public string tax_behavior { get; set; }
            public object tiers_mode { get; set; }
            public object transform_quantity { get; set; }
            public string type { get; set; }
            public int unit_amount { get; set; }
            public string unit_amount_decimal { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string @object { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string customer { get; set; }
            public int date { get; set; }
            public string description { get; set; }
            public bool discountable { get; set; }
            public List<object> discounts { get; set; }
            public object invoice { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public Period period { get; set; }
            public object plan { get; set; }
            public Price price { get; set; }
            public bool proration { get; set; }
            public int quantity { get; set; }
            public object subscription { get; set; }
            public List<object> tax_rates { get; set; }
            public int unit_amount { get; set; }
            public string unit_amount_decimal { get; set; }
        }
    }

    public class InvoiceInfo
    {
        public class AutomaticTax
        {
            public bool enabled { get; set; }
            public object status { get; set; }
        }

        public class Metadata
        {
        }

        public class Period
        {
            public int end { get; set; }
            public int start { get; set; }
        }

        public class Price
        {
            public string id { get; set; }
            public string @object { get; set; }
            public bool active { get; set; }
            public string billing_scheme { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public bool livemode { get; set; }
            public object lookup_key { get; set; }
            public Metadata metadata { get; set; }
            public object nickname { get; set; }
            public string product { get; set; }
            public object recurring { get; set; }
            public string tax_behavior { get; set; }
            public object tiers_mode { get; set; }
            public object transform_quantity { get; set; }
            public string type { get; set; }
            public int unit_amount { get; set; }
            public string unit_amount_decimal { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string @object { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string description { get; set; }
            public List<object> discount_amounts { get; set; }
            public bool discountable { get; set; }
            public List<object> discounts { get; set; }
            public string invoice_item { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public Period period { get; set; }
            public object plan { get; set; }
            public Price price { get; set; }
            public bool proration { get; set; }
            public int quantity { get; set; }
            public object subscription { get; set; }
            public List<object> tax_amounts { get; set; }
            public List<object> tax_rates { get; set; }
            public string type { get; set; }
        }

        public class Lines
        {
            public string @object { get; set; }
            public List<Datum> data { get; set; }
            public bool has_more { get; set; }
            public int total_count { get; set; }
            public string url { get; set; }
        }

        public class PaymentSettings
        {
            public object payment_method_options { get; set; }
            public object payment_method_types { get; set; }
        }

        public class StatusTransitions
        {
            public object finalized_at { get; set; }
            public object marked_uncollectible_at { get; set; }
            public object paid_at { get; set; }
            public object voided_at { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string @object { get; set; }
            public string account_country { get; set; }
            public string account_name { get; set; }
            public object account_tax_ids { get; set; }
            public int amount_due { get; set; }
            public int amount_paid { get; set; }
            public int amount_remaining { get; set; }
            public object application_fee_amount { get; set; }
            public int attempt_count { get; set; }
            public bool attempted { get; set; }
            public bool auto_advance { get; set; }
            public AutomaticTax automatic_tax { get; set; }
            public string billing_reason { get; set; }
            public object charge { get; set; }
            public string collection_method { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public object custom_fields { get; set; }
            public string customer { get; set; }
            public object customer_address { get; set; }
            public string customer_email { get; set; }
            public string customer_name { get; set; }
            public object customer_phone { get; set; }
            public object customer_shipping { get; set; }
            public string customer_tax_exempt { get; set; }
            public List<object> customer_tax_ids { get; set; }
            public object default_payment_method { get; set; }
            public object default_source { get; set; }
            public List<object> default_tax_rates { get; set; }
            public object description { get; set; }
            public object discount { get; set; }
            public List<object> discounts { get; set; }
            public object due_date { get; set; }
            public object ending_balance { get; set; }
            public object footer { get; set; }
            public object hosted_invoice_url { get; set; }
            public object invoice_pdf { get; set; }
            public object last_finalization_error { get; set; }
            public Lines lines { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public object next_payment_attempt { get; set; }
            public object number { get; set; }
            public object on_behalf_of { get; set; }
            public bool paid { get; set; }
            public bool paid_out_of_band { get; set; }
            public object payment_intent { get; set; }
            public PaymentSettings payment_settings { get; set; }
            public int period_end { get; set; }
            public int period_start { get; set; }
            public int post_payment_credit_notes_amount { get; set; }
            public int pre_payment_credit_notes_amount { get; set; }
            public object quote { get; set; }
            public object receipt_number { get; set; }
            public int starting_balance { get; set; }
            public object statement_descriptor { get; set; }
            public string status { get; set; }
            public StatusTransitions status_transitions { get; set; }
            public object subscription { get; set; }
            public int subtotal { get; set; }
            public object tax { get; set; }
            public int total { get; set; }
            public List<object> total_discount_amounts { get; set; }
            public List<object> total_tax_amounts { get; set; }
            public object transfer_data { get; set; }
            public int webhooks_delivered_at { get; set; }
        }
    }

    public class InvoicesDatas
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AutomaticTax
        {
            public bool enabled { get; set; }
            public object status { get; set; }
        }

        public class Metadata
        {
        }

        public class Period
        {
            public int end { get; set; }
            public int start { get; set; }
        }

        public class Plan
        {
            public string id { get; set; }
            public string @object { get; set; }
            public bool active { get; set; }
            public object aggregate_usage { get; set; }
            public int amount { get; set; }
            public string amount_decimal { get; set; }
            public string billing_scheme { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public string interval { get; set; }
            public int interval_count { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public object nickname { get; set; }
            public string product { get; set; }
            public object tiers_mode { get; set; }
            public object transform_usage { get; set; }
            public object trial_period_days { get; set; }
            public string usage_type { get; set; }
        }

        public class Recurring
        {
            public object aggregate_usage { get; set; }
            public string interval { get; set; }
            public int interval_count { get; set; }
            public object trial_period_days { get; set; }
            public string usage_type { get; set; }
        }

        public class Price
        {
            public string id { get; set; }
            public string @object { get; set; }
            public bool active { get; set; }
            public string billing_scheme { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public bool livemode { get; set; }
            public object lookup_key { get; set; }
            public Metadata metadata { get; set; }
            public object nickname { get; set; }
            public string product { get; set; }
            public Recurring recurring { get; set; }
            public string tax_behavior { get; set; }
            public object tiers_mode { get; set; }
            public object transform_quantity { get; set; }
            public string type { get; set; }
            public int unit_amount { get; set; }
            public string unit_amount_decimal { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string @object { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string description { get; set; }
            public List<object> discount_amounts { get; set; }
            public bool discountable { get; set; }
            public List<object> discounts { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public Period period { get; set; }
            public Plan plan { get; set; }
            public Price price { get; set; }
            public bool proration { get; set; }
            public int quantity { get; set; }
            public string subscription { get; set; }
            public string subscription_item { get; set; }
            public List<object> tax_amounts { get; set; }
            public List<object> tax_rates { get; set; }
            public string type { get; set; }
            public string invoice_item { get; set; }
            public string account_country { get; set; }
            public string account_name { get; set; }
            public object account_tax_ids { get; set; }
            public int amount_due { get; set; }
            public int amount_paid { get; set; }
            public int amount_remaining { get; set; }
            public object application_fee_amount { get; set; }
            public int attempt_count { get; set; }
            public bool attempted { get; set; }
            public bool auto_advance { get; set; }
            public AutomaticTax automatic_tax { get; set; }
            public string billing_reason { get; set; }
            public object charge { get; set; }
            public string collection_method { get; set; }
            public int created { get; set; }
            public object custom_fields { get; set; }
            public string customer { get; set; }
            public object customer_address { get; set; }
            public string customer_email { get; set; }
            public string customer_name { get; set; }
            public object customer_phone { get; set; }
            public object customer_shipping { get; set; }
            public string customer_tax_exempt { get; set; }
            public List<object> customer_tax_ids { get; set; }
            public object default_payment_method { get; set; }
            public object default_source { get; set; }
            public List<object> default_tax_rates { get; set; }
            public object discount { get; set; }
            public int? due_date { get; set; }
            public int ending_balance { get; set; }
            public object footer { get; set; }
            public string hosted_invoice_url { get; set; }
            public string invoice_pdf { get; set; }
            public object last_finalization_error { get; set; }
            public Lines lines { get; set; }
            public object next_payment_attempt { get; set; }
            public string number { get; set; }
            public object on_behalf_of { get; set; }
            public bool paid { get; set; }
            public bool paid_out_of_band { get; set; }
            public string payment_intent { get; set; }
            public PaymentSettings payment_settings { get; set; }
            public int period_end { get; set; }
            public int period_start { get; set; }
            public int post_payment_credit_notes_amount { get; set; }
            public int pre_payment_credit_notes_amount { get; set; }
            public object quote { get; set; }
            public object receipt_number { get; set; }
            public int starting_balance { get; set; }
            public object statement_descriptor { get; set; }
            public string status { get; set; }
            public StatusTransitions status_transitions { get; set; }
            public int subtotal { get; set; }
            public object tax { get; set; }
            public int total { get; set; }
            public List<object> total_discount_amounts { get; set; }
            public List<object> total_tax_amounts { get; set; }
            public object transfer_data { get; set; }
            public int webhooks_delivered_at { get; set; }
        }

        public class Lines
        {
            public string @object { get; set; }
            public List<Datum> data { get; set; }
            public bool has_more { get; set; }
            public int total_count { get; set; }
            public string url { get; set; }
        }

        public class PaymentSettings
        {
            public object payment_method_options { get; set; }
            public object payment_method_types { get; set; }
        }

        public class StatusTransitions
        {
            public int finalized_at { get; set; }
            public object marked_uncollectible_at { get; set; }
            public object paid_at { get; set; }
            public object voided_at { get; set; }
        }

        public class Root
        {
            public string @object { get; set; }
            public List<Datum> data { get; set; }
            public bool has_more { get; set; }
            public string url { get; set; }
        }
    }

    public class SubscriptionsData
    {
        public class AutomaticTax
        {
            public bool enabled { get; set; }
        }

        public class Metadata
        {
        }

        public class Plan
        {
            public string id { get; set; }
            public string @object { get; set; }
            public bool active { get; set; }
            public object aggregate_usage { get; set; }
            public int amount { get; set; }
            public string amount_decimal { get; set; }
            public string billing_scheme { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public string interval { get; set; }
            public int interval_count { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public object nickname { get; set; }
            public string product { get; set; }
            public object tiers_mode { get; set; }
            public object transform_usage { get; set; }
            public object trial_period_days { get; set; }
            public string usage_type { get; set; }
        }

        public class Recurring
        {
            public object aggregate_usage { get; set; }
            public string interval { get; set; }
            public int interval_count { get; set; }
            public object trial_period_days { get; set; }
            public string usage_type { get; set; }
        }

        public class Price
        {
            public string id { get; set; }
            public string @object { get; set; }
            public bool active { get; set; }
            public string billing_scheme { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public bool livemode { get; set; }
            public object lookup_key { get; set; }
            public Metadata metadata { get; set; }
            public object nickname { get; set; }
            public string product { get; set; }
            public Recurring recurring { get; set; }
            public string tax_behavior { get; set; }
            public object tiers_mode { get; set; }
            public object transform_quantity { get; set; }
            public string type { get; set; }
            public int unit_amount { get; set; }
            public string unit_amount_decimal { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string @object { get; set; }
            public object billing_thresholds { get; set; }
            public int created { get; set; }
            public Metadata metadata { get; set; }
            public Plan plan { get; set; }
            public Price price { get; set; }
            public int quantity { get; set; }
            public string subscription { get; set; }
            public List<object> tax_rates { get; set; }
        }

        public class Items
        {
            public string @object { get; set; }
            public List<Datum> data { get; set; }
            public bool has_more { get; set; }
            public int total_count { get; set; }
            public string url { get; set; }
        }

        public class PaymentSettings
        {
            public object payment_method_options { get; set; }
            public object payment_method_types { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string @object { get; set; }
            public object application_fee_percent { get; set; }
            public AutomaticTax automatic_tax { get; set; }
            public int billing_cycle_anchor { get; set; }
            public object billing_thresholds { get; set; }
            public object cancel_at { get; set; }
            public bool cancel_at_period_end { get; set; }
            public object canceled_at { get; set; }
            public string collection_method { get; set; }
            public int created { get; set; }
            public int current_period_end { get; set; }
            public int current_period_start { get; set; }
            public string customer { get; set; }
            public object days_until_due { get; set; }
            public object default_payment_method { get; set; }
            public object default_source { get; set; }
            public List<object> default_tax_rates { get; set; }
            public object discount { get; set; }
            public object ended_at { get; set; }
            public Items items { get; set; }
            public string latest_invoice { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public object next_pending_invoice_item_invoice { get; set; }
            public object pause_collection { get; set; }
            public PaymentSettings payment_settings { get; set; }
            public object pending_invoice_item_interval { get; set; }
            public object pending_setup_intent { get; set; }
            public object pending_update { get; set; }
            public Plan plan { get; set; }
            public int quantity { get; set; }
            public object schedule { get; set; }
            public int start_date { get; set; }
            public string status { get; set; }
            public object transfer_data { get; set; }
            public object trial_end { get; set; }
            public object trial_start { get; set; }
        }


    }

    public class SourceData
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AchCreditTransfer
        {
            public string account_number { get; set; }
            public string routing_number { get; set; }
            public string fingerprint { get; set; }
            public string swift_code { get; set; }
            public string bank_name { get; set; }
            public object refund_routing_number { get; set; }
            public object refund_account_holder_type { get; set; }
            public object refund_account_holder_name { get; set; }
        }

        public class Metadata
        {
        }

        public class Owner
        {
            public object address { get; set; }
            public string email { get; set; }
            public object name { get; set; }
            public object phone { get; set; }
            public object verified_address { get; set; }
            public object verified_email { get; set; }
            public object verified_name { get; set; }
            public object verified_phone { get; set; }
        }

        public class Receiver
        {
            public string address { get; set; }
            public int amount_charged { get; set; }
            public int amount_received { get; set; }
            public int amount_returned { get; set; }
            public string refund_attributes_method { get; set; }
            public string refund_attributes_status { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string @object { get; set; }
            public AchCreditTransfer ach_credit_transfer { get; set; }
            public object amount { get; set; }
            public string client_secret { get; set; }
            public int created { get; set; }
            public string currency { get; set; }
            public string flow { get; set; }
            public bool livemode { get; set; }
            public Metadata metadata { get; set; }
            public Owner owner { get; set; }
            public Receiver receiver { get; set; }
            public object statement_descriptor { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public string usage { get; set; }
        }


    }
}
