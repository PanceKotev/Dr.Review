namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    using Newtonsoft.Json;

    public class LocationsResponse
    {
        [JsonProperty(1)]
        public LocationResponse One {get; set;}

        [JsonProperty("2")]
        public LocationResponse Two { get; set; }

        [JsonProperty("3")]
        public LocationResponse Three { get; set; }

        [JsonProperty("4")]
        public LocationResponse Four { get; set; }

        [JsonProperty("5")]
        public LocationResponse Five { get; set; }

        [JsonProperty("6")]
        public LocationResponse Six { get; set; }

        [JsonProperty("7")]
        public LocationResponse Seven { get; set; }

        [JsonProperty("8")]
        public LocationResponse Eight { get; set; }

        [JsonProperty("9")]
        public LocationResponse Nine { get; set; }

        [JsonProperty("10")]
        public LocationResponse Ten { get; set; }

        [JsonProperty("11")]
        public LocationResponse Eleven { get; set; }

        [JsonProperty("12")]
        public LocationResponse Twelve { get; set; }

        [JsonProperty("13")]
        public LocationResponse Thirteen { get; set; }

        [JsonProperty("14")]
        public LocationResponse Fourteen { get; set; }

        [JsonProperty("16")]
        public LocationResponse Fifteen { get; set; }

        [JsonProperty("16")]
        public LocationResponse Sixteen { get; set; }
    }
}
