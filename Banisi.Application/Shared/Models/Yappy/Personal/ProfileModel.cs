﻿using System.Text.Json.Serialization;

namespace Banisi.Application.Shared.Models.Yappy.Personal
{
    public class ProfileResponseModel
    {
        [JsonPropertyName("body")]
        public Body Body { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        [JsonPropertyName("otp")]
        public long Otp { get; set; }

        [JsonPropertyName("unique_id")]
        public Guid UniqueId { get; set; }

        [JsonPropertyName("terms_conditions")]
        public TermsConditions TermsConditions { get; set; }

        [JsonPropertyName("kyc")]
        public Kyc Kyc { get; set; }
    }

    public class Kyc
    {
        [JsonPropertyName("fingerprint")]
        public Fingerprint Fingerprint { get; set; }

        [JsonPropertyName("geolocation")]
        public Geolocation Geolocation { get; set; }
    }

    public class Fingerprint
    {
        [JsonPropertyName("hardware_id")]
        public string HardwareId { get; set; }

        [JsonPropertyName("device_model")]
        public string DeviceModel { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        [JsonPropertyName("os_name")]
        public string OsName { get; set; }

        [JsonPropertyName("version")]
        public long Version { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("wifi_ssid")]
        public string WifiSsid { get; set; }

        [JsonPropertyName("localization_area_id")]
        public object LocalizationAreaId { get; set; }

        [JsonPropertyName("screen_size")]
        public string ScreenSize { get; set; }

        [JsonPropertyName("time_stamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonPropertyName("compromised")]
        public bool Compromised { get; set; }

        [JsonPropertyName("emulator")]
        public bool Emulator { get; set; }

        [JsonPropertyName("requested_ip")]
        public string RequestedIp { get; set; }
    }

    public class Geolocation
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }

    public class TermsConditions
    {
        [JsonPropertyName("accepted")]
        public bool Accepted { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}